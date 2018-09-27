using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FictionLoadingApp
{


    public class Program
    {


        //static bool internetConnection = true;
        //static bool isThereALicence = true;
        //static bool isThereAnUpdate = true;

        static bool internetConnection = NextBool(80);
        static bool isThereALicence = NextBool();
        static bool isThereAnUpdate = NextBool(60);

        static WelcomeScreen menu = new WelcomeScreen();

        static void Main(string[] args)
        {
            Task menuprint = new Task(() =>
            {
                Console.Clear();
                menu.DrawMenu();
            });

            ShowSplash();

            InternetCheck();

            if (menu.internetConnection)
            {
                Task licence = new Task(() => RequestLicence());
                Task update = new Task(() => RequestUpdate());
                licence.Start();
                update.Start();
                var continuation = Task.Factory.ContinueWhenAll(new[] { licence, update }, tasks => menuprint.Start());
            }
            else
            {
                menuprint.Start();
            }

            Console.ReadKey();
        }

        //Принимает количество процентов (1-100) на выпадение true
        public static bool NextBool(int truePercentage = 50)
        {
            Random r = new Random();
            return r.NextDouble() < truePercentage / 100.0;
        }

        //Проверка интернета
        static void InternetCheck()
        {
            Console.WriteLine(Consts.CheckInternet);
            Thread.Sleep(3000);
            if (internetConnection)
            {
                menu.internetConnection = true;
                Console.WriteLine(Consts.YESinternet);
            }
            else
            {
                menu.internetConnection = false;
                Console.WriteLine(Consts.NOInternet);
                Thread.Sleep(3000);
            }
        }

        //Запрос лицензии
        static void RequestLicence()
        {
            Console.WriteLine(Consts.licenceRequesting);
            Thread.Sleep(5000);
            if (isThereALicence)
            {
                menu.isThereALicence = true;
            }
            else
            {
                menu.isThereALicence = false;
            }
            Console.WriteLine(Consts.licenceRequestingEnd);
            Thread.Sleep(3000);
        }

        //Запрос обновления
        static void RequestUpdate()
        {
            Console.WriteLine(Consts.updateRequesting);
            Thread.Sleep(2000);
            Console.WriteLine(Consts.updateRequestingEnd);

            if (isThereAnUpdate)
            {
                menu.isThereAnUpdate = true;
                Console.WriteLine(Consts.updateIsHere);
                Update();
            }
            else
            {
                menu.isThereAnUpdate = false;
                Console.WriteLine(Consts.updateIsNotHere);
                Thread.Sleep(3000);
            }
        }

        //Обновление
        static void Update()
        {
            Console.WriteLine(Consts.updateDownloading);
            Thread.Sleep(10000);
            Console.WriteLine(Consts.updateDownloadingFinished);
            Thread.Sleep(3000);
        }

        //Отрисовка Splash Screen
        static void ShowSplash()
        {
            Console.WriteLine(Consts.splashScreenString);
            Console.WriteLine(Consts.divisionString);
        }

    }

    //Меню
    public class WelcomeScreen
    {
        public bool isThereALicence;
        public bool isThereAnUpdate;
        public bool internetConnection;

        public void DrawMenu()
        {
            Console.WriteLine(Consts.menuScreenString);
            Console.WriteLine(Consts.divisionString);
            if (internetConnection)
            {
                if (isThereALicence)
                {
                    Console.WriteLine(Consts.YESlicence);
                }
                else
                {
                    Console.WriteLine(Consts.NOlicence);
                }

                if (isThereAnUpdate)
                {
                    Console.WriteLine(Consts.YESupdate);
                }
                else
                {
                    Console.WriteLine(Consts.NOupdate);
                }
            }
            else
            {
                Console.WriteLine(Consts.NOinternet);
            }

        }

    }

    //Все константы
    public class Consts
    {
        //Screens
        public const string divisionString = "------------------------------";
        public const string splashScreenString = "Splash Screen";
        public const string menuScreenString = "Menu Screen";
        //Licece
        public const string licenceRequesting = "Requesting Licence...";
        public const string licenceRequestingEnd = "Licence Requesting is finished!";
        public const string YESlicence = "Your program is licenced";
        public const string NOlicence = "Your program is not licenced";
        //Update
        public const string updateRequesting = "Requesting Update...";
        public const string updateRequestingEnd = "Update Requesting is finished!";
        public const string YESupdate = "Your program is updated";
        public const string NOupdate = "Your program is not updated";
        public const string updateIsHere = "There is an update, now it will be downloaded";
        public const string updateIsNotHere = "There is no update";
        public const string updateDownloading = "Downloading update...";
        public const string updateDownloadingFinished = "Downloading update finished!";
        //Intenet
        public const string NOinternet = "There's no internet connection, so it is not available to check your license and update";
        public const string CheckInternet = "Watching if there is an internet...";
        public const string YESinternet = "Internet connection established";
        public const string NOInternet = "No signal";
    }




    //Spinner
    public class ConsoleSpiner
    {
        int counter;
        public ConsoleSpiner()
        {
            counter = 0;
        }
        public void Turn()
        {
            while (true)
            {
                counter++;
                switch (counter % 4)
                {
                    case 0: Console.Write("/"); break;
                    case 1: Console.Write("-"); break;
                    case 2: Console.Write("\\"); break;
                    case 3: Console.Write("|"); break;
                }
                Thread.Sleep(200);
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            }
        }
    }

}
