using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour
{
    public Animator animator; // Gắn Animator ở đây
    public Button button;     // Gắn Button ở đây

    void Start()
    {
        button.onClick.AddListener(PlayAnimation);
    }

    void PlayAnimation()
    {
        animator.SetTrigger("PlayAnimation");
    }
}
