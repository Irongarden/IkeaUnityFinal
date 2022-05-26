using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    float oldDist = 0f;
    private float maxDist = 6;
    private float minDist = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
        float distance = Vector3.Distance (transform.position, target.transform.position);
        Debug.Log("Dist to target"+distance);
        
        if (Input.touchCount >= 2)
        {
            
            Vector2 touch0, touch1;
            float dist;
            touch0 = Input.GetTouch(0).position;
            touch1 = Input.GetTouch(1).position;
            dist = Vector2.Distance(touch0, touch1);
            Debug.Log("Distance: "+dist);
            if (dist > oldDist && distance>minDist)
            {
                
                transform.position += (transform.forward) * Time.deltaTime*5;
                Debug.Log("Increasing");
                oldDist = dist;
                
                
            }else if (dist < oldDist && distance<maxDist)
            {
              
                transform.position -= (transform.forward) * Time.deltaTime*5;
                Debug.Log("Decreasing");
                oldDist = dist;
            }
            
        }
        else
        {
           // oldDist = 0;
        }
       
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Physics.Raycast(ray,out hit))
            {
                if (hit.collider.CompareTag("resetCam"))
                {
                    transform.LookAt(target.transform.position);
                }
                
                    if (hit.collider.CompareTag("plus"))
                    {
                        if (distance>2)
                        {
                            transform.position += (transform.forward) * Time.deltaTime*5;
                        }
                        
                        
                    }else if (hit.collider.gameObject.CompareTag("minus"))
                    {
                        if (distance<7)
                        {
                            transform.position -= (transform.forward) * Time.deltaTime*5;
                        }
                        
                    }else if (hit.collider.gameObject.CompareTag("leftRot"))
                    {
                        transform.RotateAround(target.transform.position, new Vector3(0.0f,1.0f,0.0f),20*Time.deltaTime*5);
                        
                    }else if (hit.collider.gameObject.CompareTag("rightRot"))
                    {
                        transform.RotateAround(target.transform.position, new Vector3(0.0f,-1.0f,0.0f),20*Time.deltaTime*5);
                    }
                    
                
            }
        }
    }

    public void setTarget(Transform target)
    {
        transform.LookAt(target.position);
    }
}
