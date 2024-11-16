using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class ChangeColorButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Start is called before the first frame update

    public Image startImage; 
    public Sprite redSprite;  
    public Sprite whiteSprite;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse entered");
        startImage.sprite = whiteSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exited");
        startImage.sprite = redSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //SceneManager.LoadScene("");
    }
}
