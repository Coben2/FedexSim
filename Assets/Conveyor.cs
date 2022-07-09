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
    Vector3 boxPosition;
    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
     
        if(instance.BoxInitialize() != null)
        {
            box = instance.boxCopy;
            if(box != null)
            {
                boxPosition = box.transform.position;
            }
          
            Debug.Log(box);
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
            entered = true;
            collisions = collision;
        }

    }

    public void MoveBox()
    {
        moveBoxPos = conveyorStartPos.position;
        Debug.Log(moveBoxPos);
    }

    public bool CheckDistance()
    {
        float time = 0;
        time += Time.deltaTime;
        if(time < 1)
        {
          box.transform.position = Vector3.Lerp(boxPosition, moveBoxPos, time);
        if (Vector3.Distance(moveBoxPos, box.transform.position) <= 0.1f)
        {
                return true;
                Debug.Log("box has moved");
        }
        else
        {
                return false;
                Debug.Log("box hasn't moved");
        }
        }
        return false;
    }

    private void OnCollisionExit(Collision collision)
    {
        collisions = collision;
        entered = false;
        collisions.gameObject.SetActive(false);
        Debug.Log("not moving");
       
    }

}
