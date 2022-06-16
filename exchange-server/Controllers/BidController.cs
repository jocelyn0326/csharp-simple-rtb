using exchange_server.Models.BidModel;
using exchange_server.Models.CommonModel;
using exchange_server.Models.SessionModel;
using exchange_server.Services;
using Microsoft.AspNetCore.Mvc;
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

        [Route("~/bid_request")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(BidResponse))]
        [ProducesResponseType(400, Type = typeof(HttpErrorMessage))]

        public IActionResult Post([FromBody] BidRequest req)
        {

            if (ModelState.IsValid) {
                try
                {
                    // todo:how to set expire time?
                    int timeout = req.timeout_ms.Value;
                    string bidRequestErrorMessage = _service.CheckBidRequestValidation(req);
                    if (String.IsNullOrEmpty(bidRequestErrorMessage))
                    {
                        _service.AddBidRequest(req);
                        BidResponse bidResponse = new BidResponse();
                        return Ok(bidResponse);

                    }
                    else
                    {
                        HttpErrorMessage httpErrorMessage = new HttpErrorMessage();
                        httpErrorMessage.error= bidRequestErrorMessage;
                        return BadRequest(httpErrorMessage);
                    }
                }
                catch(Exception ex)
                {
                    HttpErrorMessage httpErrorMessage = new HttpErrorMessage();
                    httpErrorMessage.error = ex.Message;
                    return BadRequest(httpErrorMessage);

                }

            }
                

            return null;
        }



        
    }
}
