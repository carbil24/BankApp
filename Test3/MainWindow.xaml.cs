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

        /*
        * Button used to deposit an amount to the account, updates the record in the database and append a line to the log file.
        */
        private void btnDeposit_Click(object sender, RoutedEventArgs e)
        {
            Regex numberPattern = new Regex(@"^\d+([.]\d+)?$");

            if (string.IsNullOrEmpty(txtAmount.Text) || (!(numberPattern.IsMatch(txtAmount.Text))))
            {
                MessageBox.Show("Amount can only be a number", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Error);
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

        /*
        * Button used to withdraw an amount from the account, updates the record in the database and append a line to the log file.
        */
        private void btnWithdraw_Click(object sender, RoutedEventArgs e)
        {
            Regex numberPattern = new Regex(@"^\d+([.]\d+)?$");        

            if (string.IsNullOrEmpty(txtAmount.Text) || (!numberPattern.IsMatch(txtAmount.Text)))
            {
                MessageBox.Show("Amount can only be a number", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (double.Parse(txtAmount.Text) > UserInfo.UserAccount.Balance)
                {
                    MessageBox.Show("You cannot withdraw more than $" + UserInfo.UserAccount.Balance, "Attention", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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

        /*
        * Button used to login to the bank app.
        */
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow(this);
            loginWindow.ShowDialog();
        }

        /*
        * Button used to logout from the bank app.
        */
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            ResetControls();
        }

        /*
        * Gets the information of the trasaction.
        * Objective: to add a new line in the log file with the information of the transaction.
        */
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

        #region Helper Methods

        /* Helper method to avoid duplicate code in Deposit and Withdraw */
        private void AfterTransaction()
        {
            MessageBox.Show("Transaction successful", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            //Update balance in the account of the specific user.
            repo.UpdateBalance(UserInfo);
            txtBalance.Text = UserInfo.UserAccount.Balance.ToString("c");
            txtAmount.Clear();
            txtAmount.Focus();
        }

        /*
         * Gets the user entered in the Login Window from the database and loads its properties into the Main Window.
         * Objective: to make transactions to the specific account.
         */
        public void LoadUserInfo(User currentUser)
        {
            txtbName.Text = currentUser.Name;
            txtbID.Text = currentUser.ID.ToString();
            txtBalance.Text = currentUser.UserAccount.Balance.ToString("c");
            UserInfo = currentUser;
        }

        /* Helper method to reset the controls in the form */
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

        /* Helper method to enable the controls in the form */
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
