using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPay.Objects
{
    class PasswordChanger
    {
        public PasswordChangeResult Change(string CurrentPassword, string NewPassword, string ConfirmNewPassword)
        {
            if (!WebPaySettings.FirstBoot)
            {
                if (!CheckCurrentPassword(CurrentPassword))
                    return PasswordChangeResult.CurrentPassworFail;
            }
            if (!CompareNewPasswordWithConfirm(NewPassword, ConfirmNewPassword))
                return PasswordChangeResult.ConfirmNewPasswordCompareFail;
            SetNewPassword(NewPassword);
            return PasswordChangeResult.OK;
        }

        private bool CheckCurrentPassword(string CurrentPassword)
        {
            PasswordCryptoManager PC = new PasswordCryptoManager();
            return PC.CheckPassword(CurrentPassword);
        }

        private bool CompareNewPasswordWithConfirm(string NewPassword, string ConfirmNewPassword)
        {
            return NewPassword == ConfirmNewPassword;
        }

        private void SetNewPassword(string NewPassword)
        {
            PasswordCryptoManager PM = new PasswordCryptoManager();
            PM.SetNewPassword(NewPassword);
        }

    }

    enum PasswordChangeResult
    {
        OK,
        CurrentPassworFail,
        ConfirmNewPasswordCompareFail
    }
}
