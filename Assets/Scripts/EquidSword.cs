using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquidSword : MonoBehaviour
{
    public GameObject sword; // Kiếm
    public Transform swordHolder;
    // Start is called before the first frame update
    void Start()
    {
        Equip() ;
    }

    // Update is called once per frame
    void Equip()
    {
        if (sword != null && swordHolder != null)
        {
            // Gắn kiếm vào tay
            sword.transform.SetParent(swordHolder);
          
         
        }
    }
}
