using bidder_server.Models.BidModel;
using bidder_server.Models.SessionModel;
using bidder_server.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace bidder_server.Controllers
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
        // POST api/<SessionController>
        [Route("~/init_session")]
        [HttpPost]
        public SessionResponse Post(SessionRequest req)
        {
            if (ModelState.IsValid)
            {
                
                if(_service.AddSession(req.session_id, req))
                {
                    return new SessionResponse() { result = HttpStatusCode.OK };
                }
                else
                {
                    return new SessionResponse() { result = HttpStatusCode.BadRequest, error = "The request is invalid. Session_id must be unique" };
                }

            }
            else
            {
                return new SessionResponse() { result = HttpStatusCode.BadRequest, error = "The request is invalid." };
            }
        }

        [Route("~/end_session")]
        // POST api/<SessionController>/end_session
        [HttpPost]
        public SessionResponse Post(EndSessionRequest req)
        {
            if (ModelState.IsValid)
            {
                if (_service.EndSession(req.session_id))
                {
                    return new SessionResponse() { result = HttpStatusCode.OK };
                }
                else
                {
                    return new SessionResponse() { result = HttpStatusCode.BadRequest, error = "This session_id is no longer exist." };
                }
            }
            else
            {
                return new SessionResponse() { result = HttpStatusCode.BadRequest, error = "The request is invalid." };

            }

        }
    }
}
