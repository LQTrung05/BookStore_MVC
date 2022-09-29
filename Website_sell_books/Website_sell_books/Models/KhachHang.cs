namespace Website_sell_books.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
       
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            DonHangs = new HashSet<DonHang>();
        }

        [Key]
        public int MaKH { get; set; }

        [StringLength(50)]
        [Display(Name ="Ho Tên")]
        [Required(ErrorMessage ="{0} khong  de trong")]
        public string HoTen { get; set; }

        [Display(Name = "Ngay sinh")]

        public DateTime? NgaySinh { get; set; }

        [StringLength(3)]
        public string GioiTinh { get; set; }

        [StringLength(50)]
        [Display(Name = "So dien thoai")]
        [Required(ErrorMessage = "{0} khong de trong")]
        public string DienThoai { get; set; }

        [StringLength(50)]
        public string TaiKhoan { get; set; }

        [StringLength(50)]
        [Display(Name = "Mat khau")]
        [Required(ErrorMessage = "{0} khong de trong")]

        public string MatKhau { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "{0} khong de trong")]

        public string Email { get; set; }

        [StringLength(10)]
        [Display(Name = "Dia chi")]
        [Required(ErrorMessage = "{0} khong de trong")]

        public string DiaChi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonHang> DonHangs { get; set; }
    }
}
