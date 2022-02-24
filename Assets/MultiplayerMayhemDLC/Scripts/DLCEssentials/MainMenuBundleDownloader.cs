using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MainMenuBundleDownloader : MonoBehaviour
{
    [SerializeField] private string androidSourceLinkMainMenu, iOSSourceLinkMainMenu, windowsSourceLinkMainMenu;
    [SerializeField] private AssetBundle downloadedMainMenuAssetBundle;

    public bool contentForMainMenuDownloadedSuccessfully = false;

    [Header("Be Aware : For Editor Use Only")]
    public bool clearBundleFromStorage = false;

    private void Awake()
    {
        Caching.compressionEnabled = false;
        if (clearBundleFromStorage)
            Caching.ClearCache();
    }

    private void Start()
    {
        SelectURLAccordingToCurrentPlatform();
    }

    private void SelectURLAccordingToCurrentPlatform()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
                StartCoroutine(DownloadMainMenuBundle(windowsSourceLinkMainMenu));
                break;
            case RuntimePlatform.Android:
                StartCoroutine(DownloadMainMenuBundle(androidSourceLinkMainMenu));
                break;
            case RuntimePlatform.IPhonePlayer:
                StartCoroutine(DownloadMainMenuBundle(iOSSourceLinkMainMenu));
                break;
            case RuntimePlatform.WindowsPlayer:
                StartCoroutine(DownloadMainMenuBundle(windowsSourceLinkMainMenu));
                break;
        }
    }

    private IEnumerator DownloadMainMenuBundle(string mainMenuBundleURL)
    {
        bool isCached = false;
        Debug.Log("MainMenu Bundle URL Received : " + mainMenuBundleURL);
        UnityWebRequest bundleRequest = UnityWebRequestAssetBundle.GetAssetBundle(mainMenuBundleURL, 36, 0);
        if (Caching.IsVersionCached(bundleRequest.url, 0))
            isCached = true;

        Debug.Log("MainMenu Bundle Cache status : " + isCached);

        UnityWebRequestAsyncOperation operation = bundleRequest.SendWebRequest();

        while (!operation.isDone)
        {
            if(!isCached)
                Debug.LogError("MainMenu Bundle Download Progress : " + bundleRequest.downloadProgress);
            else
                Debug.LogError("MainMenu Bundle Loading");
            yield return null;
        }

        if (bundleRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Main Menu Bundle Download Error : " + bundleRequest.error);
        }
        else
        {
            downloadedMainMenuAssetBundle = DownloadHandlerAssetBundle.GetContent(bundleRequest);
        }

    }

}
