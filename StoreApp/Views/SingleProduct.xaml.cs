using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace StoreApp.Views
{
    /// <summary>
    /// Interaction logic for SingleProduct.xaml
    /// </summary>
    public partial class SingleProduct : Window
    {
        public SingleProduct()
        {
            InitializeComponent();
        }

        private void btnBackToProductsList_Click(object sender, RoutedEventArgs e)
        {
            Items i = new Items();
            i.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            singleProductTitle.Text = "== " + Items.name + " ==";
            SingleTitle.Content = Items.name;
            SinglePrice.Content = "Price: $"+Items.price;
            SingleUpc.Content = "UPC: "+Items.upc;
            SingleCOlor.Content = "Color: "+Items.color;
            SingleCategory.Content = "Category: "+Items.category;
            SingleDescription.Text = Items.desc;
            var imagePath = Path.Combine(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../Assets\Images\")+Items.image);
            singleproductImage.Source = new BitmapImage(new Uri(imagePath));
        }

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            Dashboard d = new Dashboard();
            d.Show();
            this.Close();
        }

        private void menuTotalSales_Click(object sender, RoutedEventArgs e)
        {
            Sales s = new Sales();
            s.Show();
            this.Close();
        }

        private void menuProducts_Click(object sender, RoutedEventArgs e)
        {
            Items i = new Items();
            i.Show();
            this.Close();
        }

        private void GridBarraTitulo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMenuCustomers_Click(object sender, RoutedEventArgs e)
        {
            Customers c = new Customers();
            c.Show();
            this.Close();
        }
    }
}
