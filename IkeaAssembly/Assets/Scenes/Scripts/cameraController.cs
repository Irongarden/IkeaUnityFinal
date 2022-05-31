using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    private float maxDist = 6;
    private float minDist = 1;
    private float distanceToTarget;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance (transform.position, target.transform.position);
    }

    public void setTarget(Transform target)
    {
        transform.LookAt(target.position);
    }

    public void zoomIn()
    {
        if (distanceToTarget > minDist)
        {
            transform.position += (transform.forward) * Time.deltaTime*5;
        }
    }

    public void zoomOut()
    {
        if (distanceToTarget < maxDist)
        {
            transform.position -= (transform.forward) * Time.deltaTime*5;
        }
    }

    public void centerCamera()
    {
        transform.LookAt(target.transform.position);
    }
}
