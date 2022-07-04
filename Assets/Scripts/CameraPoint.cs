using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPoint : MonoBehaviour
{
    public Transform[] camPoints;
    public int currentPosition;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = camPoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextPoint()
    {
        currentPosition++;
        Debug.Log("current position is" + currentPosition);
            transform.position = camPoints[currentPosition].position;
        
    }
    public void LastPoint()
    {
        currentPosition--;
        Debug.Log("current position is" + currentPosition);
        transform.position = camPoints[currentPosition].position;
        
       
    }
}
