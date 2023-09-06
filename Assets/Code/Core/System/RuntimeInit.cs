using UnityEngine;
using UnityEngine.SceneManagement;

public static class RuntimeInit
{
    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        SceneManager.LoadScene(SceneConstants.SERVICES_LEVEL_INDEX, LoadSceneMode.Additive);
        SceneManager.LoadScene(SceneConstants.UI_LEVEL_INDEX, LoadSceneMode.Additive);
    }
}