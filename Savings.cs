using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking
{
    internal class Savings : Account
    {
        #region pconst
        public Savings(int an, string acn, string un, int ur, string upa, string ut, double uc, int up) : base(an, acn, un, ur, upa, ut, uc, up)
        {

        }
        #endregion

        #region methods
        public override void Withdraw(double amount)
        {
            if (amount <= 5000)
                base.Withdraw(amount);
            else
                throw new Exception("You can only withdraw a maximum of $5000 daily using a savings account.");
        }
        public override void Deposit(double amount)
        {
            if (amount <= userCash)
                base.Deposit(amount);
            else
                throw new Exception("Savings accounts cannot Deposit more than the available balance !");
        }
        #endregion
    }
}
