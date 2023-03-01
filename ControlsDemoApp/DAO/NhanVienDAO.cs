using ControlsDemoApp.DTO;
using System.Collections.Generic;
using System.Data;

namespace ControlsDemoApp.DAO
{
    public class NhanVienDAO
    {
        private static NhanVienDAO instance;

        public static NhanVienDAO Instance
        {
            get { if (instance == null) instance = new NhanVienDAO(); return instance; }
            private set { instance = value; }
        }

        private NhanVienDAO() { }

        public List<NhanVien> GetAllEmployee()
        {
            List<NhanVien> listEmps = new List<NhanVien>();

            string query = "Select * from NhanVien";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                NhanVien nhanVien = new NhanVien(item);
                listEmps.Add(nhanVien);
            }

            return listEmps;
        }

        public bool InsertEmployee(string maNV, string tenNV, string diaChi, string queQuan, string gioiTinh)
        {
            string query = string.Format("INSERT dbo.NhanVien ( MaNV , TenNV , DiaChi , QueQuan , GioiTinh )VALUES  ( N'{0}', N'{1}', N'{2}', N'{3}', N'{4}')", maNV, tenNV, diaChi, queQuan, gioiTinh);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool UpdateEmployee(string tenNV, string diaChi, string queQuan, string gioiTinh, string maNV)
        {
            string query = string.Format("UPDATE dbo.NhanVien SET TenNV = N'{0}', DiaChi = N'{1}', QueQuan = N'{2}', GioiTinh = N'{3}' WHERE MaNV = N'{4}'", tenNV, diaChi, queQuan, gioiTinh, maNV);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool DeleteEmployee(string maNV)
        {
            string query = string.Format("Delete dbo.NhanVien where MaNV = N'{0}'", maNV);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public List<NhanVien> SearchEmployee(string tenNV)
        {
            List<NhanVien> list = new List<NhanVien>();

            string query = string.Format("SELECT * FROM dbo.NhanVien WHERE TenNV LIKE N'%{0}%'", tenNV);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                NhanVien nhanVien = new NhanVien(item);
                list.Add(nhanVien);
            }

            return list;
        }
    }
}
