using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadMap : MonoBehaviour
{
    public void ReloadNormalMap()
    {
        // Luôn tải lại scene có tên "NormalMap"
        SceneManager.LoadScene("NormalMap");
    }
}
