using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking
{
    internal class Loan : Account
    {
        #region pconst
        public Loan(int an, string acn, string un, int ur, string upa, string ut, double uc, int up) : base(an, acn, un, ur, upa, ut, uc, up)
        {

        }
        #endregion

        #region methods
        public override void Deposit(double amount)
        {
            amount *= .98;
            base.Deposit(amount);
            
        }
        #endregion

    }
}
