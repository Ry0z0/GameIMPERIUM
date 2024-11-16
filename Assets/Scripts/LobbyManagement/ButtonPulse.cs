using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPulse : MonoBehaviour
{
    public RectTransform buttonTransform;
    public float minScale = 0.9f;
    public float maxScale = 1.1f;
    public float speed = 2f;

    private float lerpTime = 0f;

    void Update()
    {
        lerpTime += Time.deltaTime * speed;
        float scale = Mathf.Lerp(minScale, maxScale, Mathf.PingPong(lerpTime, 1));
        buttonTransform.localScale = new Vector3(scale, scale, scale);
    }
}
