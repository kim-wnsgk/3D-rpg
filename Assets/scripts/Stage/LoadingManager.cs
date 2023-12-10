using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class LoadingManager : MonoBehaviour
{

    public Slider progressBar;
    public TextMeshProUGUI progressText;

    public static string nextScene;

    [SerializeField]
    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }

    private IEnumerator LoadScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f); // 0 to 1 range
            progressBar.value = progress;
            progressText.text = Mathf.Round(progress * 100f) + "%";

            yield return null;
        }
    }
}
