using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Test3.Models;

namespace Test3
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private BankViewModel repo = new BankViewModel();
        private MainWindow mainWindow;

        public LoginWindow(MainWindow _mainWindow)
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.Width;
            this.SizeToContent = SizeToContent.Height;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            txtId.Focus();
            mainWindow = _mainWindow;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Regex numberPattern = new Regex(@"^\d+$");

            if (string.IsNullOrEmpty(txtId.Text) || string.IsNullOrEmpty(txtPassword.Password))
            {
                MessageBox.Show("ID and password fields cannot be empty");
            }
            else if (!(numberPattern.IsMatch(txtId.Text)))
            {
                MessageBox.Show("ID can only be a number");
            }
            else
            {
                User currentUser = repo.LoginUser(int.Parse(txtId.Text.ToString()), txtPassword.Password);

                if (currentUser != null)
                {
                    if (!currentUser.Name.Equals("admin"))
                    {
                        MessageBox.Show("Welcome " + currentUser.Name.ToString() + "!");
                        mainWindow.LoadUserInfo(currentUser);
                        mainWindow.LoadControls();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Welcome " + currentUser.Name.ToString() + "!");
                        AdminPanel adminWindow = new AdminPanel();
                        this.Close();
                        adminWindow.ShowDialog();

                    }
                }
                else
                {
                    MessageBox.Show("Invalid ID/Password");
                }
            }
        }
    }
}
