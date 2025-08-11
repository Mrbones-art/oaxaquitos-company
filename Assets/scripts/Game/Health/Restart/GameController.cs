using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartController : MonoBehaviour
{
    public GameObject loadingScreen; // Assign in Inspector if you want a loading UI

    public void RestartLevel()
    {
        if (loadingScreen != null)
            loadingScreen.SetActive(true);

        string currentScene = SceneManager.GetActiveScene().name;
        Debug.Log($"[RestartController] Player died — restarting scene: {currentScene}");
        StartCoroutine(RestartSceneCoroutine(currentScene));
    }

    private System.Collections.IEnumerator RestartSceneCoroutine(string sceneName)
    {
        yield return new WaitForSeconds(3f); // Optional delay for loading screen
        SceneManager.LoadScene(sceneName);
    }
}
