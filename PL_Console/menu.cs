using System;
using System.Collections.Generic;
using BL;
using Persistence;

public class Menu {
    public Menu () {
        AppBl = new ApplicationBL();
        UserBl = new UserBL ();
        BillBl = new BillBL ();
        existProgram = false;
    }
    ApplicationBL AppBl;
    UserBL UserBl;
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
            if (isExit == true|| existProgram ==true) break;
        }
    }
    void Login () {
        string username = null;
        string password = "";
        ConsoleKeyInfo key;

        while (true) {
            while (true) {
                Console.Clear ();
                Console.WriteLine ("=== Login ===");
                Console.Write ("Enter UserName : ");
                if (username == null || username == "" || username == new string (' ', username.Length))
                    username = Console.ReadLine ();
                else
                    Console.WriteLine (username);
                Console.Write ("Enter PassWord : " + new string ('*', password.Length));
                key = Console.ReadKey ();
                if (key.Key == ConsoleKey.Enter)
                break;
                password += key.KeyChar;
            }
            Console.WriteLine ("Enter PassWord : " + new string ('*', password.Length));
            Console.Write ("Do you want to continue?(Y/N) : ");
            string choice = Console.ReadLine ();
            if (choice == "Y" || choice == "y") {
                if (UserBl.CheckExistUserAndPass (username, password)) {
                    SystemGUI ();
                } else {
                    Console.Clear ();
                    Console.Write ("\nLogin Fail!\n\nPress anykey to countinue...");
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
            switch(choice)
            {
                case "1":
                    Search();
                break;
                case "2":
                    DisplayMyApplications();
                break;
                case "3":
                    DisplayHistoryTrade();
                break;
                case "4":
                    isExit = true;
                break;
                case "0":
                    isExit = true;
                    existProgram = true;
                break;
            }
            if(isExit)break;
        }
    }
    void Search () {
        while(true)
        {
            Console.Clear();
            Console.WriteLine ("===Search Application===");
            Console.Write ("Enter Name Application: ");
            string Name = Console.ReadLine ();
            Console.Write("\nSearching...");
            
            List<Application> listApp = AppBl.
            
        }
    }
    void DisplayMyApplications () {
        
    }
    void DisplayHistoryTrade () {
        
    }
    void NotFound () {
        
    }
}