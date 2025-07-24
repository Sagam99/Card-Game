#if UNITY_EDITOR
    using UnityEditor;
using UnityEditor.SearchService;

#endif
using UnityEngine;
using UnityEngine.SceneManagement;



public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene(1); // Load the first scene (index 1)

    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif


    }
}
