using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website_sell_books.Models;

namespace Website_sell_books.Controllers
{
    public class NguoiDungController : Controller
    {
        QuanLyBanSachModel db = new QuanLyBanSachModel();

        // GET: NguoiDung
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy( KhachHang kh)
        {
            //Sau khi chạy lệnh add thì nó mới chỉ lưu vào model KhachHang được ánh xạ từ csdl sang thôi
            if (ModelState.IsValid)
            {
                db.KhachHangs.Add(kh);
                // Chạy lệnh này thì mới cập nhật dữ liệu vào trong csdl
                db.SaveChanges();
                return RedirectToAction("DSSach","Sach");
            }
                return View();

        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap( FormCollection f)
        {
            string sdt = f["txtsdt"].ToString();
            string pass = f.Get("pass").ToString();
            KhachHang kh = db.KhachHangs.SingleOrDefault(n=>n.DienThoai == sdt && n.MatKhau == pass);
            if (kh != null)
            {
                Session["TaiKhoan"] = kh;

                return RedirectToAction("DSSach", "Sach");
            }
            Session["TaiKhoan"] = "tb";

            return View();
        }

    }
}