using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Cookbook.Filters;

namespace Cookbook.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class FoodController : Controller
    {
        //
        // GET: /Food/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            try
            {
                if (id == null || ViewData["MaMonAn"] != null)
                {
                    id = int.Parse(ViewData["MaMonAn"].ToString());
                }
                WebClient client = new WebClient();
                var json = client.DownloadString("http://cookbookapi.apphb.com/api/MonAn/LayMonAn/" + id.ToString());
                json = System.Text.Encoding.Unicode.GetString(System.Text.Encoding.Convert(System.Text.Encoding.UTF8, System.Text.Encoding.Unicode, System.Text.Encoding.Default.GetBytes(json)));
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                dynamic model = serializer.Deserialize<dynamic>(json);

                ViewData["MaMonAn"] = model["MaMonAn"];
                ViewData["TenMon"] = model["TenMon"];
                ViewData["GioiThieu"] = model["GioiThieu"];
                ViewData["Hinh"] = model["Hinh"];
                ViewData["ThoiGianChuanBi"] = model["ThoiGianChuanBi"];
                ViewData["ThoiGianNau"] = model["ThoiGianNau"];
                ViewData["NgayDang"] = model["NgayDang"];
                ViewData["NguyenLieu"] = model["NguyenLieu"];
                ViewData["CachLam"] = model["CachLam"];
                ViewData["MaNguoiDung"] = model["MaNguoiDung"];
                ViewData["MaMucDo"] = model["MaMucDo"];
                ViewData["MaLoaiMon"] = model["MaLoaiMon"];
            }
            catch { }
            return View();
        }

        [HttpPost]
        public ActionResult POST(HttpPostedFileBase file)
        {

            //Upload hinh
            Account account = new Account("hl6ei8egh", "132439745671385", "f7bunvOJ6Fg6Bqynodcx5TRQfaY");
            Cloudinary cloudinary = new Cloudinary(account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.InputStream)
            };

            var uploadResult = cloudinary.Upload(uploadParams);  // cho nay no uphinh SYNC ko co async nên up xong nó cho cái link hinh ben dưới

            ViewData["Hinh"] = uploadResult.Uri.AbsoluteUri; // luu vao database <- đem cái String hinh nay lưu vào DB

            return View("Index");
        }

        [HttpPost]
        public ActionResult POSTEDIT(int id, HttpPostedFileBase file)
        {

            //Upload hinh
            Account account = new Account("hl6ei8egh", "132439745671385", "f7bunvOJ6Fg6Bqynodcx5TRQfaY");
            Cloudinary cloudinary = new Cloudinary(account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.InputStream)
            };

            var uploadResult = cloudinary.Upload(uploadParams);  // cho nay no uphinh SYNC ko co async nên up xong nó cho cái link hinh ben dưới

            ViewData["Hinh"] = uploadResult.Uri.AbsoluteUri; // luu vao database <- đem cái String hinh nay lưu vào DB

          //  ViewData["MaMonAn"] = id;
            WebClient client = new WebClient();
            var json = client.DownloadString("http://cookbookapi.apphb.com/api/MonAn/LayMonAn/" + id.ToString());
            json = System.Text.Encoding.Unicode.GetString(System.Text.Encoding.Convert(System.Text.Encoding.UTF8, System.Text.Encoding.Unicode, System.Text.Encoding.Default.GetBytes(json)));
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            dynamic model = serializer.Deserialize<dynamic>(json);

            ViewData["MaMonAn"] = model["MaMonAn"];
            ViewData["TenMon"] = model["TenMon"];
            ViewData["GioiThieu"] = model["GioiThieu"];
           // ViewData["Hinh"] = model["Hinh"];
            ViewData["ThoiGianChuanBi"] = model["ThoiGianChuanBi"];
            ViewData["ThoiGianNau"] = model["ThoiGianNau"];
            ViewData["NgayDang"] = model["NgayDang"];
            ViewData["NguyenLieu"] = model["NguyenLieu"];
            ViewData["CachLam"] = model["CachLam"];
            ViewData["MaNguoiDung"] = model["MaNguoiDung"];
            ViewData["MaMucDo"] = model["MaMucDo"];
            ViewData["MaLoaiMon"] = model["MaLoaiMon"];

            return View("Edit");
        }
    }
}
