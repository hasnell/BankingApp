using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking
{
    internal class Admin : Account
    {
        #region pconst
        public Admin(int an, string acn, string un, int ur, string upa, string ut, double uc, int up) : base(an, acn, un, ur, upa, ut, uc, up)
        {

        }
        #endregion
    }
}
