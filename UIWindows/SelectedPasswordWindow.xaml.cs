using ImageRecognition.Data;
using ImageRecognition.Encryption;
using ImageRecognition.External_Scripts;
using ImageRecognition.Repository;
using ImageRecognition.State;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
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

namespace ImageRecognition.UIWindows
{
    /// <summary>
    /// Interaction logic for SelectedPasswordWindow.xaml
    /// </summary>
    public partial class SelectedPasswordWindow : Window
    {
        private Account _account;

        public SelectedPasswordWindow()
        {
            InitializeComponent();
        }

        public SelectedPasswordWindow(Account account)
        {
            InitializeComponent();

            
            this.myLabel1.Content = account.WebsiteURL;
            
            this.UsernameUpdated.Text = account.AccountName;
           
            string Hpassword = PasswordEncryption.Decrypt(account.AccountPassword, CurrentUser.GetCurrentUserPasswrod());
            
            this.PasswordUpdated.Text = Hpassword;
            
           _account = account;
        }

        private void BackToMainPageClicked(object sender, RoutedEventArgs e)
        {
            MainMenuWindow mainMenuWindow = new MainMenuWindow();
            this.Close();
            mainMenuWindow.Show();
        }

        private void RediterctURLClicked(object sender, RoutedEventArgs e)
        {
            ExternalCommands.run_chrome("a", _account.WebsiteURL);    
        }

        private void UsernameUpdated_TextChanged()
        {

        }

        private void DeleteClicked(object sender, RoutedEventArgs e)
        {
            
            User masterUser = UserRepository.GetUserWithAccounts(CurrentUser.GetCurrentUserName());
                      
            if (_account != null)
            {
                bool didDeletingSucced = UserRepository.DeleteAccount(masterUser, _account);
                if (didDeletingSucced)
                {
                    MessageBox.Show("Account deleted");
                    MainMenuWindow mainMenuWindow = new MainMenuWindow();
                    this.Hide();
                    mainMenuWindow.Show();
                }
                else
                {
                    MessageBox.Show("Unexpected error occurred");
                }
    
            }
            else
            {
                MessageBox.Show("Unexpected error occurred");
            }
        }

        private void EditClicked(object sender, RoutedEventArgs e)
        {
            User userTesting = UserRepository.GetUserWithAccounts(CurrentUser.GetCurrentUserName());
            if (_account != null)
            {
                _account.AccountName = this.UsernameUpdated.Text;
                _account.AccountPassword = PasswordEncryption.Encrypt(this.PasswordUpdated.Text, CurrentUser.GetCurrentUserPasswrod());
                        
                bool didEditSucced = UserRepository.EditAccount(userTesting, _account);
                if (didEditSucced)
                {
                    MessageBox.Show("Account succesfully updated");
                    MainMenuWindow mainMenuWindow = new MainMenuWindow();
                    this.Hide();
                    mainMenuWindow.Show();
                }
                else
                {
                    MessageBox.Show("Unexpected error occurred");
                }
            }
            else
            {
                MessageBox.Show("Unexpected error occurred");
            }

        }
    }
}
