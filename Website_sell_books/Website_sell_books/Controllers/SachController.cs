using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website_sell_books.Models;
using PagedList;

namespace Website_sell_books.Controllers
{
    public class SachController : Controller
    {
        // GET: Sach
        QuanLyBanSachModel db = new QuanLyBanSachModel();
       
        public PartialViewResult DSSach(int? page)
            //biến page này là biến dùng để điều hướng đến trang bao nhiêu, ví dụ bấm vào trang 2 trang 3, thì page=2 page=3
        {
            int pageSize = 12;// Số sách trên 1 trang

            int pageNumber = (page ?? 1); // Nếu có ít hơn 12 sản phẩm thì số trang chỉ là 1, tức là cứ 12 sách 1 trang, <12 thì chỉ có 1 trang thôi

            var query = db.Saches.ToList().OrderBy(n=>n.NgayCapNhat).ToPagedList(pageNumber, pageSize);
            return PartialView(query); 
        }
        public PartialViewResult _SachXuHuongPartial()
        {
            var que = db.Saches.Take(4).Where(i => i.Moi == 1).ToList();
            return PartialView(que);
        }
        public PartialViewResult XemChiTietSach(int MaSach)
        {
            // Không bắt lỗi action này khi không tìm thấy MaSach vì mỗi quyển sách mà ấn vào xem chi tiết được thì nó phải có MaSach
            // Nên việc bắt exception null không cần thiết chăng ?
            Sach s = db.Saches.SingleOrDefault( i=> i.MaSach == MaSach);
            return PartialView(s);
        }
        public ActionResult _MenuNgangPartial()
        {
            var query3 = db.ChuDes.Take(6).ToList();
            return View(query3);
        }
    }
}