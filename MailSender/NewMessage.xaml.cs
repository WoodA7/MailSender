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
        public User User { get; set; }
        public SmtpClient SmtpClient { get; set; }

        public NewMessage(User user, SmtpClient smtpClient)
        {
            InitializeComponent();
            User = user;
            SmtpClient = smtpClient;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            var from = new MailAddress(User.Name);
            var to = new MailAddress(tbTo.Text);
            var mail = new MailMessage(from, to)
            {
                Subject = tbSubject.Text,
                Body = new TextRange(rtbBody.Document.ContentStart, rtbBody.Document.ContentEnd).Text
            };

            try
            {
                SmtpClient.Credentials = new NetworkCredential(User.Name, User.Password);
                SmtpClient.EnableSsl = true;
                SmtpClient.Send(mail);
                MessageBox.Show(@"Your message was sent!");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
         }
    }
}
