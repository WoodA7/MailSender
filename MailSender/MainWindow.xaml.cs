using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;
using S22.Imap;

namespace MailSender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ImapClient ImapClient { get; set; }
        public User User { get; set; }
        public SmtpClient SmtpClient { get; set; }
        public MainWindow(ImapClient imapClient, SmtpClient smtpClient, User user)
        {
            InitializeComponent();

            ImapClient = imapClient;
            User = user;
            SmtpClient = smtpClient;

            var uids = ImapClient.Search(SearchCondition.SentSince(DateTime.Today.AddDays(-10)));
            var messages = ImapClient.GetMessages(uids);
            dgMessages.ItemsSource = (from m in messages select new { m.From, m.Subject, m.Body }).ToList();
        }
        private void btnNewMessage_Click(object sender, RoutedEventArgs e)
        {
            var newMsgWindow = new NewMessage(User, SmtpClient);
            newMsgWindow.Show();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show(@"Exit application?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                e.Cancel = true;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
