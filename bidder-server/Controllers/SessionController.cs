using bidder_server.Models.BidModel;
using bidder_server.Models.CommonModel;
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
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(HttpErrorMessage))]
        [HttpPost]
        public IActionResult Post([FromBody] SessionRequest req)
        {
            if (ModelState.IsValid)
            {
                
                if(_service.AddSession(req.session_id, req))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("The request is invalid. Session_id must be unique");
                }

            }
            else
            {
                return  BadRequest("The request is invalid.");
            }
        }

        // POST api/<SessionController>/end_session
        [Route("~/end_session")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(HttpErrorMessage))]
        [HttpPost]
        public IActionResult Post([FromBody] EndSessionRequest req)
        {
            if (ModelState.IsValid)
            {
                if (_service.EndSession(req.session_id))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("This session_id is no longer exist.");
                }
            }
            else
            {
                return BadRequest("The request is invalid.");

            }

        }
    }
}
