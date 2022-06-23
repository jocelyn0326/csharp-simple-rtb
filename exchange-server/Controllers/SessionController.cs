using exchange_server.Models.CommonModel;
using exchange_server.Models.SessionModel;
using exchange_server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using static exchange_server.Startup;

namespace exchange_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {

        private readonly SessionService _service;
        public SessionController(SessionService service)
        {
            _service = service;
        }
        [Route("~/init_session")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(HttpErrorMessage))]
        // POST /init_session
        [HttpPost]
        public IActionResult Post([FromBody] InitSessionRequest req)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (_service.CheckSessionIdUniqueness(req.session_id) && _service.CheckBidderNameUniqueness(req.bidders))
                    {
                        SessionResponse response = PostInitToBidder(req);
                        if (response.result == HttpStatusCode.OK)
                        {
                            _service.AddSession(req);
                        }
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("The request is invalid. Each session_id & bidders' name must be unique");

                    }

                }
                catch(Exception ex)
                {
                    return BadRequest($"session_id: {req.session_id}, {ex.Message}" );
                }
                
            }
            else
            {
                return BadRequest( "The request is invalid.");
            }
           

        }

        [Route("~/end_session")]
        // POST /end_session
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(HttpErrorMessage))]
        [HttpPost]
        public IActionResult Post([FromBody] EndSessionRequest req)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   
                    SessionResponse bidResponse = PostEndToBidder(req);
                    if (bidResponse.result== HttpStatusCode.OK)
                    {
                        bool endSessionSucceed = _service.EndSession(req.session_id);
                        if (endSessionSucceed) 
                        {

                            return Ok();
                        }
                        else
                        {
                            return BadRequest($"Fail to end this session: {req.session_id}. This session_id does not exist.");
                        }
                    }
                    else
                    {
                        return BadRequest($"Fail to end this session: {req.session_id}. {bidResponse.error}");

                    }
                }
                catch (Exception ex)
                {
                    return BadRequest($"Fail to end this session: {req.session_id}, {ex.Message}");
                }
            }
            else
            {
                return BadRequest("The request is invalid.");
            }

        }

        [Route("~/session_id")]
        // GET /session_id
        [HttpGet]
        public Dictionary<string, SessionData> Get()
        {
            return _service.GetCurrentSessions();
        }


        private SessionResponse PostInitToBidder(InitSessionRequest postData)
        {
            var url = postData.bidders.FirstOrDefault().endpoint + "init_session";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "POST";

            httpRequest.Accept = "application/json";

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(JsonConvert.SerializeObject(postData));
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseFromServer = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<SessionResponse>(responseFromServer);

            }
        }

        private SessionResponse PostEndToBidder(EndSessionRequest postData)
        {
            var url = _service.GetBidderEndpoint(postData.session_id) + "end_session";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "POST";

            httpRequest.Accept = "application/json";

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(JsonConvert.SerializeObject(postData));
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseFromServer = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<SessionResponse>(responseFromServer);
            }

        }

    }
}
