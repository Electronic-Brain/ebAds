/* Class : AdManager
 * For initialize AD and show Ad* */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    public AdConfiguration configuration;

    public BannerView bannerView;
    public InterstitialAd interstitialAd;

    private bool isInitialized = false;

    public static AdManager instance;

    // Use this for initialization
    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(this);

        if (configuration.initStart)
        {
            if (configuration.service == AdConfiguration.adServices.GoogleAdmob)
            {
                GoogleAds.initService();

            }
            else if (configuration.service == AdConfiguration.adServices.UnityAds)
            {
                UnityAds.initService();
            }
        }
        //TestAd();
    }

    void TestAd()
    {
        UnityAds.initService();
        UnityAds.ShowAds("video");
    }

    public class GoogleAds
    {
        /// <summary>
        /// Init Google Ad Services
        /// </summary>
        public static void initService()
        {
            if (instance.configuration.bannerID != null && !instance.isInitialized)
            {
                instance.bannerView = new BannerView(instance.configuration.bannerID, AdSize.Banner, instance.configuration.adPosition);
                AdRequest request = new AdRequest.Builder().Build();
                instance.bannerView.LoadAd(request);
                instance.isInitialized = true;
            }
            if (instance.configuration.interstitialID != null && !instance.isInitialized)
            {
                instance.interstitialAd = new InterstitialAd(instance.configuration.interstitialID);
                AdRequest request = new AdRequest.Builder().Build();
                instance.interstitialAd.LoadAd(request);
                instance.isInitialized = true;
            }
        }
        /// <summary>
        /// For Showing Banner Ad, also requsting Ad
        /// </summary>
        public static void ShowBannerAd()
        {
            //Requst Banner Ad
            if (!instance.isInitialized)
            {
                instance.bannerView = new BannerView(instance.configuration.bannerID, AdSize.Banner, instance.configuration.adPosition);
                AdRequest request = new AdRequest.Builder().Build();
                instance.bannerView.LoadAd(request);
                instance.isInitialized = true;
            }

            instance.bannerView.Show();// show Banner Ad
        }
        /// <summary>
        /// For Hiding Banner Ad
        /// </summary>
        public static void HideBanner()
        {
            instance.bannerView.Hide();
        }

        /// <summary>
        /// For showing Video Ad
        /// </summary>
        public static void RequestInterstitialAd()
        {
            if (!instance.isInitialized)
            {
                instance.interstitialAd = new InterstitialAd(instance.configuration.interstitialID);
                AdRequest request = new AdRequest.Builder().Build();
                instance.interstitialAd.LoadAd(request);
                instance.isInitialized = true;
            }

            ShowInterstitialAd();
        }
        /// <summary>
        /// Show Video to end user
        /// </summary>
        private static void ShowInterstitialAd()
        {
            if (instance.interstitialAd.IsLoaded())
                instance.interstitialAd.Show();
            else Debug.LogWarning("Interstitial Ad doesn't loaded properly");
        }
    }

    public class UnityAds
    {
        /// <summary>
        /// Initialize whole services
        /// </summary>
        public static void initService()
        {
            Advertisement.Initialize(instance.configuration.gameID);
        }

        /// <summary>
        /// Show ad depeding on Placement ID
        /// </summary>
        /// <param name="placementID"></param>
        public static void ShowAds(string placementID)
        {

            if (!Advertisement.IsReady(placementID))
            {
                Debug.LogError("Ad isn't ready  for   " + placementID);
            }
            ShowOptions options = new ShowOptions();
            if (instance.configuration.debug)
            {
                options.resultCallback = OnAd_Result;
            }
            Advertisement.Show(placementID, options);

        }
        /// <summary>
        /// Show Result of video
        /// </summary>
        /// <param name="result"></param>
        private static void OnAd_Result(ShowResult result)
        {
            switch (result)
            {
                case ShowResult.Finished:
                    Debug.Log("Ad finished Successfully");
                    break;
                case ShowResult.Skipped:
                    Debug.Log("Ad skipped by User");
                    break;
                case ShowResult.Failed:
                    Debug.LogError("Ad Failed to Display");
                    break;
            }
        }
    } 
}
