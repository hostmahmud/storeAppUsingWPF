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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StoreApp.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnUserLogin_Click(object sender, RoutedEventArgs e)
        {
            Classes.UserLogin ul = new Classes.UserLogin();
            ul.Username = txtUsername.Text;
            ul.Password = txtPassword.Password;

            var json = File.ReadAllText(@"Users.json");
            var jObject = JObject.Parse(json);
            if (jObject != null)
            {
                JArray empd = (JArray)jObject["Users"];
                if (empd != null)
                {
                    foreach (var ulo in empd.Where(obj => obj["Username"].Value<string>() == ul.Username))
                    {
                        string username = ulo["Username"].ToString();
                        string password = ulo["Password"].ToString();

                        if (ul.Username == username && ul.Password == password)
                        {
                            Dashboard vDashboard = new Dashboard();
                            vDashboard.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Username or Password.");
                        }
                    }

                }
            }
        }

        private void NewRegister_Click(object sender, RoutedEventArgs e)
        {
            Register vRegister = new Register();
            vRegister.Show();
            this.Hide();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();

        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void GridBarraTitulo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
