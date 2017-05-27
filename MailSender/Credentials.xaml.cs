﻿using System;
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

namespace MailSender
{
    /// <summary>
    /// Interaction logic for Credentials.xaml
    /// </summary>
    public partial class Credentials : Window
    {
        public Credentials()
        {
            InitializeComponent();
            tbUser.Text = @"grosudmitri78@gmail.com";
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbUser.Text))
            {
                if (!string.IsNullOrWhiteSpace(pbPasswd.Password))
                {
                    var mainWind = new MainWindow(tbUser.Text, pbPasswd.SecurePassword);
                    mainWind.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Enter password!");
                }
            }
            else
            {
                MessageBox.Show("Enter username!");
            }
        }
    }
}