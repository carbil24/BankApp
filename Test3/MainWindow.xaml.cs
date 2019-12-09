using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Test3.Models;

namespace Test3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BankViewModel repo = new BankViewModel();
        private User UserInfo { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.Width;
            this.SizeToContent = SizeToContent.Height;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ResetControls();
        }

        private void btnDeposit_Click(object sender, RoutedEventArgs e)
        {
            Regex numberPattern = new Regex(@"^\d+([.]\d+)?$");

            if (string.IsNullOrEmpty(txtAmount.Text) || (!(numberPattern.IsMatch(txtAmount.Text))))
            {
                MessageBox.Show("Amount can only be a number");
            }
            else
            {
                DateTime timeStamp = DateTime.Now.ToLocalTime();
                int accountNumber = UserInfo.UserAccount.AccountNumber;
                char typeOfTransaction = 'D';
                double oldBalance = UserInfo.UserAccount.Balance;

                UserInfo.UserAccount.Balance += double.Parse(txtAmount.Text);

                double newBalance = UserInfo.UserAccount.Balance;
                AddTransactionToFile(timeStamp, accountNumber, typeOfTransaction, oldBalance, newBalance);
                AfterTransaction();
            }
        }

        private void btnWithdraw_Click(object sender, RoutedEventArgs e)
        {
            Regex numberPattern = new Regex(@"^\d+([.]\d+)?$");        

            if (string.IsNullOrEmpty(txtAmount.Text) || (!numberPattern.IsMatch(txtAmount.Text)))
            {
                MessageBox.Show("Amount can only be a number");
            }
            else
            {
                if (double.Parse(txtAmount.Text) > UserInfo.UserAccount.Balance)
                {
                    MessageBox.Show("You cannot withdraw more than $" + UserInfo.UserAccount.Balance);
                }
                else
                {
                    DateTime timeStamp = DateTime.Now.ToLocalTime();
                    int accountNumber = UserInfo.UserAccount.AccountNumber;
                    char typeOfTransaction = 'W';
                    double oldBalance = UserInfo.UserAccount.Balance;

                    UserInfo.UserAccount.Balance -= double.Parse(txtAmount.Text);

                    double newBalance = UserInfo.UserAccount.Balance;
                    AddTransactionToFile(timeStamp, accountNumber, typeOfTransaction, oldBalance, newBalance);
                    AfterTransaction();
                }
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow(this);
            loginWindow.ShowDialog();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            ResetControls();
        }

        private void AddTransactionToFile(DateTime timeStamp, int accountNumber, char typeOfTransaction, double oldBalance, double newBalance)
        {
            try
            {
                StreamWriter streamWriter = null;
                StringBuilder transaction = new StringBuilder();

                transaction.AppendLine(String.Format("[{0}] #{1} | {2} | {3:C} | {4:C}", timeStamp, accountNumber, typeOfTransaction, oldBalance, newBalance));


                using (streamWriter = new StreamWriter("transactions.txt", true))
                {
                    streamWriter.WriteLine(transaction.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        # region Helper Methods
        private void AfterTransaction()
        {
            MessageBox.Show("Transaction successful");
            repo.UpdateBalance(UserInfo);
            txtBalance.Text = UserInfo.UserAccount.Balance.ToString("c");
            txtAmount.Clear();
            txtAmount.Focus();
        }

        public void LoadUserInfo(User currentUser)
        {
            txtbName.Text = currentUser.Name;
            txtbID.Text = currentUser.ID.ToString();
            txtBalance.Text = currentUser.UserAccount.Balance.ToString("c");
            UserInfo = currentUser;
        }

        private void ResetControls()
        {
            txtbID.Text = "";
            txtbName.Text = "";
            txtAmount.Clear();
            txtBalance.Clear();
            txtBalance.IsEnabled = false;
            txtAmount.IsEnabled = false;
            btnDeposit.IsEnabled = false;
            btnWithdraw.IsEnabled = false;
            btnLogin.Visibility = Visibility.Visible;
            btnLogout.Visibility = Visibility.Hidden;
        }

        public void LoadControls()
        {
            txtAmount.IsEnabled = true;
            txtAmount.Focus();
            btnDeposit.IsEnabled = true;
            btnWithdraw.IsEnabled = true;
            btnLogin.Visibility = Visibility.Hidden;
            btnLogout.Visibility = Visibility.Visible;
        }
        #endregion
    }
}
