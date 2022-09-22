using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking
{
    internal class Checking : Account
    {
        #region pconst
        public Checking(int an, string acn, string un, int ur, string upa, string ut, double uc, int up) : base(an, acn, un, ur, upa, ut, uc, up)
        {

        }
        #endregion

        #region methods
        public override void Withdraw(double amount)
        {
            if(amount <= 20000)
               base.Withdraw(amount);
            else
               throw new Exception("You can only withdraw a maximum of $20000 daily using a checking account.");
        }
        public override void Deposit(double amount)
        {
            if (amount <= 1.6*userCash)
                base.Deposit(amount);
            else
                throw new Exception("Checking cannot deposit more than 160% of available balance!");
        }
        #endregion
    }
}
