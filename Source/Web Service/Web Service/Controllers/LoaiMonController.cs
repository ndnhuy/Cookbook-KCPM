using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_Service.Models;

namespace Web_Service.Controllers
{
    public class LoaiMonController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<LoaiMon> Get()
        {
            List<LoaiMon> result = new List<LoaiMon>();
            using (var db = new FoodLoverContainer())
            {
                var query = from lm in db.LoaiMon
                            orderby lm.TenLoaiMon
                            select lm;
                foreach (var q in query)
                {
                    result.Add((LoaiMon)q);
                }
            }
            return result;
            //this
        }

        private object LoaiMon()
        {
            throw new NotImplementedException();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}