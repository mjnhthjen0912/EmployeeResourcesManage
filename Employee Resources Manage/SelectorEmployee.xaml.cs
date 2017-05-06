﻿using Dragablz;
using Dragablz.Dockablz;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DAO;
using BUS;
using System.ComponentModel;
using System.Windows.Controls.Primitives;

namespace Employee_Resources_Manage
{
    /// <summary>
    /// Interaction logic for SelectorEmployee.xaml
    /// </summary>

    public class BoPhan
    {
        public string Name { get; set; }
        public string ID { get; set; }
    }
    public class PhongBan
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string MaBP { get; set; }
    }
    public class LoaiHopDong
    {
        public string Name { get; set; }
        public string ID { get; set; }
    }
    public class LoaiLuong
    {
        public string Name { get; set; }
        public string ID { get; set; }
    }
    public class TinhTrang
    {
        public string Name { get; set; }
        public string ID { get; set; }
    }

    public partial class SelectorEmployee : UserControl
    {
        DataTable Table;
        List<BoPhan> listBP;
        List<PhongBan> listPB;
        List<LoaiHopDong> listLHD;
        List<LoaiLuong> listLL;
        List<TinhTrang> listTT;
        string maBP = "";
        string maPB = "";
        string maLHD = "";
        string maLL = "";
        string maTT = "";
        bool isOkay = false;

        public SelectorEmployee()
        {
            InitializeComponent();
            DataTable tbTemp = BUS.NhanVienBUS.GetNhanVienForChoose();
            dataGridChoose.DataContext = tbTemp;
            for (int i = 0; i < 2; i++)
            {
                MaterialDataGridTextColumn col = new MaterialDataGridTextColumn();
                col.Header = tbTemp.Columns[i].ColumnName;
                col.EditingElementStyle = (Style)FindResource("MaterialDesignDataGridTextColumnPopupEditingStyle");
                col.Binding = new Binding(tbTemp.Columns[i].ColumnName);
                dataGridChoose.Columns.Add(col);
            }
            tbTemp = BUS.BoPhanBUS.GetBoPhan();
            listBP = new List<BoPhan>();
            listBP.Add(new BoPhan { Name = "Tất cả", ID = "0" });
            foreach (DataRow row in tbTemp.Rows)
            {
                listBP.Add(new BoPhan { ID = row[0].ToString(), Name = row[1].ToString() });
            }
            cbBoPhan.ItemsSource = listBP;
            cbBoPhan.DisplayMemberPath = "Name";
            cbBoPhan.SelectedValuePath = "ID";
            //cbBoPhan.SelectedValue = "0";

            tbTemp = BUS.PhongBanBUS.GetPhongBan();
            listPB = new List<PhongBan>();
            listPB.Add(new PhongBan { ID = "0", Name = "Tất cả", MaBP = "" });
            foreach (DataRow row in tbTemp.Rows)
            {
                listPB.Add(new PhongBan { ID = row[0].ToString(), Name = row[1].ToString(), MaBP = row[2].ToString() });
            }
            cbPhongBan.ItemsSource = listPB;
            cbPhongBan.DisplayMemberPath = "Name";
            cbPhongBan.SelectedValuePath = "ID";
            //cbPhongBan.SelectedValue = "0";

            tbTemp = BUS.LoaiHopDongBUS.GetLoaiHopDong();
            listLHD = new List<LoaiHopDong>();
            listLHD.Add(new LoaiHopDong { ID = "0", Name = "Tất cả" });
            foreach (DataRow row in tbTemp.Rows)
            {
                listLHD.Add(new LoaiHopDong { ID = row[0].ToString(), Name = row[1].ToString() });
            }
            cbLoaiHopDong.ItemsSource = listLHD;
            cbLoaiHopDong.DisplayMemberPath = "Name";
            cbLoaiHopDong.SelectedValuePath = "ID";
            //cbLoaiHopDong.SelectedValue = "0";

            tbTemp = BUS.LoaiLuongBUS.GetLoaiLuong();
            listLL = new List<LoaiLuong>();
            listLL.Add(new LoaiLuong { ID = "0", Name = "Tất cả" });
            foreach (DataRow row in tbTemp.Rows)
            {
                listLL.Add(new LoaiLuong { ID = row[0].ToString(), Name = row[1].ToString() });
            }
            cbLoaiLuong.ItemsSource = listLL;
            cbLoaiLuong.DisplayMemberPath = "Name";
            cbLoaiLuong.SelectedValuePath = "ID";
            //cbLoaiLuong.SelectedValue = "0";

            tbTemp = BUS.TinhTrangBUS.GetTinhTrang();
            listTT = new List<TinhTrang>();
            listTT.Add(new TinhTrang { ID = "0", Name = "Tất cả" });
            foreach (DataRow row in tbTemp.Rows)
            {
                listTT.Add(new TinhTrang { ID = row[0].ToString(), Name = row[1].ToString() });
            }
            cbTinhTrang.ItemsSource = listTT;
            cbTinhTrang.DisplayMemberPath = "Name";
            cbTinhTrang.SelectedValuePath = "ID";
            //cbTinhTrang.SelectedValue = "0";

            isOkay = true;
        }

        private void CreateColumnDG()
        {
            dataGridSelected.Columns.Clear();
            for (int i = 0; i < 2; i++)
            {
                MaterialDataGridTextColumn col = new MaterialDataGridTextColumn();
                Style stl = new Style();
                stl.BasedOn = (Style)FindResource("MaterialDesignDataGridColumnHeader");
                stl.TargetType = typeof(DataGridColumnHeader);
                stl.Setters.Add(new Setter(ToolTipService.ToolTipProperty, "Click để sắp xếp!"));
                col.HeaderStyle = stl;
                col.Header = Table.Columns[i].ColumnName;
                col.EditingElementStyle = (Style)FindResource("MaterialDesignDataGridTextColumnPopupEditingStyle");
                col.Binding = new Binding(Table.Columns[i].ColumnName);
                dataGridSelected.Columns.Add(col);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (MainWindow.selectedTableStatic == null)
            {
                MainWindow.selectedTableStatic = new DataTable();
                MainWindow.selectedTableStatic.Columns.Add(new DataColumn("MaNV"));
                MainWindow.selectedTableStatic.Columns.Add(new DataColumn("HoTen"));
            }
            Table = MainWindow.selectedTableStatic;
            dataGridSelected.DataContext = MainWindow.selectedTableStatic;
            CreateColumnDG();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < dataGridChoose.SelectedItems.Count; i++)
            {
                DataRowView rowSelected = (DataRowView)dataGridChoose.SelectedItems[i];
                bool contains = MainWindow.selectedTableStatic.AsEnumerable().Any(row => rowSelected[0].ToString() == row.Field<String>("MaNV"));
                if (contains == false)
                {
                    MainWindow.selectedTableStatic.ImportRow(rowSelected.Row);
                }
            }
            if (dataGridSelected.Items.Count > 0)
            {
                var border = VisualTreeHelper.GetChild(dataGridSelected, 0) as Decorator;
                if (border != null)
                {
                    var scroll = border.Child as ScrollViewer;
                    if (scroll != null) scroll.ScrollToEnd();
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            while (dataGridSelected.SelectedItem != null)
            {
                MainWindow.selectedTableStatic.Rows.Remove((dataGridSelected.SelectedItem as DataRowView).Row);
            }
        }

        private void btnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            dataGridChoose.SelectAll();
        }

        private void btnDeleteAll_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.selectedTableStatic.Rows.Count != 0)
            {
                dialogHostRefresh.DataContext = "Bạn có chắc muốn làm mới bảng đã chọn?";
                dialogHostRefresh.IsOpen = true;
            }
        }

        private void dialogHost_DialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == true)
            {
                MainWindow.selectedTableStatic.Rows.Clear();
            }
        }

        private void cbBoPhan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isOkay == true)
            {
                if (cbBoPhan.SelectedValue.ToString() == "0")
                {
                    cbPhongBan.ItemsSource = listPB;
                    //cbPhongBan.SelectedValue = "0";
                }
                else
                {
                    List<PhongBan> listPBonBP = new List<PhongBan>();
                    listPBonBP.Add(new PhongBan { ID = "0", Name = "Tất cả", MaBP = "" });
                    foreach (PhongBan row in listPB)
                    {
                        if (row.MaBP.Trim() == cbBoPhan.SelectedValue.ToString().Trim())
                            listPBonBP.Add(new PhongBan { ID = row.ID, Name = row.Name, MaBP = row.MaBP });
                    }
                    cbPhongBan.ItemsSource = listPBonBP;
                    //cbPhongBan.SelectedValue = "0";
                }
                GetForChoose();
            }
        }
        private void ComboBoxElement_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            if (isOkay == true)
            {
                GetForChoose();
            }
        }

        public void GetForChoose()
        {
            if (tgBoPhan.IsChecked == true )
            {
                if (cbBoPhan.SelectedValue != null)
                {
                    if (cbBoPhan.SelectedValue.ToString() == "0")
                    {
                        maBP = "";
                    }
                    else
                    {
                        maBP = cbBoPhan.SelectedValue.ToString().Trim();
                    }
                }
                else maBP = "";
            }
            else maBP = "";

            if (tgPhongBan.IsChecked == true)
            {
                if (cbPhongBan.SelectedValue != null)
                {
                    if (cbPhongBan.SelectedValue.ToString() == "0")
                    {
                        maPB = "";
                    }
                    else
                    {
                        maPB = cbPhongBan.SelectedValue.ToString().Trim();
                    }
                }
                else maPB = "";
            }
            else maPB = "";

            if (tgLoaiHopDong.IsChecked == true)
            {
                if (cbLoaiHopDong.SelectedValue != null)
                {
                    if (cbLoaiHopDong.SelectedValue.ToString() == "0")
                    {
                        maLHD = "";
                    }
                    else
                    {
                        maLHD = cbLoaiHopDong.SelectedValue.ToString().Trim();
                    }
                }
                else maLHD = "";
            }
            else maLHD = "";

            if (tgLoaiLuong.IsChecked == true)
            {
                if (cbLoaiLuong.SelectedValue != null)
                {
                    if (cbLoaiLuong.SelectedValue.ToString() == "0")
                    {
                        maLL = "";
                    }
                    else
                    {
                        maLL = cbLoaiLuong.SelectedValue.ToString().Trim();
                    }
                }
                else maLL = "";
            }
            else maLL = "";

            if (tgTinhTrang.IsChecked == true)
            {
                if (cbTinhTrang.SelectedValue != null)
                {
                    if (cbTinhTrang.SelectedValue.ToString() == "0")
                    {
                        maTT = "";
                    }
                    else
                    {
                        maTT = cbTinhTrang.SelectedValue.ToString().Trim();
                    }
                }
                else maTT = "";
            }
            else maTT = "";

            dataGridChoose.DataContext = BUS.NhanVienBUS.GetNhanVienByElementForChoose(maBP, maPB, maLHD, maLL, maTT);
        }

        private void ToggleElementChecked_Checked(object sender, RoutedEventArgs e)
        {
            GetForChoose();
        }

        private void ToggleElementUnChecked_Checked(object sender, RoutedEventArgs e)
        {
            GetForChoose();
        }
    }

}