using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour , IUnityAdsListener
{
    private string playStoreID = "3604661";
    private string appStoreID = "3604660";

    private string intersitialAd = "video";
    private string rewardedVideoAd = "rewardedVideo";

    public bool isTargetPlayStore;
    public bool isTestAd;

    private void Start()
    {
        Advertisement.AddListener(this);
        InitializeAdvertisement();   
    }

    private void InitializeAdvertisement()
    {
        if(isTargetPlayStore)
        {
            Advertisement.Initialize(playStoreID , isTestAd);
            return;
        }
        Advertisement.Initialize(appStoreID, isTestAd);
    }

    public void PlayIntersitialAd()
    {
        if( !Advertisement.IsReady(intersitialAd))
        {
            return;
        }
        Advertisement.Show(intersitialAd);
    }

    public void PlayRewardedVideoAd()
    {
        if(!Advertisement.IsReady(rewardedVideoAd))
        {
            return;
        }
        Advertisement.Show(rewardedVideoAd);
    }

    public void OnUnityAdsReady(string placementId)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        FindObjectOfType<AudioManager>().Pause("GameMusic");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Failed:
                if (PlayerPrefs.GetInt("MusicEnabled", 1) == 1)
                    FindObjectOfType<AudioManager>().Play("GameMusic");
                break;
            case ShowResult.Skipped:
                if (PlayerPrefs.GetInt("MusicEnabled", 1) == 1)
                    FindObjectOfType<AudioManager>().Play("GameMusic");
                break;
            case ShowResult.Finished:
                if( placementId == rewardedVideoAd)
                {

                    if( PlayerPrefs.GetInt("isChallange" , 0) == 0)
                    {
                        GameObject crush = GameObject.FindGameObjectWithTag("CrushClosingAnim");
                        if (crush != null)
                        {
                            crush.GetComponentInChildren<Button>().interactable = false;
                        }

                        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().ContinueMainLevel();
                        GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyWaveController>().ContinueMainLevel();
                    }
                    else
                    {
                        GameObject crush = GameObject.FindGameObjectWithTag("CrushClosingAnim");
                        if( crush != null)
                        {
                            crush.GetComponentInChildren<Button>().interactable = false;
                        }
                        GameObject time = GameObject.FindGameObjectWithTag("TimeClosingAnim");
                        if (time != null)
                        {
                            time.GetComponentInChildren<Button>().interactable = false;
                        }
                        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().ContinueMainLevel();
                    }
                }

                else if(placementId == intersitialAd)
                {
                    if (PlayerPrefs.GetInt("MusicEnabled", 1) == 1)
                        FindObjectOfType<AudioManager>().Play("GameMusic");
                }

                break;
        }
    }
}
