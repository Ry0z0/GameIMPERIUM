using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDisplayPower : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject imageObject; // Kéo GameObject hình ảnh vào đây trong Inspector

    private bool isVisible = false;

    public void ToggleImage()
    {
        isVisible = !isVisible;
        imageObject.SetActive(isVisible);
    }
}
