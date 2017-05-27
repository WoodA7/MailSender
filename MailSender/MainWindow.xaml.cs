using System;
using System.Collections.Generic;
using System.Linq;
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
        public MainWindow(string userName, SecureString passwd)
        {
            InitializeComponent();

            using (var imapClient = new ImapClient(@"imap.gmail.com", 993, true))
            {
                imapClient.Login(userName, passwd.ToString(), AuthMethod.Auto);
                var uids = imapClient.Search(SearchCondition.All());
                var messages = imapClient.GetMessages(uids);
                dgMessages.ItemsSource = (from m in messages select new { m.From, m.Subject, m.Body }).ToList();
            }
        }

        private void btnNewMessage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(@"Exit application?", "", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                Close();
        }
    }
}
