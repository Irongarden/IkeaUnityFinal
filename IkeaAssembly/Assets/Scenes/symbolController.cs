using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class symbolController : MonoBehaviour
{
[SerializeField]
    private GameObject cam;

    private Vector3 offset;

   
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - cam.transform.position;
       // cam = Camera.main.GetComponent<GameObject>();

    }
    
   

    // Update is called once per frame
    private void LateUpdate()
    {
       // transform.position = cam.transform.position + offset;
       gameObject.transform.parent = cam.transform;
    }
}
