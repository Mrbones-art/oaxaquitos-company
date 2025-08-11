using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Only needed if you manipulate text dynamically

public class LevelManager : MonoBehaviour
{
    public GameObject boss;
    public float delayBeforeNextLevel = 1f;
    public string nextSceneName = "level1";
    public GameObject loadingScreen; // Assign in Inspector

    private bool bossDefeated = false;

    void Update()
    {
        if (!bossDefeated && boss == null)
        {
            bossDefeated = true;
            Invoke(nameof(ShowLoadingAndLoad), delayBeforeNextLevel);
        }
    }

    void ShowLoadingAndLoad()
    {
        if (loadingScreen != null)
            loadingScreen.SetActive(true);

        StartCoroutine(LoadSceneAsync());
    }

    System.Collections.IEnumerator LoadSceneAsync()
    {
        yield return new WaitForSeconds(1f); // Give time for loading screen to appear

        AsyncOperation operation = SceneManager.LoadSceneAsync(nextSceneName);
        operation.allowSceneActivation = true;

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}