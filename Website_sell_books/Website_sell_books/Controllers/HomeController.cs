using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website_sell_books.Models;
namespace Website_sell_books.Controllers
{
    public class HomeController : Controller
    {
        QuanLyBanSachModel db = new QuanLyBanSachModel();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        // PartialViewResult chính là class con của ActionResult, Khi không biết return về gì thì dùng chung ActionResult
       
       
    }
}