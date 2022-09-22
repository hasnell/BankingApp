using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Banking
{
    internal abstract class Account
    {

        #region properties
        public int accNo { get; set; }
        public string accName { get; set; }
        public string userName { get; set; }
        public int userRoute { get; set; }
        public string userPass { get; set; }
        public string userType { get; set; }
        public double userCash { get; set; }
        public int userPin {  get; set; }
        #endregion

        #region parameterized constructor
        public Account(int an, string acn, string un, int ur, string upa, string ut, double uc, int up)
        {
            if(up < 1000 || up > 9999)
            {
                throw new Exception("Pin must be 4 digits");
            }
            if(!(ut.Equals("checking"))&& !(ut.Equals("savings")) && !(ut.Equals("loan")) && !(ut.Equals("admin")))
            {
                throw new Exception("Please enter a valid account type");
            }

            accNo = an;
            accName = acn;
            userName = un;
            userRoute = ur;
            userPass = upa;
            userType = ut;
            userCash = uc;
            userPin = up;
        }
        #endregion

        #region methods
        public virtual void Withdraw(double amount)
        {
            if (amount <= userCash)
            {
                userCash -= amount;
                //Console.WriteLine("Withdraw Successful. You now have $" + userCash + " remaining.");
            }
            else
                throw new Exception("You cannot withdraw more money than you have!");
        }

        public virtual void Deposit(double amount)
        {
            if (amount > 0)
            {
                userCash += amount;
                //Console.WriteLine("Deposit Successful. You now have $" + userCash + " remaining.");
            }
            else
                throw new Exception("You cannot deposit a negative value!");
        }

        public string getAccType(string uName, string Pass)
        {
            SqlConnection con = new SqlConnection("server=DESKTOP-5QMM6M8\\HAILEY_SERVER;database=BankProjectDB;integrated security = true");
            SqlCommand cmd = new SqlCommand("select count(*) from Logininfo where userName=@uName and password=@pwd", con);
            return "";
        }

        #endregion  

    }
}
