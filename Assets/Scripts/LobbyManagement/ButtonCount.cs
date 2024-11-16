using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCount : MonoBehaviour
{
    // Start is called before the first frame update
    public int count = 0;
    public Button Button;
    public GameObject Image1;
    public GameObject Image2;
    public GameObject Canvas;
    void Start()
    {
        Button.onClick.AddListener(CountClick);
    }

    // Update is called once per frame
    void CountClick()
    {
        if (count == 0)
        {
            Image1.SetActive(false);
            Image2.SetActive(true);
            count++;
        }
        else if (count == 1)
        {
            Canvas.SetActive(false);
        }
    }
}
