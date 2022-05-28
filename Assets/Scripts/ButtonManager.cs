using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum ButtonType
{
    QuitApp = 1,
    ForwardScreen = 2,
    BackwardScreen = 3,
}

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private ButtonType buttonType;
    private int sceneBuildIndex = -1;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            sceneBuildIndex = SceneManager.GetActiveScene().buildIndex;

            switch (buttonType)
            {
                case ButtonType.QuitApp:
                    Application.Quit();
                    break;
                case ButtonType.ForwardScreen:
                    sceneBuildIndex++;
                    break;
                case ButtonType.BackwardScreen:
                    sceneBuildIndex--;
                    break;
            }

            if (sceneBuildIndex < 0)
            {
                sceneBuildIndex = SceneManager.sceneCountInBuildSettings - 1;
            }
            else if (sceneBuildIndex >= SceneManager.sceneCountInBuildSettings)
            {
                sceneBuildIndex = 0;
            }

            SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Single);
        });
    }
}
