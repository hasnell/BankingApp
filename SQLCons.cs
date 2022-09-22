using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking
{
    internal class SQLCons
    {
        #region login check
        public bool checkLogin(string uName, string uPass)
        {
            SqlConnection con = new SqlConnection("server=DESKTOP-5QMM6M8\\HAILEY_SERVER;database=BankProjectDB;integrated security = true");
            SqlCommand cmd = new SqlCommand("select count(*) from Logininfo where userName=@uName and password=@pwd", con);

            cmd.Parameters.AddWithValue("@uName", uName);
            cmd.Parameters.AddWithValue("@pwd", uPass);
            con.Open();
            int result = (int)cmd.ExecuteScalar();
            con.Close();
            if (result == 1)
                return true;
            else 
                return false;
        }
        #endregion

        #region get info from logged in account
        public Account getAccInfo(string uName, string uPass, int aN)
        {
            SqlConnection con = new SqlConnection("server=DESKTOP-5QMM6M8\\HAILEY_SERVER;database=BankProjectDB;integrated security = true");
            SqlCommand cmd = new SqlCommand("select * from accountInfo where accNo=@an", con);

            cmd.Parameters.AddWithValue("@an", aN);
            con.Open();
            try
            {
                SqlDataReader readARow = cmd.ExecuteReader();
                if (readARow.Read())
                {
                    Account acc = null;

                    int accNo = Convert.ToInt32(readARow[0]);
                    string accName = readARow[1].ToString();
                    string accType = readARow[2].ToString();
                    int accRoute = Convert.ToInt32(readARow[3]);
                    double accBal = Convert.ToInt32(readARow[4]);
                    int accPin = Convert.ToInt32(readARow[5]);

                    if (accType.Equals("checking"))
                        acc = new Checking(accNo, accName, uName, accRoute, uPass, accType, accBal, accPin);
                    else if(accType.Equals("savings"))
                        acc = new Savings(accNo, accName, uName, accRoute, uPass, accType, accBal, accPin);
                    else if(accType.Equals("loan"))
                        acc = new Loan(accNo, accName, uName, accRoute, uPass, accType, accBal, accPin);
                    else if(accType.Equals("admin"))
                        acc = new Admin(accNo, accName, uName, accRoute, uPass, accType, accBal, accPin);
                    else 
                        throw new Exception("Unexpected Error occured, please try again.");

                    return acc;

                }
                else
                {
                    throw new Exception("Unexpected Error occured, please try again.");
                }
            }
            catch (Exception es)
            {
                Console.WriteLine(es.Message);
            }

            con.Close();
            return null;
        }
        #endregion

        #region get account number
        public int getAccNo(string uName, string uPass)
        {
            SqlConnection con = new SqlConnection("server=DESKTOP-5QMM6M8\\HAILEY_SERVER;database=BankProjectDB;integrated security = true");
            SqlCommand cmd = new SqlCommand("select accNo from LoginInfo where userName=@uName and password=@pwd", con);

            cmd.Parameters.AddWithValue("@uName", uName);
            cmd.Parameters.AddWithValue("@pwd", uPass);
            con.Open();
            try
            {
                SqlDataReader readARow = cmd.ExecuteReader();
                if (readARow.Read())
                {
                    int accNo = Convert.ToInt32(readARow[0]);

                    con.Close();
                    return accNo;

                }
                else
                {
                    throw new Exception("something happened");
                }
            }
            catch (Exception es)
            {
                Console.WriteLine(es.Message);
            }

            con.Close();
            return -1;
        }
        #endregion

        #region get info from all accounts
        public void displayAllInfo()
        {
            SqlConnection con = new SqlConnection("server=DESKTOP-5QMM6M8\\HAILEY_SERVER;database=BankProjectDB;integrated security = true");
            SqlCommand cmd = new SqlCommand("select * from loginInfo join accountInfo on accountInfo.accNo = logininfo.accNo", con);
            con.Open();
            try
            {
                SqlDataReader readARow = cmd.ExecuteReader();
                while (readARow.Read())
                {


                    int accNo = Convert.ToInt32(readARow[0]);
                    string userName = readARow[1].ToString();
                    string password = readARow[2].ToString();
                    string accName = readARow[4].ToString();
                    string accType = readARow[5].ToString();
                    int accRoute = Convert.ToInt32(readARow[6]);
                    double accBal = Convert.ToInt32(readARow[7]);
                    int accPin = Convert.ToInt32(readARow[8]);

                    Console.WriteLine("Account number: " + accNo);
                    Console.WriteLine("Account username: " + userName);
                    Console.WriteLine("Account password: " + password);
                    Console.WriteLine("Name of user: " + accName);
                    Console.WriteLine("Type of account: " + accType);
                    Console.WriteLine("Routing number: " + accRoute);
                    Console.WriteLine("Account Balance: $" + accBal);
                    Console.WriteLine("Account pin: " + accPin + "\n");



                }
                
            }
            catch (Exception es)
            {
                Console.WriteLine(es.Message);
            }

            con.Close();
        }
        #endregion

        #region get info from single account
        public void findAccount(int aN)
        {
            SqlConnection con = new SqlConnection("server=DESKTOP-5QMM6M8\\HAILEY_SERVER;database=BankProjectDB;integrated security = true");
            SqlCommand cmd = new SqlCommand("select * from loginInfo join accountInfo on accountInfo.accNo=@aN and loginInfo.accNo=@aN", con);
            cmd.Parameters.AddWithValue("@aN", aN);
            con.Open();
                SqlDataReader readARow = cmd.ExecuteReader();
                bool passed = false;
                while (readARow.Read())
                {
                    passed = true;
                    int accNo = Convert.ToInt32(readARow[0]);
                    string userName = readARow[1].ToString();
                    string password = readARow[2].ToString();
                    string accName = readARow[4].ToString();
                    string accType = readARow[5].ToString();
                    int accRoute = Convert.ToInt32(readARow[6]);
                    double accBal = Convert.ToInt32(readARow[7]);
                    int accPin = Convert.ToInt32(readARow[8]);

                    Console.WriteLine("Account number: " + accNo);
                    Console.WriteLine("Account username: "+ userName);
                    Console.WriteLine("Account password: " + password);
                    Console.WriteLine("Name of user: " + accName);
                    Console.WriteLine("Type of account: " + accType);
                    Console.WriteLine("Routing number: " + accRoute);
                    Console.WriteLine("Account Balance: $" + accBal);
                    Console.WriteLine("Account pin: " + accPin + "\n");
                }
                if (!passed)
                    throw new Exception("Account does not exist!");

            con.Close();
        }
        #endregion

        #region get new unique account number
        public int newAccNo()
        {
            SqlConnection con = new SqlConnection("server=DESKTOP-5QMM6M8\\HAILEY_SERVER;database=BankProjectDB;integrated security = true");
            SqlCommand cmd = new SqlCommand("select max(accNo) from Logininfo", con);
            con.Open();
            int result = (int)cmd.ExecuteScalar();
            con.Close();
            return result;
        }
        #endregion

        #region create new account
        public void createNewAcc(int accNo, string accName, string accType, int accRoute, double accBal, int accPin, string userName, string password)
        {
            SqlConnection con = new SqlConnection("server=DESKTOP-5QMM6M8\\HAILEY_SERVER;database=BankProjectDB;integrated security = true");
            SqlCommand cmd = new SqlCommand("insert into accountInfo values(@accNo, @accName, @accType, @accRoute, @accBal, @accPin)", con);
            SqlCommand cmd2 = new SqlCommand("insert into loginInfo values(@accNo, @userName, @password)", con);
            SqlCommand cmd3 = new SqlCommand("delete from loginInfo where accNo = @accNo", con);
            SqlCommand cmd4 = new SqlCommand("delete from accountInfo where accNo = @accNo", con);

            cmd.Parameters.AddWithValue("@accNo", accNo);
            cmd.Parameters.AddWithValue("@accName", accName);
            cmd.Parameters.AddWithValue("@accType", accType);
            cmd.Parameters.AddWithValue("@accRoute", accRoute);
            cmd.Parameters.AddWithValue("@accBal", accBal);
            cmd.Parameters.AddWithValue("@accPin", accPin);
            cmd2.Parameters.AddWithValue("@accNo", accNo);
            cmd2.Parameters.AddWithValue("@userName", userName);
            cmd2.Parameters.AddWithValue("@password", password);
            cmd3.Parameters.AddWithValue("@accNo", accNo);
            cmd4.Parameters.AddWithValue("@accNo", accNo);
            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                Console.WriteLine("Account Created.");
            }
            catch(Exception e)
            {
                try
                {
                    cmd3.ExecuteNonQuery();
                    cmd4.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.WriteLine(e.Message);
            }
            con.Close();
            


        }
        #endregion

        #region delete existing account
        public void delAccount(int accNo)
        {
            SqlConnection con = new SqlConnection("server=DESKTOP-5QMM6M8\\HAILEY_SERVER;database=BankProjectDB;integrated security = true");
            SqlCommand cmd = new SqlCommand("delete from loginInfo where accNo = @accNo", con);
            SqlCommand cmd2 = new SqlCommand("delete from accountInfo where accNo = @accNo", con);
            cmd.Parameters.AddWithValue("@accNo", accNo);
            cmd2.Parameters.AddWithValue("@accNo", accNo);
            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                Console.WriteLine("Account Deleted.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
                con.Close();
        }
        #endregion

        #region edit existing account
        public void accEdit(int accNo, string stEdit, double noEdit, int toEdit)
        {
            SqlConnection con = new SqlConnection("server=DESKTOP-5QMM6M8\\HAILEY_SERVER;database=BankProjectDB;integrated security = true");
            SqlCommand cmd = new SqlCommand("update accountInfo set accName = @stEdit where accNo = @accNo", con);
            SqlCommand cmd2 = new SqlCommand("update accountInfo set accType = @stEdit where accNo = @accNo", con);
            SqlCommand cmd3 = new SqlCommand("update accountInfo set accRoute = @noEdit where accNo = @accNo", con);
            SqlCommand cmd4 = new SqlCommand("update accountInfo set accBal = @noEdit where accNo = @accNo", con);
            SqlCommand cmd5 = new SqlCommand("update accountInfo set accPin = @noEdit where accNo = @accNo", con);
            SqlCommand cmd6 = new SqlCommand("update loginInfo set userName = @stEdit where accNo = @accNo", con);
            SqlCommand cmd7 = new SqlCommand("update loginInfo set password = @stEdit where accNo = @accNo", con);
            #region params
            cmd.Parameters.AddWithValue("@accNo", accNo);
            cmd.Parameters.AddWithValue("@stEdit", stEdit);
            cmd2.Parameters.AddWithValue("@accNo", accNo);
            cmd2.Parameters.AddWithValue("@stEdit", stEdit);
            cmd3.Parameters.AddWithValue("@accNo", accNo);
            cmd3.Parameters.AddWithValue("@noEdit", noEdit);
            cmd4.Parameters.AddWithValue("@accNo", accNo);
            cmd4.Parameters.AddWithValue("@noEdit", noEdit);
            cmd5.Parameters.AddWithValue("@accNo", accNo);
            cmd5.Parameters.AddWithValue("@noEdit", noEdit);
            cmd6.Parameters.AddWithValue("@accNo", accNo);
            cmd6.Parameters.AddWithValue("@stEdit", stEdit);
            cmd7.Parameters.AddWithValue("@accNo", accNo);
            cmd7.Parameters.AddWithValue("@stEdit", stEdit);
            #endregion

            con.Open();
            try
            {
                switch (toEdit)
                {
                    case 1:
                        cmd.ExecuteNonQuery();
                        break;
                    case 2:
                        cmd2.ExecuteNonQuery();
                        break;
                    case 3:
                        cmd3.ExecuteNonQuery();
                        break;
                    case 4:
                        cmd4.ExecuteNonQuery();
                        break;
                    case 5:
                        cmd5.ExecuteNonQuery();
                        break;
                    case 6:
                        cmd6.ExecuteNonQuery();
                        break;
                    case 7:
                        cmd7.ExecuteNonQuery();
                        break;
                    default:
                        break;

                }
                
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        #endregion

        #region find if acc exists
        public void accExists(int aN)
        {
            SqlConnection con = new SqlConnection("server=DESKTOP-5QMM6M8\\HAILEY_SERVER;database=BankProjectDB;integrated security = true");
            SqlCommand cmd = new SqlCommand("select * from loginInfo join accountInfo on accountInfo.accNo=@aN and loginInfo.accNo=@aN", con);
            cmd.Parameters.AddWithValue("@aN", aN);
            con.Open();
            SqlDataReader readARow = cmd.ExecuteReader();
            bool passed = false;
            while (readARow.Read())
            {
                passed = true;
                
            }
            if (!passed)
                throw new Exception("Account does not exist!");

            con.Close();
        }
        #endregion

        #region get info from other account
        public Account getAltAccInfo(int aN)
        {
            SqlConnection con = new SqlConnection("server=DESKTOP-5QMM6M8\\HAILEY_SERVER;database=BankProjectDB;integrated security = true");
            SqlCommand cmd = new SqlCommand("select * from loginInfo join accountInfo on accountInfo.accNo=@aN and loginInfo.accNo=@aN", con);

            cmd.Parameters.AddWithValue("@an", aN);
            con.Open();
            try
            {
                SqlDataReader readARow = cmd.ExecuteReader();
                bool passed = false;
                if (readARow.Read())
                {
                    Account acc = null;
                    passed = true;
                    int accNo = Convert.ToInt32(readARow[0]);
                    string userName = readARow[1].ToString();
                    string password = readARow[2].ToString();
                    string accName = readARow[4].ToString();
                    string accType = readARow[5].ToString();
                    int accRoute = Convert.ToInt32(readARow[6]);
                    double accBal = Convert.ToInt32(readARow[7]);
                    int accPin = Convert.ToInt32(readARow[8]);

                    if (accType.Equals("checking"))
                        acc = new Checking(accNo, accName, userName, accRoute, password, accType, accBal, accPin);
                    else if (accType.Equals("savings"))
                        acc = new Savings(accNo, accName, userName, accRoute, password, accType, accBal, accPin);
                    else if (accType.Equals("loan"))
                        acc = new Loan(accNo, accName, userName, accRoute, password, accType, accBal, accPin);
                    else if (accType.Equals("admin"))
                        acc = new Admin(accNo, accName, userName, accRoute, password, accType, accBal, accPin);
                    else
                        throw new Exception("Unexpected Error occured, please try again.");

                    if(!passed)
                        throw new Exception("Account does not exist!");

                    return acc;

                }
                else
                {
                    throw new Exception("Unexpected Error occured, please try again.");
                }
            }
            catch (Exception es)
            {
                Console.WriteLine(es.Message);
            }

            con.Close();
            return null;
        }
        #endregion

        #region get info from accounts like name
        public void displayLikeInfo(string inAccName)
        {
            SqlConnection con = new SqlConnection("server=DESKTOP-5QMM6M8\\HAILEY_SERVER;database=BankProjectDB;integrated security = true");
            SqlCommand cmd = new SqlCommand("select * from loginInfo join accountInfo on accountInfo.accNo = logininfo.accNo and accName like @accName", con);
            cmd.Parameters.AddWithValue("@accName", inAccName);
            con.Open();
            try
            {
                SqlDataReader readARow = cmd.ExecuteReader();
                while (readARow.Read())
                {


                    int accNo = Convert.ToInt32(readARow[0]);
                    string userName = readARow[1].ToString();
                    string password = readARow[2].ToString();
                    string accName = readARow[4].ToString();
                    string accType = readARow[5].ToString();
                    int accRoute = Convert.ToInt32(readARow[6]);
                    double accBal = Convert.ToInt32(readARow[7]);
                    int accPin = Convert.ToInt32(readARow[8]);

                    Console.WriteLine("Account number: " + accNo);
                    Console.WriteLine("Account username: " + userName);
                    Console.WriteLine("Account password: " + password);
                    Console.WriteLine("Name of user: " + accName);
                    Console.WriteLine("Type of account: " + accType);
                    Console.WriteLine("Routing number: " + accRoute);
                    Console.WriteLine("Account Balance: $" + accBal);
                    Console.WriteLine("Account pin: " + accPin + "\n");



                }

            }
            catch (Exception es)
            {
                Console.WriteLine(es.Message);
            }

            con.Close();
        }
        #endregion
    }
}
