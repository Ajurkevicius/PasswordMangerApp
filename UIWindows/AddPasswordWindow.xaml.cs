using ImageRecognition.Data;
using ImageRecognition.Encryption;
using ImageRecognition.Repository;
using ImageRecognition.State;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace ImageRecognition.UIWindows
{
    /// <summary>
    /// Interaction logic for AddPasswordWindow.xaml
    /// </summary>
    public partial class AddPasswordWindow : Window
    {
        public AddPasswordWindow()
        {
            InitializeComponent();
        }

        private void addAccountClicked(object sender, RoutedEventArgs e)
        {
            if (NameInput.Text != null && UrlInput.Text != null && PasswordInput.Password != null)
            {
                User user = UserRepository.GetUserWithAccounts(CurrentUser.GetCurrentUserName());
                Account account = new Account();
                account.AccountName = NameInput.Text;
                account.AccountPassword = PasswordEncryption.Encrypt(PasswordInput.Password, CurrentUser.GetCurrentUserPasswrod());
                account.WebsiteURL = UrlInput.Text;

                bool isAdded = UserRepository.AddAccount(user, account);
                if (isAdded)
                {
                    MessageBox.Show("User succesfully added");
                }
                else
                {
                    MessageBox.Show("Something went wrong");
                }
            }
            else
            {
                MessageBox.Show("Input all values");
            }
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            MainMenuWindow mainMenuWindow = new MainMenuWindow();
            this.Close();
            mainMenuWindow.Show();
        }
    }
}
