using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//2 thư viện này  cung cấp cho ta có thể sử dụng các validation method, ví dụ như Required[Name = ("Không để trống") ],...
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Website_sell_books.Models
	// Khi tạo metadata class thì các partial class phải nằm trong cùng 1 namespace.
{
	[MetadataTypeAttribute(typeof(SachMetadata))]
	public partial class Sach
	{
		internal sealed class SachMetadata
        {
            [Display(Name = "Mã sách")]
            public int MaSach { get; set; }

            [Display(Name = "Tên sách")]
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")] 
            public string TenSach { get; set; }

            [Display(Name = "Giá bán")]
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")]
            public Nullable<decimal> GiaBan { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")]
            [Display(Name = "Mô tả")]
            public string MoTa { get; set; }

            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]//Định dạng ngày sinh
            [Display(Name = "Ngày cập nhật")]
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")] 
            public Nullable<System.DateTime> NgayCapNhat { get; set; }


            [Display(Name = "Ảnh bìa")]
            public string AnhBia { get; set; }

            [Display(Name = "Số lượng tồn")]
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")] 
            public Nullable<int> SoLuongTon { get; set; }

            [Display(Name = "Chủ đề")]
            public Nullable<int> MaChuDe { get; set; }

            [Display(Name = "Nhà xuất bản")]
            public Nullable<int> MaNXB { get; set; }

            [Display(Name = "Mới")]
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")] 
            public Nullable<int> Moi { get; set; }
        }
	}
}
// Ta sử dụng class metadata là nhằm mục đích khi update database thì những cái validation sẽ không bị mất, nếu validate 
// dữ liệu ở bên class trong entityframework như thông thường thì khi update database những cái đó sẽ bị mất. Đây chính là lý do sử dụng class metadata