using ImageRecognition.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageRecognition.UIWindows
{
    /// <summary>
    /// Interaction logic for ManagedPasswordsWindow.xaml
    /// </summary>
    public partial class ManagedPasswordsWindow : Window
    {
        
        public List<Account> ManagedAccounts { get; set; }

        public ManagedPasswordsWindow()
        {
            InitializeComponent();
        }
        public ManagedPasswordsWindow(List<Account> managedAccounts)
        {
            InitializeComponent();
            ManagedAccounts = managedAccounts;
            DataContext = this;
            
        }

        private void ListBox_Sel(object sender, SelectionChangedEventArgs e)
        {

        }

        private void accountsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedAccount = accountsListBox.SelectedItem as Account;

            
            if (selectedAccount != null)
            {
                
                FaceRecognitionWindow faceRecognitionWindow = new FaceRecognitionWindow(selectedAccount);
                this.Hide();
                faceRecognitionWindow.Show();
                             
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
