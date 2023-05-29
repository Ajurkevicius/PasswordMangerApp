﻿using ImageRecognition.State;
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

namespace ImageRecognition.UIWindows
{
    /// <summary>
    /// Interaction logic for UserSettingWindow.xaml
    /// </summary>
    public partial class UserSettingWindow : Window
    {
        public UserSettingWindow()
        {
            InitializeComponent();
            this.UserName.Content = CurrentUser.GetCurrentUserName();
        }

        private void ChangeMasterPasswordClicked(object sender, RoutedEventArgs e)
        {
            OldPassword oldPassword = new OldPassword();
            oldPassword.Show();
            this.Hide();
        }

        private void ChangePicturesClicked(object sender, RoutedEventArgs e)
        {
            FaceRecognitionWindow faceRecognitionWindow = new FaceRecognitionWindow(true);
            this.Hide();
            faceRecognitionWindow.Show();
            
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            MainMenuWindow mainMenuWindow = new MainMenuWindow();
            this.Close();
            mainMenuWindow.Show();
        }
    }
}
