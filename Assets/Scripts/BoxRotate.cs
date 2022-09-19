using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRotate : MonoBehaviour
{
    public Vector2 turn;
    public float rotationSpeed;
    public static bool isDragging;
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    private void OnMouseDrag()
    {
        float xRot = Input.GetAxis("Mouse X") * rotationSpeed;
        float yRot = Input.GetAxis("Mouse Y") * rotationSpeed;
        turn.x += xRot;
        turn.y += yRot;
        transform.localRotation = Quaternion.Euler(turn.y, -turn.x, 0);
        isDragging = true;
        Debug.Log("IsDragging");
    }
}
