using System;
using System.Collections.Generic;
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
using Emgu.CV.Structure;
using Emgu.CV;
using System.Drawing;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using Emgu.CV.Features2D;
using System.Resources;
using System.Windows.Interop;
using System.Threading;
using ImageRecognition.Model;
using System.Diagnostics;
using ImageRecognition.UIWindows;
using ImageRecognition.Repository;
using ImageRecognition.Data;

namespace ImageRecognition
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();
        }


       


        private void OpenManagedPasswordsWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            User userTesting = UserRepository.GetUserWithAccounts("ChangedTestUser");
            List<Account> listOfDisplayAccs = userTesting.ManagedAccounts;
            Debug.WriteLine(userTesting.ManagedAccounts);
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            
        }

        
    }
}
