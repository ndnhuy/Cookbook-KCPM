using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Web_Service.Models;

namespace Web_Service.Controllers
{
    public class BinhLuanController : ApiController
    {
        private FoodLoverContainer db = new FoodLoverContainer();

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("GetCommentsByFoodId")]
        public IEnumerable<usp_LietKeBinhLuanMonAn_Result> GetCommentsByFoodId(string id)
        {
            return db.usp_LietKeBinhLuanMonAn(int.Parse(id)).AsEnumerable();
        }

        // GET api/BinhLuan
        public IEnumerable<BinhLuan> GetBinhLuans()
        {
            return db.BinhLuan.AsEnumerable();
        }

        // POST api/BinhLuan
        public HttpResponseMessage PostBinhLuan(BinhLuan binhluan)
        {
            binhluan.NgayDang = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.BinhLuan.Add(binhluan);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, binhluan);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = binhluan.MaBinhLuan }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}