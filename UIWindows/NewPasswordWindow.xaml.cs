using ImageRecognition.Data;
using ImageRecognition.Encryption;
using ImageRecognition.Repository;
using ImageRecognition.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
    /// Interaction logic for NewPasswordWindow.xaml
    /// </summary>
    public partial class NewPasswordWindow : Window
    {
        public NewPasswordWindow()
        {
            InitializeComponent();
        }

        private async void NewPasswordClicked(object sender, RoutedEventArgs e)
        {
            string decryptor = CurrentUser.GetCurrentUserPasswrod();
            User userToUpdateListofPws;
            userToUpdateListofPws = CurrentUser.GetCurrentUser();
            
            userToUpdateListofPws.MasterPasswrod = newUserPasswordInput.Password;
            bool succesfullAccountUpdate = await UserRepository.ReencryptingUserPasswords(userToUpdateListofPws, decryptor);

            if (succesfullAccountUpdate)
            {
                MessageBox.Show("Password updated succesfully");
                CurrentUser.SetCurrentUserPassword(newUserPasswordInput.Password);
                MainMenuWindow mainMenuWindow = new MainMenuWindow();
                this.Hide();
                mainMenuWindow.Show();

            }
            else
            {
                MessageBox.Show("Something went wrong. Your account was not updated");
            }

        }

        private void BackToUserSettingClicked(object sender, RoutedEventArgs e)
        {
            UserSettingWindow userSettingWindow = new UserSettingWindow();
            this.Hide();
            userSettingWindow.Show();
        }
    }
}
