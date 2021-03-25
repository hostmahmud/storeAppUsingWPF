using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace StoreApp.Views
{
    /// <summary>
    /// Interaction logic for Sales.xaml
    /// </summary>
    public partial class Sales : Window
    {
        public Sales()
        {
            InitializeComponent();
        }
        private void GetSaleDetails()
        {
            var json = File.ReadAllText(@"Sales.json");
            var jObject = JObject.Parse(json);
            if (jObject != null)
            {
                JArray sales = (JArray)jObject["Sales"];
                if (sales != null)
                {
                    List<Classes.Sales> sal = new List<Classes.Sales>();
                    int count = 0;
                    foreach (var item in sales)
                    {
                        sal.Add(new Classes.Sales() { Id = Convert.ToInt32(item["Id"]), ProductName = item["ProductName"].ToString(), Quantity = Convert.ToInt32(item["Quantity"]), TotalAmount = Convert.ToDecimal(item["TotalAmount"]), CustName = item["CustName"].ToString(), SaleDate = Convert.ToDateTime(item["SaleDate"]).Date, Status = item["Status"].ToString() });
                        count++;
                    }
                    txttotalSalesCount.Text = "Total Sales: "+count.ToString();
                    lvUsers.ItemsSource = sal;
                }
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void GridBarraTitulo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetSaleDetails();
        }

        private void btnAddNewSale_Click(object sender, RoutedEventArgs e)
        {
            AddSale addSale = new AddSale();
            addSale.Show();
            this.Close();

        }

        private void menuProducts_Click(object sender, RoutedEventArgs e)
        {
            Items items = new Items();
            items.Show();
            this.Close();
        }

        private void btnMenuCustomers_Click(object sender, RoutedEventArgs e)
        {
            Customers customers = new Customers();
            customers.Show();
            this.Close();
        }
    }
}
