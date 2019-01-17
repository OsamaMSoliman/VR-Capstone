using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    private static WaitForSeconds delay2s = new WaitForSeconds(2f);

    public static IEnumerator Begin(string name)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);
        asyncLoad.allowSceneActivation = false;
        yield return delay2s;
        while (!asyncLoad.isDone) yield return null;
        asyncLoad.allowSceneActivation = true;
    }

    public static IEnumerator Begin(int sceneBuildIndex)
    {
        yield return delay2s;
        SceneManager.LoadSceneAsync(sceneBuildIndex);
    }
}
