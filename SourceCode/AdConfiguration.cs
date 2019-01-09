using UnityEngine;
using GoogleMobileAds.Api;

[CreateAssetMenu(fileName = "AdsConfig", menuName = "Ads/Configuration", order = 0)]
public class AdConfiguration : ScriptableObject
{
    public enum adServices { UnityAds,GoogleAdmob };
    public adServices service;
    public bool debug;
    public bool initStart;

    [Header("•Unity Ads Config•")]
    public string gameID;
    [Space]

    [Header("•Admob Config•")]
    public string bannerID;
    public string interstitialID;
    [Space]
    public AdPosition adPosition;
}
