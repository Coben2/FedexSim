using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public Transform conveyorStartPos;
    public GameManager instance;
    public GameObject box;
    public float forceAmount;
    private bool entered = false;
    private Collision collisions;
    public Canvas questionCanvas;
    public bool hasBoxMoved;
    Vector3 moveBoxPos;
    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        if(hasBoxMoved)
        {
            CheckDistance();

        }
     
        if(instance.BoxMove() != null)
        {
            box = instance.boxCopy;
        }
        if (entered)
        {
            var box = collisions.gameObject.GetComponent<Rigidbody>();
            box.AddForce(Vector3.left * forceAmount, ForceMode.Force);
            Debug.Log("is moving");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.tag == "Non IC")
        {
            collisions = collision;
        }

    }

    public void MoveBox()
    {
        moveBoxPos = conveyorStartPos.position;
        Debug.Log(moveBoxPos);
        hasBoxMoved = true;
    }

    public void CheckDistance()
    {
        Vector3.Lerp(box.transform.position, moveBoxPos, 1);
        if (Vector3.Distance(moveBoxPos, box.transform.position) < 0.1f)
        {
            hasBoxMoved = false;
        }
        else
        {
            
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        entered = false;
        Debug.Log("not moving");
       
    }
}
