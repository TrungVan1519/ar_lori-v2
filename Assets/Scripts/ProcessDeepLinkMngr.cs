using UnityEngine;
using UnityEngine.SceneManagement;

public class ProcessDeepLinkMngr : MonoBehaviour
{
    public static ProcessDeepLinkMngr Instance { get; private set; }
    public string deeplinkURL;

    private void Update()
    {
        if (Instance == null)
        {
            Instance = this;
            Application.deepLinkActivated += onDeepLinkActivated;

            if (!string.IsNullOrEmpty(Application.absoluteURL))
            {
                // Cold start and Application.absoluteURL not null so process Deep Link.
                onDeepLinkActivated(Application.absoluteURL);
            }
            else
            {
                // Initialize DeepLink Manager global variable.
                deeplinkURL = "[none]"; 
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    private void onDeepLinkActivated(string url)
    {
        // Update DeepLink Manager global variable, so URL can be accessed from anywhere.
        deeplinkURL = url;

        // Decode the URL to determine action. 
        // Tthe app expects a link formatted like this: "unitydl://lori?SampleScene"
        string sceneName = url.Split("?"[0])[1];
        bool validScene;

        switch (sceneName)
        {
            case "SampleScene":
                validScene = true;
                break;
            default:
                validScene = false;
                break;
        }

        if (validScene)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }
    }
}
