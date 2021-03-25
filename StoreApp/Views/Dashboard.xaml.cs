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
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void GridBarraTitulo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void GetSaleDetails()
        {
            var json = File.ReadAllText(@"Sales.json");
            var jObject = JObject.Parse(json);
            if (jObject != null)
            {
                JArray empd = (JArray)jObject["Sales"];
                if (empd != null)
                {
                    List<Classes.Sales> empl = new List<Classes.Sales>();
                    int count = 0;
                    foreach (var item in empd)
                    {
                        empl.Add(new Classes.Sales() { Id = Convert.ToInt32(item["Id"]), ProductName = item["ProductName"].ToString(), Quantity = Convert.ToInt32(item["Quantity"]), TotalAmount = Convert.ToDecimal(item["TotalAmount"]), CustName = item["CustName"].ToString(), SaleDate = Convert.ToDateTime(item["SaleDate"]).Date });
                        count++;
                    }
                    txtTotalSales.Text = count.ToString();
                    lvUsers.ItemsSource = empl;
                }
            }
        }
        private void CountProducts()
        {
            var json = File.ReadAllText(@"Products.json");
            var jObject = JObject.Parse(json);
            if (jObject != null)
            {
                JArray productsArray = (JArray)jObject["Products"];
                if (productsArray != null)
                {
                    List<Classes.Items> pr = new List<Classes.Items>();
                    int count = 0;
                    foreach (var item in productsArray)
                    {
                        pr.Add(new Classes.Items() { Id = Convert.ToInt32(item["Id"]) });
                        count++;
                    }
                    txtTotalItem.Text = count.ToString();
                    
                }
            }
        }
        private void CountCustomers()
        {
            var json = File.ReadAllText(@"Customers.json");
            var jObject = JObject.Parse(json);
            if (jObject != null)
            {
                JArray customersArray = (JArray)jObject["Customers"];
                if (customersArray != null)
                {
                    List<Classes.Customers> cust = new List<Classes.Customers>();
                    int count = 0;
                    foreach (var item in customersArray)
                    {
                        cust.Add(new Classes.Customers() { Id = Convert.ToInt32(item["Id"]) });
                        count++;
                    }
                    txtTotalCustomer.Text = count.ToString();

                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetSaleDetails();
            CountProducts();
            CountCustomers();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void menuSales_Click(object sender, RoutedEventArgs e)
        {
            Sales sales = new Sales();
            sales.Show();
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
            Customers c = new Customers();
            c.Show();
            this.Close();
        }

        private void btnGoToSales_Click(object sender, RoutedEventArgs e)
        {
            Sales sales = new Sales();
            sales.Show();
            this.Close();
        }

        private void btnGoToItems_Click(object sender, RoutedEventArgs e)
        {
            Items i = new Items();
            i.Show();
            this.Close();
        }

        private void btnGoToCustomers_Click(object sender, RoutedEventArgs e)
        {
            Customers c = new Customers();
            c.Show();
            this.Close();
        }
    }
}
