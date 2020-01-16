
Unity version - 18 or low;
Scripting backend .net;
Internet Client check box in publishing settings;
Do not forget to connect the link to the Microsoft Advertising SDK for Xaml!

Note!
Every InterstitialAd has a corresponding ad unit that is used by Microsoft services 
to serve ads to the control, and every ad unit consists of an ad unit ID and application ID. 
In this project, you assign test ad unit ID and application ID values to your control. 
These test values can only be used in a test version of your app. Before you publish your app to the Store, 
you must replace these test values with live values from Partner Center.



Ad this code to MainPage.xaml.cs :

	using Microsoft.Advertising.WinRT.UI;

To MainPage class:

 	InterstitialAd myInterstitialAd = null;
 	string myAppId = "d25517cb-12d4-4699-8bdc-52040c712cab";
 	string myAdUnitId = "test";

	private async void UWP_EventManager_LoadAd()
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                myInterstitialAd.RequestAd(AdType.Video, myAppId, myAdUnitId);
            });
        }

        private async void UWP_EventManager_ShowAd()
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (InterstitialAdState.Ready == myInterstitialAd.State)
                {
                    myInterstitialAd.Show();
                }
            });
        }

        void MyInterstitialAd_AdReady(object sender, object e)
        {
            UWP_EventManager.AdReady();
        }

        void MyInterstitialAd_ErrorOccurred(object sender, AdErrorEventArgs e)
        {
            UWP_EventManager.AdIsErrorOccurred();
        }

        void MyInterstitialAd_Completed(object sender, object e)
        {
            UWP_EventManager.AdIsCompleted();
        }

        void MyInterstitialAd_Cancelled(object sender, object e)
        {
            UWP_EventManager.AdIsCanceled();
        }


To MainPage class constructor:

 		myInterstitialAd = new InterstitialAd();
        myInterstitialAd.AdReady += MyInterstitialAd_AdReady;
        myInterstitialAd.ErrorOccurred += MyInterstitialAd_ErrorOccurred;
        myInterstitialAd.Completed += MyInterstitialAd_Completed;
        myInterstitialAd.Cancelled += MyInterstitialAd_Cancelled;

        UWP_EventManager.ShowAd += UWP_EventManager_ShowAd;
        UWP_EventManager.LoadAd += UWP_EventManager_LoadAd;