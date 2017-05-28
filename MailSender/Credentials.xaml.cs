using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using S22.Imap;

namespace MailSender
{
    /// <summary>
    /// Interaction logic for Credentials.xaml
    /// </summary>
    public partial class Credentials : Window
    {
        public struct Host
        {
            public string Name { get; set; }
            public int ImapPort { get; set; }
            public int SmtpPort { get; set; }
        }
        public List<Host> Hosts { get; set; }
        public Credentials()
        {
            Hosts = new List<Host>
            {
                new Host {Name = "gmail.com", SmtpPort = 587, ImapPort = 993},
                new Host {Name = "mail.ru", SmtpPort = 465, ImapPort = 993},
                new Host {Name = "yahoo.com", SmtpPort = 587, ImapPort = 993},
                new Host {Name = "rambler.ru", SmtpPort = 465, ImapPort = 993}
            };

            InitializeComponent();

            foreach (var h in Hosts)
            {
                cbHost.Items.Add(h.Name);
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbUser.Text))
            {
                MessageBox.Show("Username is empty!");
                return;
            }

            if (string.IsNullOrWhiteSpace(pbPasswd.Password))
            {
                MessageBox.Show("Password is empty!");
                return;
            }

            var selHost = new Host();
            foreach (var h in Hosts.Where(h => h.Name == cbHost.Text))
            {
                selHost = h;
            }

            var userName = tbUser.Text + @"@" + selHost.Name;

            var user = new User
            {
                Name = userName,
                Password = pbPasswd.SecurePassword
            };

            try
            {
                var imapClient = new ImapClient(@"imap." + selHost.Name, selHost.ImapPort, true);
                imapClient.Login(userName, pbPasswd.Password, AuthMethod.Auto);

                var smtpClient = new SmtpClient(@"smtp." + selHost.Name, selHost.SmtpPort);

                var mainWind = new MainWindow(imapClient, smtpClient, user);
                mainWind.Show();
                Close();
            }
            catch (InvalidCredentialsException)
            {
                MessageBox.Show(@"Invalid username or password!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
