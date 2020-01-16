using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ad_Dispatcher : MonoBehaviour
{
    public static Ad_Dispatcher Instance { get; private set; }
    public Text info, count, reward;
    static bool adIsReady;
    int result, reward_num;
    string[] result_description = { "", "ad is ready to show", "ad is cancelled", "error", "ad is completed" };

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
                Destroy(gameObject);
        }
    }

    private void Start()
    {
        UWP_EventManager.AdIsReady += UWP_EventManager_AdIsReady;
        UWP_EventManager.AdCompleted += UWP_EventManager_AdCompleted;
        UWP_EventManager.AdErrorOccurred += UWP_EventManager_AdErrorOccurred;
        UWP_EventManager.AdCanceled += UWP_EventManager_AdCanceled;
    }


    private void UWP_EventManager_AdIsReady()
    {
        adIsReady = true;
        result = 1;
    }

    private void UWP_EventManager_AdCanceled()
    {
        adIsReady = false;
        result = 2;
    }

    private void UWP_EventManager_AdErrorOccurred()
    {
        adIsReady = false;
        result = 3;
    }

    private void UWP_EventManager_AdCompleted()
    {
        adIsReady = false;
        result = 4;
    }

    public void StartShowAd()
    {
        if (adIsReady)
        {
            UWP_EventManager.ShowAdvertisement();
            StartCoroutine(WaitingForResult());
        }
    }

    public void StartLoadingAd()
    {
        if (adIsReady)
        {
            info.text = "ad is ready to show";
            return;
        }

        info.text = "start loading ad";
        UWP_EventManager.LoadAdvertisement();
        StartCoroutine(WaitingForResult());
        StartCoroutine(WaitCount());
    }

    public static void LoadAd()
    {
        if (Instance)
        {
            Instance.StartLoadingAd();
            return;
        }
        UWP_EventManager.LoadAdvertisement();
    }

    IEnumerator WaitCount()
    {
        if (result != 0) yield break;

        var wait = new WaitForSeconds(1.0f);
        int i = 0;
        while (result == 0)
        {
            yield return wait;
            i++;
            count.text = i.ToString();
            if (adIsReady || i >= 30)
            {
                if (!adIsReady) info.text = "waiting for loading";
                break;
            }
        }
        count.text = "0";
    }

    IEnumerator WaitingForResult()
    {
        if (result != 0) yield break;

        var wait = new WaitForSeconds(0.1f);
        while (result == 0)
        {
            yield return wait;
        }
        info.text = result_description[result];
        if (result == 4)
        {
            // some code to give a reward to the player;
            reward.text = (++reward_num).ToString();
        }
        result = 0;
    }


    private void OnDestroy()
    {
        UWP_EventManager.AdIsReady -= UWP_EventManager_AdIsReady;
        UWP_EventManager.AdCompleted -= UWP_EventManager_AdCompleted;
        UWP_EventManager.AdErrorOccurred -= UWP_EventManager_AdErrorOccurred;
        UWP_EventManager.AdCanceled -= UWP_EventManager_AdCanceled;
    }
}
