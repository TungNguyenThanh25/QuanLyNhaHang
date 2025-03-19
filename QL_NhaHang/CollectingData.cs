using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Thu nhập toàn bộ dữ liệu trên 1 dòng từ một bảng cụ thể
/*
VD: bảng KhachHang(MaKH, TenKH, GioiTinh, SDT, Email)
    + txt_box1 lấy dữ liệu MaKH
    + txt_box2 lấy dữ liệu TenKH
    + txt_box3 lấy dữ liệu GioiTinh
    + txt_box4 lấy dữ liệu SDT
    + txt_box5 lấy dữ liệu Email
    + txt_box6 ko có gì hết
 */
namespace QL_NhaHang
{
    internal class CollectingData
    {
        public static string txt_box1 = ""; // Thường chứa các khoá chính
        public static string txt_box2 = "";
        public static string txt_box3 = "";
        public static string txt_box4 = "";
        public static string txt_box5 = "";
        public static string txt_box6 = "";
    }
}
