using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPay.Objects
{
    class Authenticator
    {
        public bool CheckPassword(string Password)
        {
            PasswordCryptoManager PCM = new PasswordCryptoManager();
            return PCM.CheckPassword(Password);
        }
    }
}
