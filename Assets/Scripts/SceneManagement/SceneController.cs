using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    int map = 1;
    
    public void NextScene()
    {
        map++;
    }

    public int CurrentMap() { return map; } 
}
