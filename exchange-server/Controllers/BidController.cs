using exchange_server.Models.BidModel;
using exchange_server.Models.CommonModel;
using exchange_server.Models.SessionModel;
using exchange_server.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace exchange_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly SessionService _service;
        public BidController(SessionService service)
        {
            _service = service;
        }

        // POST api/<BidController>
        private System.Timers.Timer RequestTimer;

        [Route("~/bid_request")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(BidResponse))]
        [ProducesResponseType(400, Type = typeof(HttpErrorMessage))]
        public IActionResult Post([FromBody] BidRequest req)
        {

            try
            {
                //todo: commit前取消註解
                //this.RequestTimer = new System.Timers.Timer();
                //this.RequestTimer.Interval = req.timeout_ms.Value;
                //RequestTimer.Start();

                if (ModelState.IsValid)
                {
                    //Check request validation.
                    string bidRequestErrorMessage = _service.CheckBidRequestValidation(req);
                    if (String.IsNullOrEmpty(bidRequestErrorMessage))
                    {
                        //add this bidrequest into SessionInfoDic
                        BidResponse bidResponse = new BidResponse()
                        {
                            session_id = req.session_id,
                            request_id = req.request_id
                        };
                        //To aquire bidder's name and bidder's enpoint in this session.
                        List<Bidder> bidders = _service.GestSessionBidders(req.session_id);

                        foreach (Bidder bidder in bidders)
                        {
                            BidderResponse response = PostBidRequestToBidder(req, bidder);
                            bidResponse.bidderResponses.Add(response);
                        }
                        _service.AddBidRequest(req);

                        bidResponse.win_bid = _service.PickAWinner(bidResponse.bidderResponses, req.floor_price);

                        #region Post notify_win_bid when there's winner in this bid.
                        if (null != bidResponse.win_bid)
                        {
                            NotifyWinBidRequest notifyWinBidRequest = new NotifyWinBidRequest()
                            {
                                session_id = req.session_id,
                                request_id = req.request_id,
                                clear_price = bidResponse.win_bid.price,
                                name = bidResponse.win_bid.name

                            };
                            NotifyWinBid(notifyWinBidRequest);
                        }
                        #endregion
                        return Ok(bidResponse);

                    }
                    else
                    {
                        HttpErrorMessage httpErrorMessage = new HttpErrorMessage();
                        httpErrorMessage.error = bidRequestErrorMessage;
                        return BadRequest(httpErrorMessage);
                    }
                }
                else
                {
                    return BadRequest("This bid_request is invalid.");
                }
            }
            catch (Exception ex)
            {
                HttpErrorMessage httpErrorMessage = new HttpErrorMessage();
                httpErrorMessage.error = ex.Message;
                return BadRequest(httpErrorMessage);

            }
        }

        private IActionResult NotifyWinBid(NotifyWinBidRequest notifyWinBidRequest)
        {
            var url = _service.GetBidderEndpoint(notifyWinBidRequest.session_id) + "notify_win_bid";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "POST";
            httpRequest.Accept = "application/json";

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(JsonConvert.SerializeObject(notifyWinBidRequest));
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseFromServer = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<IActionResult>(responseFromServer);
            }
        }

        private BidderResponse PostBidRequestToBidder(BidRequest postData, Bidder bidder)
        {
            
            var url = bidder.endpoint + "bid_request";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "POST";

            httpRequest.Accept = "application/json";


            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                postData.name = bidder.name;
                streamWriter.Write(JsonConvert.SerializeObject( postData ));
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseFromServer = streamReader.ReadToEnd();
                var responseJson = JsonConvert.DeserializeObject<BidderResponse>(responseFromServer);
                return new BidderResponse() {
                    name = responseJson.name,
                    price = responseJson.price
                };

            };
        }
    }
}
