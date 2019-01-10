using UnityEngine;
using GoogleMobileAds.Api;

[CreateAssetMenu(fileName = "AdsConfig", menuName = "Ads/Configuration", order = 0)]
public class AdConfiguration : ScriptableObject
{
    public enum adServices { UnityAds,GoogleAdmob };
    public adServices service;  //Services for Ad
    public bool debug; //Debug Ads Results
    public bool initStart; //Initialize Services on Start

    [Header("•Unity Ads Config•")]
    public string gameID; //Your Game ID for Unity Ads. Get it from here https://operate.dashboard.unity3d.com
    [Space]

    [Header("•Admob Config•")]
    public string bannerID; //Your Banner ID from https://apps.admob.com > Apps > Your Apps > Ad Units, Create new one if doesn't exist. Same for new App
    public string interstitialID; //Your Insterstitial ID from https://apps.admob.com > Apps > Your Apps > Ad Units, Create new one if doesn't exist. Same for new App
    [Space]
    public AdPosition adPosition; // Banner Ad Position in Screen
}
