using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LoadSceneByIndex(int sceneIndex)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneIndex);
    }
    public void QuitGame()
    {
        Debug.Log("Game is quitting...");

        // Thoát trong bản build
        Application.Quit();

        // Thoát trong Unity Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
