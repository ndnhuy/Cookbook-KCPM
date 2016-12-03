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
    public class NguoiDungController : ApiController
    {
        private FoodLoverContainer db = new FoodLoverContainer();

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("Get")]
        public NguoiDung Get(string id)
        {
            NguoiDung nguoidung = db.NguoiDung.Find(id);
            if (nguoidung == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return nguoidung;
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("TimKiem")]
        public IEnumerable<usp_TimKiemNguoiDung_Result> TimKiem(string id)
        {
            string[] splits = id.Split('-');

            int skipCount = int.Parse(splits[splits.Length - 1]) * 9;
            return db.usp_TimKiemNguoiDung(splits[0], skipCount, 9);
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("Superhero")]
        public usp_TopDauBep_Result Superhero(string id)
        {
            return db.usp_TopDauBep().AsEnumerable().First();
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("Page")]
        public IQueryable<NguoiDung> GetPageNguoiDungs(string id)
        {
            int skipCount = int.Parse(id) * 10;
            return db.NguoiDung.OrderBy(o => o.Ten).Skip(skipCount).Take(10);
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("Profile")]
        public IEnumerable<NguoiDung> GetProfile(string id)
        {
            return db.NguoiDung.Where(o => o.MaNguoiDung == id);
        }

        // GET api/NguoiDung
        public IEnumerable<NguoiDung> GetNguoiDungs()
        {
            return db.NguoiDung.AsEnumerable();
        }

        // PUT api/NguoiDung/5
        public HttpResponseMessage PutNguoiDung(string id, NguoiDung nguoidung)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != nguoidung.MaNguoiDung)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(nguoidung).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/NguoiDung
        public HttpResponseMessage PostNguoiDung(NguoiDung nguoidung)
        {
            if (ModelState.IsValid)
            {
                var results = db.NguoiDung.Where(o => o.MaNguoiDung == nguoidung.MaNguoiDung);
                if (results.Count() != 0)
                {
                    var user = results.First();
                    user.Ho = nguoidung.Ho;
                    user.Ten = nguoidung.Ten;
                    user.Hinh = nguoidung.Hinh;
                    user.DiaChi = nguoidung.DiaChi;
                }
                else
                {
                    db.NguoiDung.Add(nguoidung);
                }

                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, nguoidung);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = nguoidung.MaNguoiDung }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/NguoiDung/5
        public HttpResponseMessage DeleteNguoiDung(string id)
        {
            NguoiDung nguoidung = db.NguoiDung.Find(id);
            if (nguoidung == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.NguoiDung.Remove(nguoidung);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, nguoidung);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}