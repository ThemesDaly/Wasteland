using UnityEngine;
using UnityEngine.SceneManagement;

public static class RuntimeInit
{
    // [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        
        
        SceneManager.LoadScene(EngineConstants.SERVICES_LEVEL_INDEX, LoadSceneMode.Additive);
        SceneManager.LoadScene(EngineConstants.UI_LEVEL_INDEX, LoadSceneMode.Additive);
    }
}