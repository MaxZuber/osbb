using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XCL.Core.Services.Abstract;

namespace XCL.WebUI.Controllers
{
    public class ValuesController : ApiController
    {

        private readonly IRandomValuesService _randomValuesService;
        private readonly IEntranceService entranceService;

        public ValuesController(IRandomValuesService randomValuesService, IEntranceService entranceService)
        {
            _randomValuesService = randomValuesService;
            this.entranceService = entranceService;
        }

        // GET api/values
        public HttpResponseMessage Get(DateTime date, int? entranceId = null)
        {
            return Request.CreateResponse(HttpStatusCode.OK, entranceService.GetEntranceSensorValues(date, entranceId));
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
