using ORM_Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace DEMOAPP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DBConnection db;
        public MainWindow()
        {
            InitializeComponent();
            db = DBConnectionFactory.CreateDBInstance(@"Data source=DESKTOP-R463O3I\SQLEXPRESS;Database=ORM;User ID=sa;Password=123456; Integrated Security=true", "sqlserver");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ResetTable();
        }

        private void InsertBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SelectBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            Product product = DataTable.SelectedItem as Product;
            if (product == null)
            {
                MessageBox.Show("Please choose product to update");
                return;
            }

            product.Name = TBName.Text;
            product.Price = decimal.Parse(TBPrice.Text);
            product.BarcodeId = int.Parse(TBBarcodeID.Text);
            product.CategoryId = int.Parse(TBCategory.Text);
            var repo = db.CreateRepository<Product>();
            repo.Update(product);
            MessageBox.Show("Update success!");
            ResetTable();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            Product product = DataTable.SelectedItem as Product;
            if(product == null)
            {
                MessageBox.Show("Please choose product to delete");
                return;
            }
            var repo = db.CreateRepository<Product>();
            repo.Delete(product);
            MessageBox.Show("Delete success!");
            ResetTable();
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            string name = TBName.Text;
            decimal price = decimal.Parse(TBPrice.Text);
            int barcodeId = int.Parse(TBBarcodeID.Text);
            int categoryId = int.Parse(TBCategory.Text);

            Product product = new Product()
            {
                Price = price,
                Name = name,
                BarcodeId = barcodeId,
                CategoryId = categoryId
            };
            var repo = db.CreateRepository<Product>();
            repo.Insert(product);
            ResetTable();
        }

        private void DataTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Product product = DataTable.SelectedItem as Product;
            if (product == null) return;
            TBName.Text = product.Name;
            TBPrice.Text = product.Price.ToString();
            TBCategory.Text = product.CategoryId.ToString();
            TBBarcodeID.Text = product.BarcodeId.ToString();
        }

        public void ResetTable()
        {
            var repo = db.CreateRepository<Product>();
            var list = repo.List();
            DataTable.ItemsSource = list;
        }
    }
}
