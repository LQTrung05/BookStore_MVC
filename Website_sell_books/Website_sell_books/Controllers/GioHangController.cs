using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website_sell_books.Models;
namespace Website_sell_books.Controllers
{
    
    public class GioHangController : Controller
    {
        // GET: GioHang
        QuanLyBanSachModel db = new QuanLyBanSachModel();
        //Lấy giỏ hàng, kiểm tra xem session có hàng chưa
        #region Giỏ hàng
        public List<GioHang> LayGioHang()
        {
            //Sử dụng keyword as để ép kiểu Session, bình thường học hay ép kiểu theo dạng
            //List<GioHang> lstGH = (List<GioHang>)Session["GioHang"]; Nếu ép kiểu thế này, khi giỏ hàng mà chưa tồn tại(khách hàng
            //chưa đặt hàng) thì sẽ bị lỗi, cách dưới đây sẽ khắc phục lỗi này
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            
            
            if (lstGioHang == null)
            {
                //Nếu Giỏ hàng chưa tồn tại, thì thực hiện khởi tạo giỏ hàng
                lstGioHang = new List<GioHang>();
                Session["GioHang"]  = lstGioHang;
            }
            //Nếu giỏ hàng đã tồn tại thì return giỏ hàng luôn
            return lstGioHang;

        }

        //Thêm sách vào giỏ hàng, khi kích nút đặt hàng thì hàm này chạy
        public ActionResult ThemGioHang(int MaSach, string strUrl)
        {

            //Lấy ra session giỏ hàng
            List<GioHang> lstGioHang = LayGioHang();
            
            //Kiểm tra xem trong session giỏ hàng đã tồn tại mặt hàng này hay chưa, nếu rồi thì chỉ tăng số lượng thôi
            GioHang sanpham = lstGioHang.Find(i=>i.MaSach == MaSach);
            if (sanpham == null)
            {
                 sanpham = new GioHang(MaSach);
                lstGioHang.Add(sanpham);
                return Redirect(strUrl);
            }
            else
            {
                sanpham.SoLuong++;
                return Redirect(strUrl);
            }

        }
        //Tính tổng số lượng sách 
        private int TongSL()
        {
            int tongSL = 0;
            List<GioHang> lstGH = Session["GioHang"] as List<GioHang>;
            if (lstGH == null)
            {
                tongSL = 0;
            }else
                tongSL = lstGH.Sum(n => n.SoLuong);

            return tongSL;
        }
        //Tính tổng tiền phải trả
        private decimal TongTien()
        {
            decimal tongtien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                tongtien = lstGioHang.Sum(n => n.ThanhTien);
            }
            return tongtien;
        }
        
        //Cập nhật giỏ hàng, sửa số lượng sách muốn mua
        public ActionResult CapNhatGioHang(int MaSach, FormCollection form)
        {
            // Nếu MaSach trên thanh Url không có trong csdl thì return null
            Sach sach = db.Saches.SingleOrDefault(j => j.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(j => j.MaSach == MaSach);
            if (sanpham != null)
            {
                // Cập nhật giỏ hàng bằng cách tăng giảm số lượng sách
                sanpham.SoLuong = int.Parse(form["txtsoluong"].ToString());
            }
            return View("GioHang");

        }
        //Xóa giỏ hàng
        public ActionResult XoaGioHang(int MaSach)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(i=>i.MaSach == MaSach);
            if (sanpham != null)
            {
                lstGioHang.Remove(sanpham);
                return RedirectToAction("GioHang");
            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("DSSach", "Sach");
            }
            return RedirectToAction("GioHang");

        }  
        //Xây dựng view trang GioHang
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("DSSach", "Sach");

            }
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }

        // Tạo một partial Giỏ hàng
        public ActionResult Icon_GH()
        {
            ViewBag.TongSL = TongSL();
            ViewBag.TongTien = TongTien();
            return PartialView();
        } 
        //Xóa từng mặt hàng ra khỏi giỏ hàng
        public ActionResult XoaSachGH(int MaSach)
        {
            //Lấy ra giỏ hàng
            List<GioHang> lstGioHang = LayGioHang();
            //Kiểm tra xem sách cần xóa có tồn tại trong giỏ hàng không 
            GioHang sach = lstGioHang.SingleOrDefault(n=>n.MaSach== MaSach);    
            if( sach != null)
            {
                lstGioHang.RemoveAll(n=>n.MaSach == MaSach);
                return RedirectToAction("GioHang");
            }    
            // Nếu xóa sách đi và giỏ hàng trống thì điều hướng về trang chủ luôn 
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("DSSach", "Sach");
            }
            return RedirectToAction("GioHang");

        }
        //Cập nhật từng mặt hàng khi thay đổi số lượng mua 
        public ActionResult CapNhatSLSach(int MaSach, FormCollection f)
        {
            //Trước tiên là lấy danh sách sách từ session
            List<GioHang> lstGH = LayGioHang();

            GioHang sach = lstGH.SingleOrDefault(n=>n.MaSach== MaSach);

            if (sach != null)
            {
                sach.SoLuong = int.Parse(f["txtSoLuong"].ToString());   
            }
            return RedirectToAction("GioHang");
        }
        #endregion

        #region Đặt hàng
        //Xây dựng 1 chức năng đặt hàng
        [HttpPost]
        public ActionResult DatHang()
        {
            //Kieemr tra xem dang nhap chua
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
                return RedirectToAction("DangNhap", "NguoiDung");
            if (Session["GioHang"] == null)
            {
                RedirectToAction("DSSach", "Sach");
            }

            //Theem don hang
            DonHang dh = new DonHang();
            List<GioHang> gh = LayGioHang();
            KhachHang kh = (KhachHang)Session["TaiKhoan"];
            
            // Đưa dữ liệu vào bảng đơn hàng
            dh.MaKH = kh.MaKH;
            dh.NgayDat = DateTime.Now;
            db.DonHangs.Add(dh);
            db.SaveChanges();

            // Trong bảng DonHang chỉ lưu mã đơn hàng và mã khách hàng, bảng chi tiết đơn hàng mới lưu số lượng sách mà khách mu
            foreach (var item in gh)
            {
                ChiTietDonHang ctdh = new ChiTietDonHang();
                ctdh.MaDonHang = dh.MaDonHang;
                ctdh.MaSach = item.MaSach;
                ctdh.SoLuong = item.SoLuong;
                ctdh.DonGia = item.GiaBan;
                db.ChiTietDonHangs.Add(ctdh);
                db.SaveChanges();
            }
            return RedirectToAction("DSSach", "Sach");
        }

        #endregion
    }
}