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
        public SessionController( SessionService service)
        {
            _service = service;
        }
        [Route("~/init_session")]
        // POST /init_session
        [HttpPost]
        public SessionResponse Post(InitSessionRequest req)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _service.AddSession(req.session_id);
                    return PostInitToBidder(req);
                }catch(Exception ex)
                {
                    return new SessionResponse() { result = HttpStatusCode.BadRequest, error = $"session_id: {req.session_id}, {ex.Message}" };
                }
                
            }
            else
            {
                return new SessionResponse() { result = HttpStatusCode.BadRequest, error= "The request is invalid." };
            }
           

        }

        [Route("~/end_session")]
        // POST /end_session
        [HttpPost]
        public SessionResponse Post(EndSessionRequest req)
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

                            return new SessionResponse() { result = HttpStatusCode.OK };
                        }
                        else
                        {
                            return new SessionResponse() { result = HttpStatusCode.BadRequest, error = $"Fail to end this session: {req.session_id}. This session_id is no longer exist." };
                        }
                    }
                    else
                    {
                        return new SessionResponse() { result = HttpStatusCode.BadRequest, error = $"Fail to end this session: {req.session_id}. {bidResponse.error}" };

                    }
                }
                catch (Exception ex)
                {
                    return new SessionResponse() { result = HttpStatusCode.BadRequest, error = $"Fail to end this session: {req.session_id}, {ex.Message}" };
                }
            }
            else
            {
                return new SessionResponse() { result = HttpStatusCode.BadRequest, error = "The request is invalid." };
            }

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
            var url = postData.endpoint + "end_session";
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
