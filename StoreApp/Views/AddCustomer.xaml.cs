using Newtonsoft.Json;
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
    /// Interaction logic for AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Window
    {
        public AddCustomer()
        {
            InitializeComponent();
            List<string> city = new List<string>()
            {
                "Dhaka",
                "Chittagong",
                "Comilla"
            };
            List<string> country = new List<string>()
            {
                "Bangladesh",
                "India",
                "Pakistan"
            };
            cmbCity.ItemsSource = city;
            cmbCountry.ItemsSource = country;
            cmbCountry.Text = "Bangladesh";
            cmbCity.Text = "Dhaka";
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Customers customers = new Customers();
            customers.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Classes.AddCustomer add = new Classes.AddCustomer();
            add.Id = Convert.ToInt32(txtId.Text);
            add.FullName = txtName.Text;
            add.Email = txtEmail.Text;
            add.Phone = txtPhone.Text;
            add.Address = txtAddress.Text;
            add.City = cmbCity.Text;
            add.Country = cmbCountry.Text;

            var newCustomer = "{ 'Id': '" + add.Id + "', 'FullName': '" + add.FullName + "', 'Email': '" + add.Email + "', 'Phone': '" + add.Phone + "', 'Address': '" + add.Address + "', 'City': '" + add.City + "', 'Country': '" + add.Country + "'}";

            try
            {
                var custJson = File.ReadAllText(@"Customers.json");
                var jsonObject = JObject.Parse(custJson);
                var customerArr = jsonObject.GetValue("Customers") as JArray;
                var customer = JObject.Parse(newCustomer);
                customerArr.Add(customer);

                jsonObject["Customers"] = customerArr;
                string jsonResult = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                File.WriteAllText(@"Customers.json", jsonResult);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured : " + ex.Message.ToString());
            }
            finally
            {
                MessageBox.Show("New Customer Added!");
                txtId.Clear();
                txtAddress.Clear();
                txtEmail.Clear();
                txtName.Clear();
                txtPhone.Clear();
                cmbCity.Text = "Dhaka";
                cmbCountry.Text = "Bangladesh";
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Customers c = new Customers();
            c.Show();
            this.Close();
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

        private void btnMenuCustomers_Click(object sender, RoutedEventArgs e)
        {
            Customers cs = new Customers();
            cs.Show();
            this.Close();
        }
    }
}
