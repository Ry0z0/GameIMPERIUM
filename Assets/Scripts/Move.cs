using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject character;   // Gán nhân vật vào đây từ Unity Inspector
    public Animator animator;      // Gán animator của nhân vật
    public float moveDistance = 2f; // Khoảng cách di chuyển
    public Button moveButton;      // Gán button từ Unity Inspector

    private Vector3 targetPosition;

    void Start()
    {
        // Gán sự kiện cho button khi click
        moveButton.onClick.AddListener(MoveCharacter);
        targetPosition = character.transform.position;
    }

    void MoveCharacter()
    {
        // Kích hoạt animation
        animator.SetTrigger("walk_right");

        // Cập nhật vị trí đích (di chuyển nhân vật tới phía trước 1 đoạn)
        targetPosition += Vector3.right * moveDistance;

        // Di chuyển nhân vật
        StartCoroutine(MoveToPosition());
    }

    private IEnumerator MoveToPosition()
    {
        float elapsedTime = 0;
        float duration = 1f;  // Thời gian di chuyển
        Vector3 startingPos = character.transform.position;

        while (elapsedTime < duration)
        {
            character.transform.position = Vector3.Lerp(startingPos, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        character.transform.position = targetPosition;
    }
}
