using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FictionLoadingApp
{
    public class FictionLoadingApp
    {
        static void Main(string[] args)
        {
            // Initializing a welcomescreen element.
            WelcomeScreen menu = new WelcomeScreen();

            // Creating an input parameters.
            bool internetConnection = true;
            bool isThereALicence = true;
            bool isThereAnUpdate = true;

            //static bool internetConnection = NextBool(80);
            //static bool isThereALicence = NextBool();
            //static bool isThereAnUpdate = NextBool(60);

            // Initializing tokens.
            CancellationTokenSource licenceCancellationTokenSourse = new CancellationTokenSource();
            CancellationTokenSource updateCancellationTokenSourse = new CancellationTokenSource();
            CancellationTokenSource internetCancellationTokenSourse = new CancellationTokenSource();
            CancellationToken licenceToken = licenceCancellationTokenSourse.Token;
            CancellationToken updateToken = updateCancellationTokenSourse.Token;
            CancellationToken internetToken = internetCancellationTokenSourse.Token;
            if (isThereALicence != true)
                licenceCancellationTokenSourse.Cancel();
            if (isThereAnUpdate != true)
                updateCancellationTokenSourse.Cancel();
            if (internetConnection != true)
                internetCancellationTokenSourse.Cancel();

            // Printing splash screen.
            Task ShowSplash = new Task(() =>
            {
                Console.WriteLine(Consts.splashScreenString);
                Console.WriteLine(Consts.divisionString);
            });
            ShowSplash.Start();


            //-----Internet check-----

            // Write, that now we will check for connection.
            // Wait for splash screen.
            Task WriTeInternetCheck = ShowSplash.ContinueWith(ant =>
            {
                Console.WriteLine(Consts.CheckInternet);
            });

            // Internet check. Wait printing.
            Task InternetCheck = WriTeInternetCheck.ContinueWith(ant =>
            {
                menu.internetConnection = true;
                Console.WriteLine(Consts.YESinternet);
            }, internetToken);

            // Notification if no internet.      
            Task NoInternet = InternetCheck.ContinueWith(ant =>
            {
                Thread.Sleep(3000);
                menu.internetConnection = false;
                Console.WriteLine(Consts.NOinternet);
                Thread.Sleep(3000);
            }, TaskContinuationOptions.OnlyOnCanceled);


            //-----Licence check-----

            // Write that now we will check licence (waiting internet availability).
            Task WriTeLicenceCheck = InternetCheck.ContinueWith(ant =>
            {
                Console.WriteLine(Consts.licenceRequesting);
            }, TaskContinuationOptions.NotOnCanceled);

            // Licence request.
            Task RequestLicence = InternetCheck.ContinueWith(ant =>
            {
                Thread.Sleep(5000);
                menu.isThereALicence = true;
                Console.WriteLine(Consts.licenceRequestingEnd);
                Console.WriteLine(Consts.haveALicence);
                Thread.Sleep(3000);
            }, licenceToken, TaskContinuationOptions.NotOnCanceled, TaskScheduler.Default);

            // Notification if no request.
            Task NoLicence = RequestLicence.ContinueWith(ant =>
            {
                Thread.Sleep(5000);
                menu.isThereALicence = false;
                Console.WriteLine(Consts.licenceRequestingEnd);
                Console.WriteLine(Consts.donthaveALicence);
                Thread.Sleep(3000);
            }, TaskContinuationOptions.OnlyOnFaulted);


            //-----Update availability-----

            // Write that now we will check for updates (waiting internet access true).
            Task WriTeUpdateCheck = InternetCheck.ContinueWith(ant =>
            {
                Console.WriteLine(Consts.updateRequesting);
            }, TaskContinuationOptions.NotOnCanceled);

            // Update request.
            Task RequestUpdate = WriTeUpdateCheck.ContinueWith(ant =>
            {

                Thread.Sleep(3000);
                menu.isThereAnUpdate = true;
                Console.WriteLine(Consts.updateRequestingEnd);
                Console.WriteLine(Consts.updateIsHere);
            }, updateToken, TaskContinuationOptions.NotOnCanceled, TaskScheduler.Default);

            // Notification if no update.
            Task NoUpdate = RequestUpdate.ContinueWith(ant =>
            {
                Thread.Sleep(3000);
                menu.isThereAnUpdate = false;
                Console.WriteLine(Consts.updateRequestingEnd);
                Console.WriteLine(Consts.updateIsNotHere);
                Thread.Sleep(3000);
            }, TaskContinuationOptions.OnlyOnFaulted);

            // Updating.
            Task Update = RequestUpdate.ContinueWith(ant =>
            {
                Console.WriteLine(Consts.updateDownloading);
                Thread.Sleep(10000);
                Console.WriteLine(Consts.updateDownloadingFinished);
                Thread.Sleep(3000);
            }, TaskContinuationOptions.NotOnCanceled);


            //-----Menu screen-----

            // Printing menu.
            Action menuprint = () =>
            {
                Console.Clear();
                menu.DrawMenu();
            };

            // Waiting for update and licence.
            Task print1 = Task.Factory.ContinueWhenAll(new[] { NoInternet, RequestUpdate, InternetCheck, RequestLicence }, tasks =>
            {
                menuprint();
            });

            Console.ReadKey();
        }

        // Gets percentage (1-100) on return true.
        public static bool NextBool(int truePercentage = 50)
        {
            Random r = new Random();
            return r.NextDouble() < truePercentage / 100.0;
        }
    }

    /// <summary>
    /// Drawing menu class.
    /// </summary>
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
                Console.WriteLine(Consts.NOkekInternet);
            }
        }
    }

    /// <summary>
    /// All constants.
    /// </summary>
    public class Consts
    {
        // Screens.
        public const string divisionString = "------------------------------";
        public const string splashScreenString = "Splash Screen";
        public const string menuScreenString = "Menu Screen";
       
        // Licece.
        public const string licenceRequesting = "Requesting Licence...";
        public const string licenceRequestingEnd = "Licence Requesting is finished!";
        public const string haveALicence = "You have a licence!";
        public const string donthaveALicence = "You don't have a licence!";
        public const string YESlicence = "Your program is licenced";
        public const string NOlicence = "Your program is not licenced";
       
        // Update.
        public const string updateRequesting = "Requesting Update...";
        public const string updateRequestingEnd = "Update Requesting is finished!";
        public const string YESupdate = "Your program is updated";
        public const string NOupdate = "Your program is not updated";
        public const string updateIsHere = "There is an update, now it will be downloaded";
        public const string updateIsNotHere = "There is no update";
        public const string updateDownloading = "Downloading update...";
        public const string updateDownloadingFinished = "Downloading update finished!";
        
        // Intenet.
        public const string NOinternet = "There's no internet connection, so it is not available to check your license and update";
        public const string CheckInternet = "Watching if there is an internet...";
        public const string YESinternet = "Internet connection established";
        public const string NOkekInternet = "No signal";
    }
}
