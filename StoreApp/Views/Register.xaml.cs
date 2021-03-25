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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void alreadyRegistered_Click(object sender, RoutedEventArgs e)
        {
            Login vLogin = new Login();
            vLogin.Show();
            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Classes.Register cRegister = new Classes.Register();
            cRegister.Username = txtUsername.Text;
            cRegister.Password = txtPassword.Password;

            var newCompanyMember = "{ 'Username': '" + cRegister.Username + "', 'Password': '" + cRegister.Password + "'}";
            try
            {
                var json = File.ReadAllText(@"Users.json");
                var jsonObj = JObject.Parse(json);
                var EmployeeArrary = jsonObj.GetValue("Users") as JArray;
                var newEmployee = JObject.Parse(newCompanyMember);
                EmployeeArrary.Add(newEmployee);

                jsonObj["Users"] = EmployeeArrary;
                string newJsonResult = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
                File.WriteAllText(@"Users.json", newJsonResult);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured : " + ex.Message.ToString());
            }
            finally
            {
                MessageBox.Show("Registration Successfull. Redirecting to login page...");
                Login login = new Login();
                login.Show();
                this.Close();
                //txtfName.Clear();
                //txtLName.Clear();
                //txtId.Clear();
                //txtEmail.Clear();
                //txtContactNo.Clear();
                //cmbTitle.Text = "";
            }

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
