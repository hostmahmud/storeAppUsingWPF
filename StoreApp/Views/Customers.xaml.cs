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
    /// Interaction logic for Customers.xaml
    /// </summary>
    public partial class Customers : Window
    {
        public Customers()
        {
            InitializeComponent();
        }
        public static int id;
        public static string name = "";
        public static string email = "";
        public static string phone = "";
        public static string address = "";
        public static string city = "";
        public static string country = "";
        private void GetCustomersList()
        {
            var json = File.ReadAllText(@"Customers.json");
            var jObject = JObject.Parse(json);
            if (jObject != null)
            {
                JArray customers = (JArray)jObject["Customers"];
                if (customers != null)
                {
                    List<Classes.Customers> cust = new List<Classes.Customers>();
                    int count = 0;
                    foreach (var item in customers)
                    {
                        cust.Add(new Classes.Customers() { Id = Convert.ToInt32(item["Id"]), FullName = item["FullName"].ToString(), Email = item["Email"].ToString(), Phone = item["Phone"].ToString(), Address = item["Address"].ToString(), City = item["City"].ToString(), Country = item["Country"].ToString() });
                        count++;
                    }
                    txttotalCustomerCount.Text = "Total Customers: " + count.ToString();
                    activeCustomers.ItemsSource = cust;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetCustomersList();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            var custjson = File.ReadAllText(@"Customers.json");
            try
            {
                var jObject = JObject.Parse(custjson);
                JArray custArr = (JArray)jObject["Customers"];

                Button button = sender as Button;
                Classes.Customers custId = button.CommandParameter as Classes.Customers;
                int cId = custId.Id;

                var CustomerName = string.Empty;
                var CustToDelete = custArr.FirstOrDefault(obj => obj["Id"].Value<int>() == cId);

                custArr.Remove(CustToDelete);

                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(@"Customers.json", output);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MessageBox.Show("Customer Deleted");
                GetCustomersList();
            }
        }

        private void menuProducts_Click(object sender, RoutedEventArgs e)
        {
            Items items = new Items();
            items.Show();
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

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        private void btnAddNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            AddCustomer addCustomer = new AddCustomer();
            addCustomer.Show();
            this.Close();
        }

        private void menuTotalSales_Click(object sender, RoutedEventArgs e)
        {
            Sales sales = new Sales();
            sales.Show();
            this.Close();
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            Classes.Customers cust = b.CommandParameter as Classes.Customers;

            id = cust.Id;
            name = cust.FullName;
            email = cust.Email;
            phone = cust.Phone;
            address = cust.Address;
            city = cust.City;
            country = cust.Country;

            SingleCustomer sc = new SingleCustomer();
            sc.Show();
            this.Close();


        }
    }
}
