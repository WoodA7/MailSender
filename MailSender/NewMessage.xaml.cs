using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security;
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

namespace MailSender
{
    /// <summary>
    /// Interaction logic for NewMessage.xaml
    /// </summary>
    public partial class NewMessage : Window
    {
        public string Username { get; set; }
        public SecureString Passwd { get; set; }

        public NewMessage(string userName, SecureString passwd)
        {
            this.Username = userName;
            this.Passwd = passwd;
            InitializeComponent();
            tbFrom.Text = userName;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            var from = new MailAddress(tbFrom.Text);
            var to = new MailAddress(tbTo.Text);
            var mail = new MailMessage(from, to);

            mail.Subject = tbSubject.Text;
            mail.Body = new TextRange(rtbBody.Document.ContentStart, rtbBody.Document.ContentEnd).Text;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587
            };

            smtp.Credentials = new NetworkCredential(Username, Passwd);
            smtp.EnableSsl = true;
            smtp.Send(mail);

        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            var msgsWind = new MainWindow(Username, Passwd);
            msgsWind.Show();
        }
    }
}
