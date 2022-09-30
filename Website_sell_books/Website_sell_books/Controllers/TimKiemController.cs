using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using Website_sell_books.Models;

namespace Website_sell_books.Controllers
{
    public class TimKiemController : Controller
    {
        // GET: TimKiem
        QuanLyBanSachModel db =new  QuanLyBanSachModel();

        [HttpPost]
        public ActionResult KetQuaTK(FormCollection f, int? page)
        {
            string sTuKhoa = f["txttimkiem"].ToString();
            ViewBag.TuKhoa = sTuKhoa;
            List<Sach> KQTK = db.Saches.Where(n => n.TenSach.Contains(sTuKhoa)).ToList();

            var pageNumber = (page ?? 1);
            var pageSize = 12;

            if (KQTK.Count <= 0)
            {
                ViewBag.TB = "Không tìm thấy sản phẩm nào";
                return View(db.Saches.OrderBy(n => n.MaSach).ToPagedList(pageNumber, pageSize));
            }
            ViewBag.TB = "Tìm thấy " + KQTK.Count + " cuốn sách phù hợp";

            return View(KQTK.OrderBy(n => n.MaSach).ToPagedList(pageNumber, pageSize));
        }
        

        [HttpGet]
        public ActionResult KetQuaTK(int? page, string sTuKhoa)
        {
            ViewBag.TuKhoa = sTuKhoa;

            List<Sach> KQTK = db.Saches.Where(n => n.TenSach.Contains(sTuKhoa)).ToList();

            var pageNumber = (page ?? 1);
            var pageSize = 12;

            if (KQTK.Count == 0)
            {
                ViewBag.TB = "Không tìm thấy sản phẩm nào";
                return View(db.Saches.OrderBy(n => n.MaSach).ToPagedList(pageNumber, pageSize));
            }
            ViewBag.TB = "Tìm thấy " + KQTK.Count + " cuốn sách phù hợp";
            return View(KQTK.OrderBy(n => n.MaSach).ToPagedList(pageNumber, pageSize));
        }
        
    }
}