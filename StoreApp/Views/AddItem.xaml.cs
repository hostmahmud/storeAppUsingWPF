using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for AddItem.xaml
    /// </summary>
    public partial class AddItem : Window
    {
        public AddItem()
        {
            InitializeComponent();
            List<string> category = new List<string>()
            {
                "--Select Category--",
                "Electronics",
                "Home Decor",
                "Outdoor"
            };
            List<string> color = new List<string>()
            {
                "--Select Color--",
                "Red",
                "Blue",
                "Green"
            };
            cmbCategory.ItemsSource = category;
            cmbCategory.Text = "--Select Category--";
            cmbColor.ItemsSource = color;
            cmbColor.Text = "--Select Color--";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Items i = new Items();
            i.Show();
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Items i = new Items();
            i.Show();
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Classes.Items i = new Classes.Items();
                i.Id = Convert.ToInt32(txtId.Text);
                i.Name = txtName.Text;
                i.Price = Convert.ToDecimal(txtPrice.Text);
                i.Upc = txtupc.Text;
                i.Quantity = Convert.ToInt32(txtQuantity.Text);
                i.Color = cmbColor.Text;
                i.Category = cmbCategory.Text;
                i.Image = txtfilepath.Text;
                i.Description = txtDescription.Text;
                if (inStock.IsChecked == true)
                {
                    i.IsStock = "In Stock";
                }
                if (outStock.IsChecked == true)
                {
                    i.IsStock = "Out of Stock";
                }
                i.Image = txtfilepath.Text;

                var newItem = "{ 'Id': '" + i.Id + "', 'Name': '" + i.Name + "', 'Price': '" + i.Price + "', 'Upc': '" + i.Upc + "', 'Quantity': '" + i.Quantity + "', 'Color': '" + i.Color + "', 'Category': '" + i.Category + "', 'IsStock': '" + i.IsStock + "', 'Image': '" + i.Image + "', 'Description': '" + i.Description + "'}";

                var itemJson = File.ReadAllText(@"Products.json");
                var jsonObject = JObject.Parse(itemJson);
                var itemArr = jsonObject.GetValue("Products") as JArray;
                var customer = JObject.Parse(newItem);
                itemArr.Add(customer);

                jsonObject["Products"] = itemArr;
                string jsonResult = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                File.WriteAllText(@"Products.json", jsonResult);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured : " + ex.Message.ToString());
            }
            finally
            {
                MessageBox.Show("New Product Added!");
                txtId.Clear();
                txtName.Clear();
                txtPrice.Clear();
                txtQuantity.Clear();
                txtupc.Clear();
                itemPic.Source = null;
                txtDescription.Clear();
                cmbCategory.Text = "--Select Category--";
                cmbColor.Text = "--Select Color--";
            }
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
            Customers c = new Customers();
            c.Show();
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


        private void btnUploadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                itemPic.Source = new BitmapImage(new Uri(ofd.FileName));

                var tmpFileName = DateTime.Now.Minute+"-"+DateTime.Now.Second + Path.GetExtension(ofd.FileName);
                txtfilepath.Text = tmpFileName;
                var imagePath = Path.Combine(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../Assets\Images\") + tmpFileName);
                File.Copy(ofd.FileName, imagePath);
            }
        }
    }
}