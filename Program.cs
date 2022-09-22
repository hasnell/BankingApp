
using Banking;
using System.Xml.Linq;

while (true)
{
    try
    {
        #region variables
        SQLCons logObj = new SQLCons();
        bool login = false;
        Account a = null;
        Account da = null;
        int choice = 0, accNo = -1;
        #endregion

        #region login
        Console.WriteLine("---------------Welcome to Banking---------------");
        while (!login)
        {
            Console.WriteLine("Enter your username:");
            string uname = Console.ReadLine();
            Console.WriteLine("Enter your password:");
            string upass = Console.ReadLine();
            login = logObj.checkLogin(uname, upass);
            if (login)
            {
                accNo = logObj.getAccNo(uname, upass);
                a = logObj.getAccInfo(uname, upass, accNo);
            }
            else
                Console.WriteLine("Invalid credentials, Try again.");
        }
        #endregion

        Console.Clear();
        Console.WriteLine("Welcome, " + a.accName + ", to your " + a.userType + " account!");

        #region admin account
        if (a.userType.Equals("admin"))
        {
            while (choice != 6)
            {
                try
                {
                    Console.WriteLine("What would you like to do?");
                    Console.WriteLine("1.) Show All Accounts.");
                    Console.WriteLine("2.) Search an Account.");
                    Console.WriteLine("3.) Add New Account.");
                    Console.WriteLine("4.) Delete an Account.");
                    Console.WriteLine("5.) Update an Account.");
                    Console.WriteLine("6.) Log Out.");
                    choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            #region show all accounts
                            Console.Clear();
                            logObj.displayAllInfo();
                            #endregion
                            break;

                        case 2:
                            #region search account
                            Console.Clear();
                            try
                            {
                                Console.WriteLine("Do you want to search by:");
                                Console.WriteLine("1.) Account Number");
                                Console.WriteLine("2.) Customer Name");
                                int aSearchType = Convert.ToInt32(Console.ReadLine());
                                if (aSearchType == 1)
                                {
                                    Console.WriteLine("Enter the account number of the account you wish to find:");
                                    int number2 = Convert.ToInt32(Console.ReadLine());
                                    logObj.findAccount(number2);
                                }
                                else if (aSearchType == 2)
                                {
                                    Console.WriteLine("Enter part of the customer name for the account(s) you wish to find:");
                                    string aCustName = "%";
                                    aCustName += Console.ReadLine();
                                    aCustName += "%";
                                    logObj.displayLikeInfo(aCustName);
                                }
                                else
                                    throw new Exception("Please input a valid number.");
                            }
                            catch (Exception es)
                            {
                                Console.WriteLine(es.Message);
                            }
                            #endregion
                            break;

                        case 3:
                            #region create account
                            Console.Clear();
                            Console.WriteLine("Please enter information for the account you would like to create:");
                            Console.WriteLine("Name of User:");
                            string createName = Console.ReadLine();
                            Console.WriteLine("Type of Account:");
                            string createType = Console.ReadLine();
                            Console.WriteLine("Account Routing Number:");
                            int createRoute = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Account Balance:");
                            double createBal = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Account Pin:");
                            int createPin = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Account Username:");
                            string createUName = Console.ReadLine();
                            Console.WriteLine("Account Password:");
                            string createPass = Console.ReadLine();
                            int createAccNo = logObj.newAccNo() + 1;
                            if (createType.Equals("checking"))
                                da = new Checking(createAccNo, createName, createUName, createRoute, createPass, createType, createBal, createPin);
                            else if (createType.Equals("savings"))
                                da = new Savings(createAccNo, createName, createUName, createRoute, createPass, createType, createBal, createPin);
                            else if (createType.Equals("loan"))
                                da = new Loan(createAccNo, createName, createUName, createRoute, createPass, createType, createBal, createPin);
                            else if (createType.Equals("admin"))
                            {
                                createRoute = 0;
                                createBal = 0;
                                da = new Admin(createAccNo, createName, createUName, createRoute, createPass, createType, createBal, createPin);
                            }
                            else
                            {
                                Console.WriteLine("Please enter a valid account type.");
                                break;
                            }
                            logObj.createNewAcc(createAccNo, createName, createType, createRoute, createBal, createPin, createUName, createPass);
                            #endregion
                            break;

                        case 4:
                            #region delete account
                            Console.Clear();
                            Console.WriteLine("Enter the account number of the account you wish to delete:");
                            int aDelete = Convert.ToInt32(Console.ReadLine());
                            logObj.accExists(aDelete);
                            da = logObj.getAltAccInfo(aDelete);
                            Console.WriteLine("Are you sure you would like to delete " + da.accName + "'s account?");
                            Console.WriteLine("1.) Yes");
                            Console.WriteLine("2.) No");
                            int aDelSure = Convert.ToInt32(Console.ReadLine());
                            if (aDelSure != 1)
                                break;
                            if (aDelete == accNo)
                            {
                                Console.WriteLine("Deleting your own account will log you out permanantly, would you like to proceed?");
                                Console.WriteLine("1.) Yes");
                                Console.WriteLine("2.) No");
                                int deleteChoice = Convert.ToInt32(Console.ReadLine());
                                if (deleteChoice != 1)
                                    break;
                                else
                                    choice = 6;
                            }
                            logObj.delAccount(aDelete);
                            #endregion
                            break;

                        case 5:
                            #region edit account
                            Console.Clear();
                            Console.WriteLine("Enter the account number of the account you wish to edit:");
                            int editAccNo = Convert.ToInt32(Console.ReadLine());
                            logObj.accExists(editAccNo);
                            da = logObj.getAltAccInfo(editAccNo);
                            Console.WriteLine("Are you sure you would like to edit " + da.accName + "'s account?");
                            Console.WriteLine("1.) Yes");
                            Console.WriteLine("2.) No");
                            int aEditSure = Convert.ToInt32(Console.ReadLine());
                            if (aEditSure != 1)
                                break;
                            if (editAccNo == accNo)
                            {
                                Console.WriteLine("Editing your own account may cause major issues, would you like to proceed?");
                                Console.WriteLine("1.) Yes");
                                Console.WriteLine("2.) No");
                                int editChoice = Convert.ToInt32(Console.ReadLine());
                                if (editChoice != 1)
                                    break;
                            }
                            Console.WriteLine("What would you like to edit?");
                            Console.WriteLine("1.) Account Name");
                            Console.WriteLine("2.) Account Type");
                            Console.WriteLine("3.) Routing Number");
                            Console.WriteLine("4.) Account Balance");
                            Console.WriteLine("5.) Account Pin");
                            Console.WriteLine("6.) Account Username");
                            Console.WriteLine("7.) Account Password");
                            Console.WriteLine("8.) Quit");
                            int number5 = Convert.ToInt32(Console.ReadLine());
                            switch (number5)
                            {
                                case 1:
                                    Console.WriteLine("What would you like to change the account name to?");
                                    string editName = Console.ReadLine();
                                    logObj.accEdit(editAccNo, editName, -1, 1);
                                    Console.WriteLine("Update successful!");
                                    break;
                                case 2:
                                    Console.WriteLine("What would you like to change the account type to?");
                                    string editType = Console.ReadLine();
                                    logObj.accEdit(editAccNo, editType, -1, 2);
                                    Console.WriteLine("Update successful!");
                                    break;
                                case 3:
                                    Console.WriteLine("What would you like to change the routing number to?");
                                    int editRoute = Convert.ToInt32(Console.ReadLine());
                                    logObj.accEdit(editAccNo, null, editRoute, 3);
                                    Console.WriteLine("Update successful!");
                                    break;
                                case 4:
                                    Console.WriteLine("What would you like to change the account balance to?");
                                    double editBal = Convert.ToDouble(Console.ReadLine());
                                    logObj.accEdit(editAccNo, null, editBal, 4);
                                    Console.WriteLine("Update successful!");
                                    break;
                                case 5:
                                    Console.WriteLine("What would you like to change the account pin to?");
                                    int editPin = Convert.ToInt32(Console.ReadLine());
                                    logObj.accEdit(editAccNo, null, editPin, 5);
                                    Console.WriteLine("Update successful!");
                                    break;
                                case 6:
                                    Console.WriteLine("What would you like to change the account username to?");
                                    string editUName = Console.ReadLine();
                                    logObj.accEdit(editAccNo, editUName, -1, 6);
                                    Console.WriteLine("Update successful!");
                                    break;
                                case 7:
                                    Console.WriteLine("What would you like to change the account password to?");
                                    string editPass = Console.ReadLine();
                                    logObj.accEdit(editAccNo, editPass, -1, 7);
                                    Console.WriteLine("Update successful!");
                                    break;
                                case 8:
                                    break;
                                default:
                                    break;
                            }
                            #endregion
                            break;
                        case 6:
                            Console.Clear();
                            Console.WriteLine("Goodbye.");
                            break;
                        default:
                            Console.WriteLine("Please enter a valid number.");
                            break;
                    }
                }
                catch(Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }


            }
        }
        #endregion
        else
        {
            while (choice != 6)
            {
                try
                {
                    Console.WriteLine("What would you like to do?");
                    Console.WriteLine("1.) Show Account Details.");
                    Console.WriteLine("2.) Change Password.");
                    Console.WriteLine("3.) Withdraw Money.");
                    Console.WriteLine("4.) Deposit Money.");
                    Console.WriteLine("5.) Transfer Money.");
                    Console.WriteLine("6.) Log Out.");
                    choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            #region show account details
                            Console.Clear();
                            Console.WriteLine("Displaying Account Information\n");
                            Console.WriteLine("Account Number: " + a.accNo);
                            Console.WriteLine("Account Name: " + a.accName);
                            Console.WriteLine("Routing Number: " + a.userRoute);
                            Console.WriteLine("Account Type: " + a.userType);
                            Console.WriteLine("Account Balance: $" + a.userCash);
                            Console.WriteLine("Account Pin: " + a.userPin);
                            Console.WriteLine("Account Username: " + a.userName);
                            Console.WriteLine("Account Password: " + a.userPass + "\n");
                            #endregion
                            break;
                        case 2:
                            #region change pass
                            Console.Clear();
                            Console.WriteLine("What would you like to change your password to?");
                            string cEditPass = Console.ReadLine();
                            a.userPass = cEditPass;
                            logObj.accEdit(a.accNo, cEditPass, -1, 7);
                            Console.WriteLine("Password Updated.");
                            #endregion
                            break;
                        case 3:
                            #region withdraw funds
                            try
                            {
                                Console.Clear();
                                Console.WriteLine("How much would you like to withdraw?");
                                double cWithdraw = Convert.ToDouble(Console.ReadLine());
                                a.Withdraw(cWithdraw);
                                logObj.accEdit(a.accNo, null, a.userCash, 4);
                                Console.WriteLine("Withdraw Successful. You now have $" + a.userCash + " remaining.");
                            }
                            catch (Exception es)
                            {
                                Console.WriteLine(es.Message);
                            }
                            #endregion
                            break;
                        case 4:
                            #region deposit funds
                            try
                            {
                                Console.Clear();
                                Console.WriteLine("How much would you like to deposit?");
                                double cDepo = Convert.ToDouble(Console.ReadLine());
                                a.Deposit(cDepo);
                                logObj.accEdit(a.accNo, null, a.userCash, 4);
                                Console.WriteLine("Deposit Successful. You now have $" + a.userCash + " remaining.");
                            }
                            catch (Exception es)
                            {
                                Console.WriteLine(es.Message);
                            }
                            #endregion
                            break;
                        case 5:
                            #region transfer funds
                            try
                            {
                                Console.Clear();
                                bool flag = false;
                                Console.WriteLine("What is the account number of the user you would like to send money to?");
                                int cTransAccNo = Convert.ToInt32(Console.ReadLine());
                                logObj.accExists(cTransAccNo);
                                da = logObj.getAltAccInfo(cTransAccNo);
                                Console.WriteLine("Are you sure you would like to transfer funds to " + da.accName + "?");
                                Console.WriteLine("1.) Yes");
                                Console.WriteLine("2.) No");
                                int cTransSure = Convert.ToInt32(Console.ReadLine());
                                if (cTransSure != 1)
                                    break;
                                Console.WriteLine("How much would you like to transfer?");
                                double cTransTot = Convert.ToDouble(Console.ReadLine());
                                a.Withdraw(cTransTot);
                                da.Deposit(cTransTot);
                                logObj.accEdit(a.accNo, null, a.userCash, 4);
                                logObj.accEdit(da.accNo, null, da.userCash, 4);
                                Console.WriteLine("Transfer Successful. You now have $" + a.userCash + " remaining.");

                            }
                            catch (Exception es)
                            {
                                Console.WriteLine(es.Message);
                                Console.WriteLine("");
                            }
                            #endregion
                            break;
                        case 6:
                            Console.Clear();
                            Console.WriteLine("Thank you for banking with us. Goodbye.");
                            break;
                        default:
                            Console.WriteLine("Please enter a valid number.");
                            break;
                    }
                }
                catch(Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }

            }
        }
        Console.ReadLine();
        Console.Clear();
    }
    catch (Exception e)
    {
        Console.Clear();
        Console.WriteLine("The following error has occured:");
        Console.WriteLine(e.Message);
        Console.WriteLine("Please try again.");
    }

}

    




