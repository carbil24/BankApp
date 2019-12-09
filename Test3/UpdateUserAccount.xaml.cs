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
    /// Interaction logic for UpdateUserAccount.xaml
    /// </summary>
    public partial class UpdateUserAccount : Window
    {
        private User UserInfo { get; }
        private BankViewModel repo = new BankViewModel();

        public UpdateUserAccount(User existingUser)
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.Width;
            this.SizeToContent = SizeToContent.Height;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            UserInfo = existingUser;
            LoadUserInfo();
            txtNewName.Focus();
        }

        private void btnUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewName.Text) || string.IsNullOrEmpty(txtNewPassword.Password))
            {
                MessageBox.Show("New name and new password fields cannot be empty");
            }
            else
            {
                UserInfo.Name = txtNewName.Text;
                UserInfo.Password = txtNewPassword.Password;
                repo.UpdateUser(UserInfo);
                this.Close();
            }
        }

        #region Helper Methods
        private void LoadUserInfo()
        {
            txtbId.Text = UserInfo.ID.ToString();
            txtbAccount.Text = UserInfo.UserAccount.AccountNumber.ToString();
        }
        #endregion
    }
}
