using bidder_server.Models.BidModel;
using bidder_server.Models.CommonModel;
using bidder_server.Models.WinBidModel;
using bidder_server.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace bidder_server.Controllers
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

       
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(BidResponse))]
        [ProducesResponseType(400, Type = typeof(HttpErrorMessage))]
        [Route("~/bid_request")]// POST api/<BidController>
        public IActionResult Post([FromBody] BidRequest req)
        {
            try
            {
                if (ModelState.IsValid) {
                    return Ok(_service.GetBidPrice(req));
                }
                else
                {
                    return BadRequest("The Model is Invalidate.");
                }

            }
            catch(Exception ex)
            {
                HttpErrorMessage httpErrorMessage = new HttpErrorMessage();
                httpErrorMessage.error = ex.Message;
                return BadRequest(httpErrorMessage);

            }

        }
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(HttpErrorMessage))]
        [Route("~/notify_win_bid")]// POST api/<BidController>
        [HttpPost]
        public IActionResult Post([FromBody] NotifyWinBidRequest req)
        {
            try
            {
                if (_service.WinNotify(req)){
                    return Ok();
                }
                else
                {
                    return BadRequest("The bidder doesn't have suffcient budget.");

                }

            }
            catch (Exception ex)
            {
                HttpErrorMessage httpErrorMessage = new HttpErrorMessage();
                httpErrorMessage.error = ex.Message;
                return BadRequest(httpErrorMessage);

            }

        }


    }
}
