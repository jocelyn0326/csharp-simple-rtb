using exchange_server.Models.BidModel;
using exchange_server.Models.SessionModel;
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
        private const string EXCHANGEURL = "https://localhost:44352/";

        // GET: api/<BidController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BidController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BidController>
        [HttpPost]
        public BidResponse Post([FromBody] BidRequest req)
        {
            // todo:how to set expire time?
            int timeout = req.timeout_ms;
            // checkValidation
            CheckBidValid();

            return null;
        }

        private void CheckBidValid()
        {
            
        }

        
    }
}
