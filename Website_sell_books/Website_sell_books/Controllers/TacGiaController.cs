using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website_sell_books.Models;

namespace Website_sell_books.Controllers
{
    public class TacGiaController : Controller
    {
        // GET: TacGia
        QuanLyBanSachModel db = new QuanLyBanSachModel();
        public PartialViewResult _TacGiaNoiTiengPartial()
        {
            //Đưa ra những tác giả có số lượng sách xuất bản nhiều nhất
            var query = db.TacGias.Take(4).ToList();
            return PartialView(query);
        }
    }
}