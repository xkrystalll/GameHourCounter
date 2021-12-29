using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public static void Switch(string scenename)
    {
        SceneManager.LoadScene(scenename, LoadSceneMode.Single);
    }
}
