using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Mục đích 'đăng nhập dạng public' là để xử lý các trường hợp mở nhiều tabs cùng đăng nhập
/*
Cách xử lý:
    + Trong Admin hoặc User khi người dùng đăng nhập cùng tài khoản và bên thứ 1 thoát chương trình
    + Phần phiên đăng nhập (UserSession) sẽ bị xoá sạch cho cùng tài khoản đăng nhập
    + Đợi 5s để tự đăng xuất --> trở lại trang Login
*/
namespace QL_NhaHang
{
    internal class DangNhap
    {
        public static string dangNhap;
        public static string matKhau;
    }
}
