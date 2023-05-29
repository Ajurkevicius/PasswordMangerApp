using ImageRecognition.Data;
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
    /// Interaction logic for MainMenuWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : Window
    {
        public MainMenuWindow()
        {
            InitializeComponent();
        }

        private void PaswordsMenuClicked(object sender, RoutedEventArgs e)
        {
            
            User userAccount = CurrentUser.GetCurrentUser();
            User userToDisplay = UserRepository.GetUserWithAccounts(userAccount.Name);
            List<Account> displayList = userToDisplay.ManagedAccounts;
            Debug.WriteLine(displayList);
            ManagedPasswordsWindow managedPasswordsWindow = new ManagedPasswordsWindow(displayList);
            this.Hide();
            managedPasswordsWindow.Show();
        }

        private void AddAccountClicked(object sender, RoutedEventArgs e)
        {
            AddPasswordWindow addPasswordWindow = new AddPasswordWindow();
            this.Hide();
            addPasswordWindow.Show();
        }

        private void UserSettingsClicked(object sender, RoutedEventArgs e)
        {
            UserSettingWindow userSettingWindow = new UserSettingWindow();
            this.Hide();
            userSettingWindow.Show();
        }
    }
}
