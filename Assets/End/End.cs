using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
	// Start is called before the first frame update
	private Animator animator;
	void Start()
	{
		animator = GetComponent<Animator>();
		if (animator != null)
		{
			StartCoroutine(TriggerAnimationLoop());
		}
	}

	// Coroutine to repeatedly set the Trigger
	IEnumerator TriggerAnimationLoop()
	{
		while (true)
		{
			animator.SetTrigger("BossWin");
			yield return new WaitForSeconds(2.0f); // Adjust the delay to match your animation length
		}
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
