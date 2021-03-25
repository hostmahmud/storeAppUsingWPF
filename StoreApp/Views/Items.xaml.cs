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
    /// Interaction logic for Items.xaml
    /// </summary>
    public partial class Items : Window
    {
        public Items()
        {
            InitializeComponent();
        }

        public static int id;
        public static string name = "";
        public static decimal price = decimal.Zero;
        public static string upc = "";
        public static int quantity = 0;
        public static string color = "";
        public static string category = "";
        public static string isstock = "";
        public static string image = "";
        public static string desc = "";

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        private void menuTotalSales_Click(object sender, RoutedEventArgs e)
        {
            Sales sales = new Sales();
            sales.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetItemsList();
        }

        private void btnAddNewItem_Click(object sender, RoutedEventArgs e)
        {
            AddItem ai = new AddItem();
            ai.Show();
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

        private void GetItemsList()
        {
            var json = File.ReadAllText(@"Products.json");
            var jObject = JObject.Parse(json);
            if (jObject != null)
            {
                JArray products = (JArray)jObject["Products"];
                if (products != null)
                {
                    List<Classes.Items> pro = new List<Classes.Items>();
                    int count = 0;
                    foreach (var item in products)
                    {
                        pro.Add(new Classes.Items() { Id = Convert.ToInt32(item["Id"]), Name = item["Name"].ToString(), Price = Convert.ToDecimal(item["Price"]), Upc = item["Upc"].ToString(), Quantity = Convert.ToInt32(item["Quantity"]), Color = item["Color"].ToString(), Category = item["Category"].ToString(), Image = item["Image"].ToString(), Description = item["Description"].ToString() });
                        count++;
                    }
                    txttotalProductsCount.Text = "Total Products: " + count.ToString();
                    lvUsers.ItemsSource = pro;
                }
            }
        }

        private void btnMenuCustomers_Click(object sender, RoutedEventArgs e)
        {
            Customers customers = new Customers();
            customers.Show();
            this.Close();
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            Classes.Items items = b.CommandParameter as Classes.Items;

            id = items.Id;
            name = items.Name;
            price = items.Price;
            upc = items.Upc;
            quantity = items.Quantity;
            color = items.Color;
            category = items.Category;
            isstock = items.IsStock;
            image = items.Image;
            desc = items.Description;

            SingleProduct sp = new SingleProduct();
            sp.Show();
            this.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var itemsjson = File.ReadAllText(@"Products.json");
            try
            {
                var jObject = JObject.Parse(itemsjson);
                JArray itemsArr = (JArray)jObject["Products"];

                Button button = sender as Button;
                Classes.Items itemId = button.CommandParameter as Classes.Items;
                int iId = itemId.Id;

                var ItemToDelete = itemsArr.FirstOrDefault(obj => obj["Id"].Value<int>() == iId);

                MessageBox.Show("Are you sure you want to delete this product?");

                itemsArr.Remove(ItemToDelete);

                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(@"Products.json", output);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MessageBox.Show("Product Deleted!");
                GetItemsList();
            }
        }
    }
}
