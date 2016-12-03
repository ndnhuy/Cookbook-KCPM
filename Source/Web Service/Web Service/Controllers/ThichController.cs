using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Web_Service.Models;

namespace Web_Service.Controllers
{
    public class ThichController : ApiController
    {
        private FoodLoverContainer db = new FoodLoverContainer();

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("Exists")]
        public String Exists(string id)
        {
            string[] splits = id.Split('-');

            string UserId = splits[0];
            int FoodId = int.Parse(splits[1]);

            var results = db.Thich.Where(o => o.MaNguoiDung == UserId).Where(o => o.MaMonAn == FoodId);
            return results.Count() == 0 ? "False" : "True";
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("Add")]
        public String Add(string id)
        {
            string[] splits = id.Split('-');

            string UserId = splits[0];
            int FoodId = int.Parse(splits[1]);

            var results = db.Thich.Where(o => o.MaNguoiDung == UserId).Where(o => o.MaMonAn == FoodId);

            try
            {
                if (results.Count() != 0)
                {
                    return results.Count().ToString();
                }

                Thich like = new Thich();
                like.MaMonAn = FoodId;
                like.MaNguoiDung = UserId;

                db.Thich.Add(like);
                db.SaveChanges();

                return (results.Count()).ToString();
            }
            catch
            {
                return results.Count().ToString();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ThichExists(string id)
        {
            return db.Thich.Count(e => e.MaNguoiDung == id) > 0;
        }
    }
}