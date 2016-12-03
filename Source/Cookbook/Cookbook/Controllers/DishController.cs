using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Cookbook.Controllers
{
    public class DishController : Controller
    {
        //
        // GET: /Dish/MaMonAn

        public ActionResult Index(string id)
        {
            dynamic model = new System.Dynamic.ExpandoObject(); ;

            try
            {
                WebClient client = new WebClient();
                var json = client.DownloadString(String.Format("http://cookbookapi.apphb.com/api/MonAn/Get/{0}", id));
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                dynamic data = serializer.Deserialize<dynamic>(json);

                model.MaMonAn = id;
                model.TenMonAn = System.Text.Encoding.Unicode.GetString(System.Text.Encoding.Convert(System.Text.Encoding.UTF8, System.Text.Encoding.Unicode, System.Text.Encoding.Default.GetBytes(data["TenMon"])));
                model.GioiThieu = System.Text.Encoding.Unicode.GetString(System.Text.Encoding.Convert(System.Text.Encoding.UTF8, System.Text.Encoding.Unicode, System.Text.Encoding.Default.GetBytes(data["GioiThieu"])));
                model.Hinh = data["Hinh"];
                model.ThoiGianNau = data["ThoiGianNau"];
                model.ThoiGianChuanBi = data["ThoiGianChuanBi"];
                model.NguyenLieu = System.Text.Encoding.Unicode.GetString(System.Text.Encoding.Convert(System.Text.Encoding.UTF8, System.Text.Encoding.Unicode, System.Text.Encoding.Default.GetBytes(data["NguyenLieu"])));
                model.CachLam = System.Text.Encoding.Unicode.GetString(System.Text.Encoding.Convert(System.Text.Encoding.UTF8, System.Text.Encoding.Unicode, System.Text.Encoding.Default.GetBytes(data["CachLam"])));
                model.TenMucDo = System.Text.Encoding.Unicode.GetString(System.Text.Encoding.Convert(System.Text.Encoding.UTF8, System.Text.Encoding.Unicode, System.Text.Encoding.Default.GetBytes(data["MucDo"]["TenMucDo"])));
                model.TenLoaiMon = System.Text.Encoding.Unicode.GetString(System.Text.Encoding.Convert(System.Text.Encoding.UTF8, System.Text.Encoding.Unicode, System.Text.Encoding.Default.GetBytes(data["LoaiMon"]["TenLoaiMon"])));
                model.NgayDang = data["NgayDang"];
                model.SoLuongThich = data["Thich"].Length;
                model.OwnerMa = data["MaNguoiDung"];
                model.OwnerHoTen = System.Text.Encoding.Unicode.GetString(System.Text.Encoding.Convert(System.Text.Encoding.UTF8, System.Text.Encoding.Unicode, System.Text.Encoding.Default.GetBytes(data["NguoiDung"]["Ho"]))) + " " + System.Text.Encoding.Unicode.GetString(System.Text.Encoding.Convert(System.Text.Encoding.UTF8, System.Text.Encoding.Unicode, System.Text.Encoding.Default.GetBytes(data["NguoiDung"]["Ten"])));
                model.OwnerHinh = data["NguoiDung"]["Hinh"];
                model.OwnerDiaChi = System.Text.Encoding.Unicode.GetString(System.Text.Encoding.Convert(System.Text.Encoding.UTF8, System.Text.Encoding.Unicode, System.Text.Encoding.Default.GetBytes(data["NguoiDung"]["DiaChi"])));
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
