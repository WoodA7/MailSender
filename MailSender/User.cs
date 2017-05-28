using System.Collections.Specialized;
using System.Security;

namespace MailSender
{
    public struct User
    {
        public string Name { get; set; }
        public SecureString Password { get; set; }
    }
}