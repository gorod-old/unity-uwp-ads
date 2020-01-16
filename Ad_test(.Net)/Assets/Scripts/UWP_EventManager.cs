using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UWP_EventManager
{

    #region Advertisement

    public static event OnShowAd ShowAd;
    public delegate void OnShowAd();

    public static event OnLoadAd LoadAd;
    public delegate void OnLoadAd();

    public static event OnAdReady AdIsReady;
    public delegate void OnAdReady();

    public static event OnAdErrorOccurred AdErrorOccurred;
    public delegate void OnAdErrorOccurred();

    public static event OnAdCanceled AdCanceled;
    public delegate void OnAdCanceled();

    public static event OnAdCompleted AdCompleted;
    public delegate void OnAdCompleted();

    public static void ShowAdvertisement()
    {
        ShowAd?.Invoke();
    }

    public static void LoadAdvertisement()
    {
        LoadAd?.Invoke();
    }

    public static void AdReady()
    {
        AdIsReady?.Invoke();
    }

    public static void AdIsErrorOccurred()
    {
        AdErrorOccurred?.Invoke();
    }

    public static void AdIsCanceled()
    {
        AdCanceled?.Invoke();
    }

    public static void AdIsCompleted()
    {
        AdCompleted?.Invoke();
    }

    #endregion



}
