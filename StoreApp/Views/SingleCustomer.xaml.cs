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
    /// Interaction logic for SingleCustomer.xaml
    /// </summary>
    public partial class SingleCustomer : Window
    {
        public SingleCustomer()
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string jsonUpdate = File.ReadAllText(@"Customers.json");

            try
            {
                var jObject = JObject.Parse(jsonUpdate);
                JArray JsonUpdateArrary = (JArray)jObject["Customers"];

                var Id = Convert.ToInt32(txtId.Text);
                var FullName = txtName.Text; ;
                var Email = txtEmail.Text;
                var Phone = txtPhone.Text;
                var Address = txtAddress.Text;
                var city = cmbCity.Text;
                var country = cmbCountry.Text;

                foreach (var customer in JsonUpdateArrary.Where(obj => obj["Id"].Value<int>() == Id))
                {
                    customer["FullName"] = !string.IsNullOrEmpty(FullName) ? FullName : "";
                    customer["Email"] = !string.IsNullOrEmpty(Email) ? Email : "";
                    customer["Phone"] = !string.IsNullOrEmpty(Phone) ? Phone : "";
                    customer["Address"] = !string.IsNullOrEmpty(Address) ? Address : "";
                    customer["City"] = !string.IsNullOrEmpty(city) ? city : "";
                    customer["Country"] = !string.IsNullOrEmpty(country) ? country : "";
                }

                jObject["Customers"] = JsonUpdateArrary;
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(@"Customers.json", output);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Update Error : " + ex.Message.ToString());
            }
            finally
            {
                MessageBox.Show("Record Updated!");
                Customers c = new Customers();
                c.Show();
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Customers c = new Customers();
            c.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            singleCustomerTitle.Text = "== "+Customers.name+" ==";
            txtId.Text = Customers.id.ToString();
            txtName.Text = Customers.name.ToString();
            txtEmail.Text = Customers.email.ToString();
            txtPhone.Text = Customers.phone.ToString();
            txtAddress.Text = Customers.address.ToString();
            cmbCity.Text = Customers.city.ToString();
            cmbCountry.Text = Customers.country.ToString();
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

        private void btnBackToCustomerList_Click(object sender, RoutedEventArgs e)
        {
            Customers c = new Customers();
            c.Show();
            this.Close();
        }
    }
}
