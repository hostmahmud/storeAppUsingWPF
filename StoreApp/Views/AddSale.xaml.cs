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
    /// Interaction logic for AddSale.xaml
    /// </summary>
    public partial class AddSale : Window
    {
        public AddSale()
        {
            InitializeComponent();
            List<string> status = new List<string>()
            {
                "Pending",
                "Processing",
                "Completed"
            };
            cmbStatus.ItemsSource = status;
            cmbStatus.Text = "Pending";
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Sales sales = new Sales();
            sales.Show();
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Classes.Sales sale = new Classes.Sales();

            sale.Id = Convert.ToInt32(txtId.Text);
            sale.ProductName = cmbProductName.Text;
            sale.Quantity = Convert.ToInt32(txtQty.Text);
            sale.TotalAmount = Convert.ToDecimal(txtAmt.Text);
            sale.CustName = cmbCustomerList.Text;
            sale.SaleDate = Convert.ToDateTime(txtSaleDate.Text);
            sale.Status = cmbStatus.Text;


            var newSale = "{ 'Id': '" + sale.Id + "', 'ProductName': '" + sale.ProductName + "', 'Quantity': '" + sale.Quantity + "', 'TotalAmount': '" + sale.TotalAmount + "', 'CustName': '" + sale.CustName + "', 'SaleDate': '" + sale.SaleDate + "', 'Status': '" + sale.Status + "'}";
            try
            {
                var json = File.ReadAllText(@"Sales.json");
                var jsonObj = JObject.Parse(json);
                var SaleArrary = jsonObj.GetValue("Sales") as JArray;
                var tmpSale = JObject.Parse(newSale);
                SaleArrary.Add(tmpSale);

                jsonObj["Sales"] = SaleArrary;
                string newJsonResult = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
                File.WriteAllText(@"Sales.json", newJsonResult);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add Error : " + ex.Message.ToString());
            }
            finally
            {
                MessageBox.Show("Employee Added!");
                txtId.Clear();
                txtAmt.Clear();
                txtQty.Clear();
                txtSaleDate.Text = "";
                cmbCustomerList.Text = "";
                cmbProductName.Text = "";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetCustomerName();
            GetProductName();
        }

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
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

        private void btnMenuSales_Click(object sender, RoutedEventArgs e)
        {
            Sales sales = new Sales();
            sales.Show();
            this.Close();
        }

        private void btnMenuCustomer_Click(object sender, RoutedEventArgs e)
        {
            Customers c = new Customers();
            c.Show();
            this.Close();
        }

        private void btnMenuProducts_Click(object sender, RoutedEventArgs e)
        {
            Items items = new Items();
            items.Show();
            this.Close();
        }
        private void GetCustomerName()
        {
            var json = File.ReadAllText(@"Customers.json");
            var jObject = JObject.Parse(json);
            if (jObject != null)
            {
                JArray customer = (JArray)jObject["Customers"];
                if (customer != null)
                {
                    //List<Classes.Customers> cust = new List<Classes.Customers>();
                    List<string> c = new List<string>();
                    foreach (var item in customer)
                    {
                        c.Add(item["FullName"].ToString());
                    }
                    cmbCustomerList.ItemsSource = c;
                }
            }
        }
        private void GetProductName()
        {
            var json = File.ReadAllText(@"Products.json");
            var jObject = JObject.Parse(json);
            if (jObject != null)
            {
                JArray product = (JArray)jObject["Products"];
                if (product != null)
                {
                    //List<Classes.Customers> cust = new List<Classes.Customers>();
                    List<string> p = new List<string>();
                    foreach (var item in product)
                    {
                        p.Add(item["Name"].ToString());
                    }
                    cmbProductName.ItemsSource = p;
                }
            }
        }
    }
}
