using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website_sell_books.Models
{
    public class GioHang
    {
        QuanLyBanSachModel db = new QuanLyBanSachModel();
        public int MaSach { get; set; }
        public string TenSach { get; set; }
        public string MoTa { get; set; }
        public string TenNXB { get; set; }
        public string AnhBia { get; set; }
        public decimal GiaBan { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien { 
            get  { return SoLuong * GiaBan; }
        }

        //Khởi tạo 1 contructor để khi bấm vào nút Thêm vào giỏ hàng thì 1 đối tượng tự động được tạo ra
        public GioHang(int ms)
        {

            Sach s = db.Saches.Single(i => i.MaSach == ms);
            this.MaSach = ms;
            TenSach = s.TenSach;
            AnhBia = s.AnhBia;
            GiaBan =decimal.Parse(s.GiaBan.ToString());
            TenNXB = db.NhaXuatBans.Single(i => i.MaNXB == s.MaNXB).TenNXB;

            MoTa = s.MoTa;
            SoLuong = 1;
            
        }
       
       
    }
}