using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Mục đích lấy tên bảng trong Database là để xử lý việc thêm/sửa khi thực hiện
/*
Cách thực hiện:
    + Khi ấn nút thêm/sửa thì sẽ lưu tên bảng thông qua tableName
    + Dựa vào tên bảng (tableName) để load dữ liệu phù hợp cho form Add_Update_Data
 */

namespace QL_NhaHang
{
    internal class TableName
    {
        public static string tableName = "";
    }
}
