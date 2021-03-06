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
                try
                {
                    if (UserBl.CheckExistUserAndPass (username, password)) {
                        userOnline = UserBl.GetUserByUserName(username);
                        SystemGUI ();
                    } else {
                        Console.Clear ();
                        Console.Write ("\nIncorrect Username or password!\n\nPress anykey to countinue...");
                        Console.ReadKey ();
                    }
                }catch
                {
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
            List<Application> listApp = new List<Application>();
            while (true) {
                Console.Clear ();
                Console.WriteLine ("===Search Application===");
                Console.Write ("Enter Name Application: ");
                Console.WriteLine (nameapp);
                if(nameapp != "" && nameapp.Trim() != "")
                {
                    listApp = AppBl.SearchApplicationByName(nameapp);
                    if(listApp.Count > 0)
                    Console.WriteLine("\n------------------------");
                    foreach(var x in listApp)
                    {
                        Console.WriteLine(x.Name);
                    }
                }
                Console.SetCursorPosition(24 + nameapp.Length, 1);
                key = Console.ReadKey ();
                if (key.Key == ConsoleKey.Escape) {
                    isExit = true;
                    break;
                } else if (key.Key == ConsoleKey.Enter) {
                    if(listApp.Count <= 0)
                    {
                        Console.Write("Not found!");
                    }
                    else if(listApp.Count == 1)
                    {
                        DisplayAnApp(listApp[0]);
                        break;
                    }
                    else
                    {
                        DisplayListApp(listApp);
                        break;
                    }
                    
                } else if (key.Key == ConsoleKey.Backspace) {
                    if(nameapp.Length > 0)
                    nameapp = nameapp.Remove (nameapp.Length - 1);
                } else nameapp += key.KeyChar;
            }
            if (isExit == true) break;
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
            bool isOwn = false;
            string size;
            Console.Clear ();
            Console.WriteLine ($"Application Name : {app.Name}");
            Console.WriteLine ($"Kind             : {app.Kind}");
            Console.WriteLine ($"Description      : {app.Description}");
            Console.WriteLine ($"Publisher        : {app.Publisher}");
            Console.WriteLine ($"DatePublish      : {app.DatePublisher.Date.Day+"/"+app.DatePublisher.Date.Month+"/"+app.DatePublisher.Date.Year}");
            Console.WriteLine ($"Price            : {app.Price} VND");
            if(app.Size >= 100)size = (app.Size / 1000).ToString() + " GB";
            else size = app.Size.ToString() + " MB";
            Console.WriteLine ($"Size             : {size}");
            if(UserBl.GetApplicationBoughtByUserID(userOnline.User_ID).Contains(app))
            {
                Console.WriteLine("\nThis Application has OWNER!");
                isOwn = true;
            }
            else
            {
                Console.WriteLine ("\n1. Buy");
            }
            Console.Write("0. Return\n\n#Choice: ");
            string choice = Console.ReadLine();
            if(choice == "1" && isOwn == false)
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
                    if(userOnline.ListPayment[ichoice-1].Name == "By Store")
                    {
                        if(PaymentBl.CheckPayment(userOnline.ListPayment[ichoice-1], app.Price))
                        {
                            Console.Write("We are checkking payment account...");
                            Bill bill = new Bill()
                            {
                                App = app,
                                User = userOnline,
                                Payment = userOnline.ListPayment[ichoice-1],
                                UnitPrice = app.Price
                            };
                            try
                            {
                                bool checkCreate = BillBl.CreateBill(bill);
                                if(checkCreate)
                                {
                                    Console.Clear();
                                    Console.WriteLine($"Buy {app.Name} !\nSuccessful\n\nPress anykey to return...");
                                    Console.ReadKey();
                                    isExit = true;
                                }
                            }
                            catch
                            {
                                Console.Clear();
                                Console.WriteLine("Not Successful\n\nPress anykey to return...");
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Not Successful\n\nPress anykey to return...");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("This payment havent been updated!\n\nPress anykey to return...");
                        Console.ReadKey();
                    }
                }
            }

            if(isExit == true)break;
        }
    }
    void DisplayMyApplications () {
        while(true)
        {
            Console.Clear();
            Console.WriteLine("=== App Bought ===");
            List<Application> listApp = UserBl.GetApplicationBoughtByUserID(userOnline.User_ID);
            int i = 1;
            foreach(var x in listApp)
            {
                Console.WriteLine(i + ". " + x.Name);
                i++;
            }
            Console.Write("0. Return\n\n#Choice : ");
            string choice = Console.ReadLine();
            int ichoice;
            string size;
            if(int.TryParse(choice, out ichoice))
            {
                if(ichoice == 0)break;
                if(ichoice <= listApp.Count)
                {
                    Console.Clear ();
                    Console.WriteLine ($"Application Name : {listApp[ichoice-1].Name}");
                    Console.WriteLine ($"Kind             : {listApp[ichoice-1].Kind}");
                    Console.WriteLine ($"Description      : {listApp[ichoice-1].Description}");
                    Console.WriteLine ($"Publisher        : {listApp[ichoice-1].Publisher}");
                    Console.WriteLine ($"DatePublish      : {listApp[ichoice-1].DatePublisher.Date.Day+"/"+listApp[ichoice-1].DatePublisher.Date.Month+"/"+listApp[ichoice-1].DatePublisher.Date.Year}");
                    Console.WriteLine ($"Price            : {listApp[ichoice-1].Price} VND");
                    if(listApp[ichoice-1].Size >= 100)size = (listApp[ichoice-1].Size / 1000).ToString() + " GB";
                     else size = listApp[ichoice-1].Size.ToString() + " MB";
                     Console.WriteLine ($"Size             : {size}");
                }
                Console.Write("\nPress anykey to return...");
                Console.ReadKey();
            }
        }
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