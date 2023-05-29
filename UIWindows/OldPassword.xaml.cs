using ImageRecognition.Data;
using ImageRecognition.Encryption;
using ImageRecognition.Repository;
using ImageRecognition.State;
using System;
using System.Collections.Generic;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;

namespace ImageRecognition.UIWindows
{
    /// <summary>
    /// Interaction logic for OldPassword.xaml
    /// </summary>
    public partial class OldPassword : Window
    {
        bool changedPassword = false;
        bool approvedPassword = false;
        private Account _account;
        public OldPassword()
        {
            InitializeComponent();
            changedPassword = true;
        }
        public OldPassword(bool changePW, Account account)
        {
            InitializeComponent();
            approvedPassword = changePW;
            _account = account;
        }
        private void MasterPasswordClicked(object sender, RoutedEventArgs e)
        {
                User userToCheck = UserRepository.GetUserWithAccounts(CurrentUser.GetCurrentUserName());
                bool isUserValid = Hasher.Validate(userToCheck.MasterPasswrod, MasterPasswordInput.Password);
            if (approvedPassword)
            {
                if (isUserValid)
                {
                    SelectedPasswordWindow selectedPasswordWindow = new SelectedPasswordWindow(_account);
                    this.Hide();
                    selectedPasswordWindow.Show();
                }
                else
                {
                    MessageBox.Show("Wrong credentials");
                }
            }

            if (changedPassword)
            {
                if (isUserValid)
                {
                    NewPasswordWindow newPasswordWindow = new NewPasswordWindow();
                    this.Hide();
                    newPasswordWindow.Show();
                }
                else
                {
                    MessageBox.Show("Wrong credentials");
                }
            }

        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            MainMenuWindow mainMenuWindow = new MainMenuWindow();
            this.Hide();
            mainMenuWindow.Show();
        }
    }
}
