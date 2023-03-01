using System.Data;

namespace ControlsDemoApp.DTO
{
    public class NhanVien
    {
        public NhanVien(string maNV, string tenNV, string diaChi, string queQuan, string gioiTinh)
        {
            MaNV = maNV;
            TenNV = tenNV;
            DiaChi = diaChi;
            QueQuan = queQuan;
            GioiTinh = gioiTinh;
        }

        public NhanVien(DataRow row)
        {
            MaNV = row["maNV"].ToString();
            TenNV = row["tenNV"].ToString();
            DiaChi = row["diaChi"].ToString();
            QueQuan = row["queQuan"].ToString();
            GioiTinh = row["gioiTinh"].ToString();
        }

        private string maNV;
        private string tenNV;
        private string diaChi;
        private string queQuan;
        private string gioiTinh;

        public string MaNV { get => maNV; set => maNV = value; }
        public string TenNV { get => tenNV; set => tenNV = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string QueQuan { get => queQuan; set => queQuan = value; }
        public string GioiTinh { get => gioiTinh; set => gioiTinh = value; }
    }
}
