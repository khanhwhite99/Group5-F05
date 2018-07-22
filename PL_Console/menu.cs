using System;
using System.Collections.Generic;
using BL;
using Persistence;

public class Menu {
    User userOnline;
    public Menu () {
        AppBl = new ApplicationBL ();
        UserBl = new UserBL ();
        PaymentBl = new PaymentBL();
        BillBl = new BillBL ();
        existProgram = false;
    }
    ApplicationBL AppBl;
    UserBL UserBl;
    PaymentBL PaymentBl;
    BillBL BillBl;
    bool existProgram;
    public void MainMenu () {
        while (true) {
            bool isExit = false;
            Console.Clear ();
            Console.WriteLine ("===Application Store===");
            Console.WriteLine ("1.Login");
            Console.WriteLine ("0.Exit");
            Console.Write ("#Choice: ");
            string Choice = Console.ReadLine ();
            switch (Choice) {
                case "1":
                    Login ();
                    break;
                case "0":
                    isExit = true;
                    break;
            }
            if (isExit == true || existProgram == true) break;
        }
    }
    void Login () {
        string username = "";
        string password = "";

        while (true) {
            string p = "";
            while (true) {
                int lengthP = p.Length;
                ConsoleKeyInfo key;
                Console.Clear ();
                Console.WriteLine ("===Login===");
                if (username.Length == 0) {
                    Console.Write ("Enter Username: ");
                    username = Console.ReadLine ();
                } else {
                    Console.WriteLine ("Enter Username: " + username);
                }
                if (password.Length == 0) {
                    Console.Write ("Enter Password: " + new string ('*', p.Length));
                    key = Console.ReadKey ();
                    if (key.Key == ConsoleKey.Enter) {
                        password = p;
                        break;
                    } else if (key.Key == ConsoleKey.Backspace) {
                        if (p.Length != 0)
                            p = p.Remove (p.Length - 1);
                    } else p += key.KeyChar;
                } else {
                    break;
                }
            }
            Console.WriteLine ("Enter Password: " + new string ('*', password.Length));
            Console.Write ("Do you want to continue?(Y/N) : ");
            string choice = Console.ReadLine ();
            if (choice == "Y" || choice == "y") {
                if (UserBl.CheckExistUserAndPass (username, password)) {
                    userOnline = UserBl.GetUserByUserName(username);
                    SystemGUI ();
                } else {
                    Console.Clear ();
                    Console.Write ("\nIncorrect Username or password!\n\nPress anykey to countinue...");
                    Console.ReadKey ();
                }
                break;
            } else if (choice == "N" || choice == "n")
                break;
        }
    }
    void SystemGUI () {
        while (true) {
            bool isExit = false;
            Console.Clear ();
            Console.WriteLine ("===Application Store===");
            Console.WriteLine ("1. Search Application");
            Console.WriteLine ("2. My Applications");
            Console.WriteLine ("3. History Trade");
            Console.WriteLine ("4. Log out");
            Console.WriteLine ("0. Exit");
            Console.Write ("#Choice ");
            string choice = Console.ReadLine ();
            switch (choice) {
                case "1":
                    Search ();
                    break;
                case "2":
                    DisplayMyApplications ();
                    break;
                case "3":
                    DisplayHistoryTrade ();
                    break;
                case "4":
                    isExit = true;
                    userOnline = null;
                    break;
                case "0":
                    isExit = true;
                    existProgram = true;
                    break;
            }
            if (isExit) break;
        }
    }
    void Search () {
        while (true) {
            bool isExit = false;
            string nameapp = "";
            ConsoleKeyInfo key;
            while (true) {
                Console.Clear ();
                Console.WriteLine ("===Search Application===");
                Console.Write ("Enter Name Application: ");
                Console.Write (nameapp);
                key = Console.ReadKey ();
                if (key.Key == ConsoleKey.Escape) {
                    isExit = true;
                    break;
                } else if (key.Key == ConsoleKey.Enter) {
                    break;
                } else if (key.Key == ConsoleKey.Backspace) {
                    nameapp = nameapp.Remove (nameapp.Length - 1);
                } else nameapp += key.KeyChar;
            }
            if (isExit == true) break;
            Console.Write ("\nSearching...");
            List<Application> listApp = AppBl.SearchApplicationByName (nameapp);
            if (listApp.Count <= 0) {
                Console.Clear ();
                Console.WriteLine ("Search Fail!\nNot Found Application...\n");
                Console.Write ("Press anykey to return...");
                Console.ReadLine ();
                break;
            } else {
                DisplayListApp (listApp);
                break;
            }
        }
    }
    void DisplayListApp (List<Application> listApp) {
        while (true) {
            int ichoice;
            Console.Clear ();
            int i = 1;
            foreach (var x in listApp) {
                Console.WriteLine ($"{i++}.{x.Name}");
            }
            Console.WriteLine ("0. Return");
            Console.Write ("\n#Choice: ");
            string schoice = Console.ReadLine ();
            if (int.TryParse (schoice, out ichoice)) {
                if(ichoice == 0)
                    break;
                else if (ichoice >= 1 && ichoice <= listApp.Count) {
                    DisplayAnApp (listApp[ichoice - 1]);
                    break;
                }
            }
        }

    }
    void DisplayAnApp (Application app) {
        while (true) {
            bool isExit = false;
            Console.Clear ();
            Console.WriteLine ($"Application Name : {app.Name}");
            Console.WriteLine ($"Kind             : {app.Kind}");
            Console.WriteLine ($"Description      : {app.Description}");
            Console.WriteLine ($"Publisher        : {app.Publisher}");
            Console.WriteLine ($"DatePublish      : {app.DatePublisher.Day+'/'+app.DatePublisher.Month+'/'+app.DatePublisher.Year}");
            Console.WriteLine ($"Price            : {app.Price} VND");
            Console.WriteLine ($"Size             : {app.Size} MB");
            Console.Write ("\n1. Buy\n0. Return\n\n#Choice: ");
            string choice = Console.ReadLine();
            if(choice == "1")
            {
                BuyApp(app);
            }
            else if(choice == "0")
                isExit = true;
            if(isExit)break;
        }
    }
    void BuyApp(Application app)
    {
        while(true)
        {
            bool isExit = false;
            int ichoice;
            Console.Clear();
            Console.WriteLine("===Buy Application===");
            Console.WriteLine ($"Application Name : {app.Name}");
            Console.WriteLine ($"Pay:             : {app.Price} VND");
            Console.WriteLine("Payment Method:\n");
            userOnline.ListPayment = PaymentBl.GetPayments(userOnline.User_ID);
            for(int i = 0; i < userOnline.ListPayment.Count; i++)
            {
                int p = i + 1;
                Console.WriteLine(p + ". " + userOnline.ListPayment[i].Name);
            }

            Console.WriteLine("0. Return");
            Console.Write("#Choice: ");
            string schoice = Console.ReadLine();
            if(schoice == "0")
                isExit = true;
            else if(int.TryParse(schoice, out ichoice))
            {
                if(ichoice >= 1 && ichoice <= userOnline.ListPayment.Count)
                {
                    Console.Clear();
                    Console.Write("We are checkking payment account...");
                    if(userOnline.ListPayment[ichoice-1].Name == "By Store")
                    {
                        if(PaymentBl.CheckPayment(userOnline.ListPayment[ichoice-1], app.Price))
                        {
                            Bill bill = new Bill()
                            {
                                App = app,
                                User = userOnline,
                                UnitPrice = app.Price
                            };
                            bool checkCreate = BillBl.CreateBill(bill);
                            if(checkCreate)
                            {
                                Console.WriteLine($"Buy {app.Name} !\nSuccessful\n\nPress anykey to return...");
                                Console.ReadKey();
                                isExit = true;
                            }
                            else
                            {
                                Console.WriteLine("Not Successful\n\nPress anykey to return...");
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Not Successful\n\nPress anykey to return...");
                            Console.ReadKey();
                        }
                    }
                }
            }

            if(isExit == true)break;
        }
    }
    void DisplayMyApplications () {
        Console.Clear();
        Console.WriteLine("=== App Bought ===");
        List<Application> listApp = UserBl.GetApplicationBoughtByUserID(userOnline.User_ID);
        foreach(var x in listApp)
        {
            Console.WriteLine(x.Name);
        }
        Console.Write("\nPress anykey to return ...");
        Console.ReadKey();
    }
    void DisplayHistoryTrade () {
        Console.Clear();
        Console.WriteLine("=== History Trade ===");
        List<Bill> listBill = BillBl.GetListBillByUserID(userOnline.User_ID);
        Console.WriteLine("BillID" + new string(' ', 4) + "Application" + new string(' ', 19) + "Price" + new string(' ', 7) + "Date");
        foreach(var x in listBill)
        {
            string print = x.Bill_ID + new string(' ',10 - x.Bill_ID.ToString().Length) + 
                           x.App.Name + new string(' ', 30 - x.App.Name.ToString().Length) + 
                           x.UnitPrice + new string(' ', 12 - x.UnitPrice.ToString().Length) + 
                           x.DateCreate.Day + "/" + x.DateCreate.Month + "/" + x.DateCreate.Year;
            Console.WriteLine(print);
        }
        Console.Write("\nPress anykey to return ...");
        Console.ReadKey();
    }
}