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
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        private BankViewModel repo = new BankViewModel();

        public AdminPanel()
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.Width;
            this.SizeToContent = SizeToContent.Height;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //Update the grid
            dgUsers.ItemsSource = repo.GetAllUsersData();
        }

        /* Button to call the AddUser window. */
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddUserAccount addUserWindow = new AddUserAccount();
                addUserWindow.ShowDialog();
                //Update the grid
                dgUsers.ItemsSource = repo.GetAllUsersData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /* Button to call the UpdateUser window. */
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem != null)
            {
                try
                {
                    User current = dgUsers.SelectedItem as User;
                    UpdateUserAccount updateUserWindow = new UpdateUserAccount(current);
                    updateUserWindow.ShowDialog();
                    //Update the grid
                    dgUsers.ItemsSource = repo.GetAllUsersData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select an user first", "User not selected", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /* Button to delete a record from the database */
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem != null)
            {
                if (MessageBox.Show("Are you sure you want to delete?\n" +
                 "This will delete the account record as well", "Confirm Delete",
                 MessageBoxButton.YesNo, MessageBoxImage.Stop) == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Calls the method to delete record in the ViewModel class.
                        repo.DeleteUserRecord((dgUsers.SelectedItem as User).ID);
                        // Update the grid
                        dgUsers.ItemsSource = repo.GetAllUsersData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }             
            else
            {
                MessageBox.Show("Please select an user first", "User not selected", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
