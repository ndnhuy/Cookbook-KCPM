using HtmlAgilityPack;
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
    public class RutTrichController : ApiController
    {
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        public List<MonAn> RutTrichThongTin()
        {
            List<MonAn> Data = new List<MonAn>();
            List<String> Href = new List<string>();

            var webSite = new HtmlWeb();
            var doc = webSite.Load("http://www.knorr.com.vn/recipes/canh/297426");
            var metaTags = doc.DocumentNode.SelectNodes("//div[@class='c601 recipe-search-results']//div[@class='viewRecipeList']//a[@ct-type='recipeInfo']");
            int dem = 0;
            if (metaTags != null)
            {
                foreach (var tag in metaTags)
                {
                    if (dem % 2 == 0)
                        if (tag.Attributes["href"] != null)
                            Href.Add(@"http://www.knorr.com.vn" + tag.Attributes["href"].Value);
                    dem++;
                }
            }

            foreach (string href in Href)
            {
                MonAn MA = new MonAn();

                HtmlWeb Web = new HtmlWeb();
                HtmlDocument HtmlDoc = Web.Load(href);

                // Hình ảnh
                var HinhAnh = HtmlDoc.DocumentNode.SelectNodes("//div[@class='recipe-content-header']//div[@class='image']//img[@itemprop='image']");
                if (HinhAnh != null)
                {
                    foreach (var tag in HinhAnh)
                    {
                        if (tag.Attributes["src"] != null)
                            MA.Hinh = tag.Attributes["src"].Value;
                    }
                }

                // Món ăn
                IEnumerable<HtmlNode> nodes = HtmlDoc.DocumentNode.SelectNodes("//div[@class='recipe-content-header']//h1[@itemprop='name']");
                foreach (HtmlNode node in nodes)
                {
                    MA.TenMon = node.InnerText;
                }

                // Nguyên liệu
                nodes = HtmlDoc.DocumentNode.SelectNodes("//ul[@class='recipe-ingredients-list']//li[@itemprop='ingredients']");
                string nl = "";
                foreach (HtmlNode node in nodes)
                {
                    nl += node.InnerText + "\n";
                }
                MA.NguyenLieu = nl;
         
                // Cách làm
                nodes = HtmlDoc.DocumentNode.SelectNodes("//ul[@class='directions']//li");
                string th = "";
                foreach (HtmlNode node in nodes)
                {
                    th += node.InnerText + "\n";
                }
                MA.CachLam = th;
                Data.Add(MA);
            }
            return Data;
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        [ActionName("RutTrichMonAn")]
        public List<MonAn> RutTrichMonAn(string id)
        {
            List<MonAn> lstMonAn = RutTrichThongTin();
            return lstMonAn;
        }
    }
}
