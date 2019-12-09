using System;
using System.Collections.Generic;
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
using Test3.Models;

namespace Test3
{
    /// <summary>
    /// Interaction logic for AddUserAccount.xaml
    /// </summary>
    public partial class AddUserAccount : Window
    {
        private BankViewModel repo = new BankViewModel();

        public AddUserAccount()
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.Width;
            this.SizeToContent = SizeToContent.Height;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            txtName.Focus();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPassword.Password))
            {
                MessageBox.Show("Name and password fields cannot be empty");
            }
            else
            {
                User user = new User()
                {
                    Name = txtName.Text,
                    Password = txtPassword.Password,
                    UserAccount = new Account()
                    {
                        Balance = 0
                    }
                };

                repo.AddNewUser(user);
                this.Close();
            }
        }
    }
}
