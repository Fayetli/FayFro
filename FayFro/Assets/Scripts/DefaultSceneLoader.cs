using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefaultSceneLoader : MonoBehaviour
{
    public static IEnumerator LoadNewScene(string sceneName)
    {
        float fadeTime = GameObject.FindObjectOfType<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(sceneName);
    }

    public static IEnumerator LoadNewScene()
    {
        float fadeTime = GameObject.FindObjectOfType<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
