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
    public class MonAnController : ApiController
    {
        private FoodLoverContainer db = new FoodLoverContainer();

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("Get")]
        public MonAn Get(int id)
        {
            var results = db.MonAn.Where(o => o.MaMonAn == id).Include(o => o.MucDo).Include(o => o.LoaiMon).Include(o => o.Thich).Include(o => o.NguoiDung);
            if (results.Count() == 0)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return results.First();
        }

        // GET api/MonAn/5
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("LayMonAn")]
        public MonAn LayMonAn(int id)
        {
            MonAn monan = db.MonAn.Find(id);
            if (monan == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return monan;
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("TimKiem")]
        public IEnumerable<usp_TimKiemMonAn_Result> TimKiem(string id)
        {
            string[] splits = id.Split('-');

            int skipCount = int.Parse(splits[splits.Length - 1]) * 9;
            return db.usp_TimKiemMonAn(splits[0], skipCount, 9);
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("TopMonAnCuaDauBep")]
        public IEnumerable<usp_TopMonAnCuaDauBep_Result> TopMonAnCuaDauBep(string id)
        {
            return db.usp_TopMonAnCuaDauBep(id, 5);
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("Before")]
        public IEnumerable<MonAn> Before(string id)
        {
            return db.MonAn.OrderByDescending(o => o.NgayDang).Skip(10).Take(int.Parse(id)).Include(o => o.NguoiDung).Include(o => o.Thich);
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("New")]
        public IEnumerable<MonAn> New(string id)
        {
            return db.MonAn.OrderByDescending(o => o.NgayDang).Take(int.Parse(id)).Include(o => o.NguoiDung).Include(o => o.Thich);
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("TopLike")]
        public IEnumerable<usp_TopMonAnThich_Result> TopLike(string id)
        {
            return db.usp_TopMonAnThich(int.Parse(id));
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("UserDishes")]
        public IEnumerable<MonAn> UserDishes(string id)
        {
            return db.MonAn.Where(o => o.MaNguoiDung == id).OrderByDescending(o => o.NgayDang).Include(o => o.Thich);
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("UserWishes")]
        public IEnumerable<MonAn> UserWishes(string id)
        {
            var likes = db.Thich.Where(o => o.MaNguoiDung == id);
            var foods = db.MonAn;

            var results = from food in foods
                          from like in likes
                          where food.MaMonAn == like.MaMonAn
                          select food;

            return results.OrderByDescending(o => o.NgayDang).Include(o => o.Thich);
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("TatCaTatCa")]
        public IEnumerable<MonAn> TatCaTatCa(string id)
        {
            int skipCount = int.Parse(id) * 9;
            return db.MonAn.OrderByDescending(o => o.NgayDang).Skip(skipCount).Take(9).Include(o => o.Thich);
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("DeTatCa")]
        public IEnumerable<MonAn> DeTatCa(string id)
        {
            int skipCount = int.Parse(id) * 9;
            return db.MonAn.Where(o => o.MucDo.MaMucDo == 1).OrderByDescending(o => o.NgayDang).Skip(skipCount).Take(9).Include(o => o.Thich);
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("TrungBinhTatCa")]
        public IEnumerable<MonAn> TrungBinhTatCa(string id)
        {
            int skipCount = int.Parse(id) * 9;
            return db.MonAn.Where(o => o.MucDo.MaMucDo == 2).OrderByDescending(o => o.NgayDang).Skip(skipCount).Take(9).Include(o => o.Thich);
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("KhoTatCa")]
        public IEnumerable<MonAn> KhoTatCa(string id)
        {
            int skipCount = int.Parse(id) * 9;
            return db.MonAn.Where(o => o.MucDo.MaMucDo == 3).OrderByDescending(o => o.NgayDang).Skip(skipCount).Take(9).Include(o => o.Thich);
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("DeAnSang")]
        public IQueryable<MonAn> DeAnSang(string id)
        {
            int skipCount = int.Parse(id) * 9;
            return db.MonAn.Where(o => o.MucDo.MaMucDo == 1).Where(o => o.LoaiMon.MaLoaiMon == 1).OrderByDescending(o => o.NgayDang).Skip(skipCount).Take(9).Include(o => o.Thich);
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("DeMonChinh")]
        public IQueryable<MonAn> DeMonChinh(string id)
        {
            int skipCount = int.Parse(id) * 9;
            return db.MonAn.Where(o => o.MucDo.MaMucDo == 1).Where(o => o.LoaiMon.MaLoaiMon == 2).OrderByDescending(o => o.NgayDang).Skip(skipCount).Take(9).Include(o => o.Thich);
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("DeTrangMieng")]
        public IQueryable<MonAn> DeTrangMieng(string id)
        {
            int skipCount = int.Parse(id) * 9;
            return db.MonAn.Where(o => o.MucDo.MaMucDo == 1).Where(o => o.LoaiMon.MaLoaiMon == 3).OrderByDescending(o => o.NgayDang).Skip(skipCount).Take(9).Include(o => o.Thich);
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("TrungBinhAnSang")]
        public IQueryable<MonAn> TrungBinhAnSang(string id)
        {
            int skipCount = int.Parse(id) * 9;
            return db.MonAn.Where(o => o.MucDo.MaMucDo == 2).Where(o => o.LoaiMon.MaLoaiMon == 1).OrderByDescending(o => o.NgayDang).Skip(skipCount).Take(9).Include(o => o.Thich);
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("TrungBinhMonChinh")]
        public IQueryable<MonAn> TrungBinhMonChinh(string id)
        {
            int skipCount = int.Parse(id) * 9;
            return db.MonAn.Where(o => o.MucDo.MaMucDo == 2).Where(o => o.LoaiMon.MaLoaiMon == 2).OrderByDescending(o => o.NgayDang).Skip(skipCount).Take(9).Include(o => o.Thich);
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("TrungBinhTrangMieng")]
        public IQueryable<MonAn> TrungBinhTrangMieng(string id)
        {
            int skipCount = int.Parse(id) * 9;
            return db.MonAn.Where(o => o.MucDo.MaMucDo == 2).Where(o => o.LoaiMon.MaLoaiMon == 3).OrderByDescending(o => o.NgayDang).Skip(skipCount).Take(9).Include(o => o.Thich);
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("KhoAnSang")]
        public IQueryable<MonAn> KhoAnSang(string id)
        {
            int skipCount = int.Parse(id) * 9;
            return db.MonAn.Where(o => o.MucDo.MaMucDo == 3).Where(o => o.LoaiMon.MaLoaiMon == 1).OrderByDescending(o => o.NgayDang).Skip(skipCount).Take(9).Include(o => o.Thich);
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("KhoMonChinh")]
        public IQueryable<MonAn> KhoMonChinh(string id)
        {
            int skipCount = int.Parse(id) * 9;
            return db.MonAn.Where(o => o.MucDo.MaMucDo == 3).Where(o => o.LoaiMon.MaLoaiMon == 2).OrderByDescending(o => o.NgayDang).Skip(skipCount).Take(9).Include(o => o.Thich);
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("KhoTrangMieng")]
        public IQueryable<MonAn> KhoTrangMieng(string id)
        {
            int skipCount = int.Parse(id) * 9;
            return db.MonAn.Where(o => o.MucDo.MaMucDo == 3).Where(o => o.LoaiMon.MaLoaiMon == 3).OrderByDescending(o => o.NgayDang).Skip(skipCount).Take(9).Include(o => o.Thich);
        }

        // GET api/MonAn
        public IEnumerable<MonAn> GetMonAns()
        {
            var monan = db.MonAn.Include(m => m.NguoiDung).Include(m => m.MucDo).Include(m => m.LoaiMon);
            return monan.AsEnumerable();
        }

        // PUT api/MonAn/5
        public HttpResponseMessage PutMonAn(int id, MonAn monan)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != monan.MaMonAn)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(monan).State = EntityState.Modified;

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

        // POST api/MonAn
        public HttpResponseMessage PostMonAn(MonAn monan)
        {
            if (ModelState.IsValid)
            {
                db.MonAn.Add(monan);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, monan);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = monan.MaMonAn }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/MonAn/5
        public HttpResponseMessage DeleteMonAn(int id)
        {
            MonAn monan = db.MonAn.Find(id);
            if (monan == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.MonAn.Remove(monan);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, monan);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}