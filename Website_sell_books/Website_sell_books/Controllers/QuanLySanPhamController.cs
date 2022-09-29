using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website_sell_books.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace Website_sell_books.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        // GET: QuanLySanPham
        QuanLyBanSachModel db = new QuanLyBanSachModel();

        public ActionResult Index( int? page)
        {
            int pageSize = 6;
            int pageNumber = (page ?? 1); // Số trang là page, nếu page == null thì return về 1

            return View(db.Saches.ToList().OrderBy(n=>n.MaSach).ToPagedList(pageNumber,pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            //Đưa dữ liệu vào dropdownlist
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList(), "MaNXB", "TenNXB");

            return View();
        }
        
        [HttpPost]
        public ActionResult Create(Sach sach, HttpPostedFileBase fileUpload)
        {
            
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList(), "MaNXB", "TenNXB");
            

            //Thêm sách vào csdl
            if (ModelState.IsValid)
            {
                // Lưu tên của file ảnh vào database
                var fileName = Path.GetFileName(fileUpload.FileName);
                //Lưu path của file
                var path = Path.Combine(Server.MapPath("~/wwwroot/images/books/"), fileName);
                //Kiểm tra ảnh đã tồn tại bằng tên
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Tên file ảnh đã tồn tại";
                }
                else
                {
                    fileUpload.SaveAs(path);
                }
                sach.AnhBia = fileUpload.FileName;
                // Kiểm tra đường dẫn ảnh bìa
                
                db.Saches.Add(sach);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }
    }
}