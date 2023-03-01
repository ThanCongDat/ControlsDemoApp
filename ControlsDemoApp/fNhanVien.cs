using ControlsDemoApp.DAO;
using ControlsDemoApp.DTO;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace ControlsDemoApp
{
    public partial class fNhanVien : Form
    {
        BindingSource listEmployee = new BindingSource();
        public fNhanVien()
        {
            InitializeComponent();
            LoadData();
        }

        void LoadData()
        {
            dataGridView1.DataSource = listEmployee;

            LoadListEmployee();
            EmployeeBinding();
        }
        List<NhanVien> SearchEmployee(string tenNV)
        {
            List<NhanVien> listEmp = NhanVienDAO.Instance.SearchEmployee(tenNV);

            return listEmp;
        }
        void LoadListEmployee()
        {
            listEmployee.DataSource = NhanVienDAO.Instance.GetAllEmployee();
        }

        void EmployeeBinding()
        {
            txt_MaNV.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "MaNV", true, DataSourceUpdateMode.Never));
            txt_TenNV.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "TenNV", true, DataSourceUpdateMode.Never));
            txt_DiaChi.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "DiaChi", true, DataSourceUpdateMode.Never));
            txt_QueQuan.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "QueQuan", true, DataSourceUpdateMode.Never));
            txt_GioiTinh.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "GioiTinh", true, DataSourceUpdateMode.Never));
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            string maNV = txt_MaNV.Text;
            string tenNV = txt_TenNV.Text;
            string diaChi = txt_DiaChi.Text;
            string queQuan = txt_QueQuan.Text;
            string gioiTinh = txt_GioiTinh.Text;

            if(NhanVienDAO.Instance.InsertEmployee(maNV, tenNV, diaChi, queQuan, gioiTinh))
            {
                MessageBox.Show("Thêm nhân viên thành công!");
                LoadListEmployee();
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm nhân viên!");
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            string maNV = txt_MaNV.Text;
            string tenNV = txt_TenNV.Text;
            string diaChi = txt_DiaChi.Text;
            string queQuan = txt_QueQuan.Text;
            string gioiTinh = txt_GioiTinh.Text;

            if (NhanVienDAO.Instance.UpdateEmployee(tenNV, diaChi, queQuan, gioiTinh, maNV))
            {
                MessageBox.Show("Sửa nhân viên thành công!");
                LoadListEmployee();
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa nhân viên!");
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            string maNV = txt_MaNV.Text;

            if (NhanVienDAO.Instance.DeleteEmployee(maNV))
            {
                MessageBox.Show("Xóa nhân viên thành công!");
                LoadListEmployee();
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa nhân viên!");
            }
        }
        private void ImportExcel(string path)
        {
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(path)))
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets[0];
                DataTable dataTable = new DataTable();
                for(int i = excelWorksheet.Dimension.Start.Column; i <= excelWorksheet.Dimension.End.Column; i++)
                {
                    dataTable.Columns.Add(excelWorksheet.Cells[1, i].Value.ToString());
                }
                for(int i = excelWorksheet.Dimension.Start.Row + 1; i <= excelWorksheet.Dimension.End.Row; i++)
                {
                    List<string> listRows = new List<string>();
                    for(int j = excelWorksheet.Dimension.Start.Column; j <= excelWorksheet.Dimension.End.Column; j++)
                    {
                        listRows.Add(excelWorksheet.Cells[i, j].Value.ToString());
                    }
                    dataTable.Rows.Add(listRows.ToArray());
                }
                dataGridView1.DataSource = dataTable;
                LoadListEmployee();
            }
        }
        private void btn_Import_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Import Excel";
            openFileDialog.Filter = "Excel (*.xlsx|*.xlsx|Excel 2003 (*.xlsx)|*.xls)";
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ImportExcel(openFileDialog.FileName);
                    MessageBox.Show("Nhập file thành công");
                    LoadListEmployee();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Nhập file không thành công!\n" + ex.Message);
                }
            }
        }

        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            listEmployee.DataSource = SearchEmployee(txt_TimKiem.Text);
        }
    }
}
