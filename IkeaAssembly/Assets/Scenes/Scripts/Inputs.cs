using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Inputs : MonoBehaviour
{
    private float oldTouchDistance = 0;
    private float touchDistance;
    [SerializeField]
    private GameObject cameraObject;
    private RaycastHit hit;
    private Boolean animationRunning = false;
    
    // Update is called once per frame
    void Update()
    {
        animationRunning = GetComponent<Assembly>().getAnimationBool();
        zoomCheck();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        try
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (Physics.Raycast(ray,out hit))
                {
                    if (hit.collider.CompareTag("resetCam"))
                    {
                        cameraObject.GetComponent<CameraController>().centerCamera();
                    }

                    if (!animationRunning && hit.collider.CompareTag("prevStep"))
                    {
                        cameraObject.GetComponent<CameraController>().centerCamera();
                    }
                    if(hit.collider.CompareTag("Finish"))
                    {
                        hit = new RaycastHit();
                        GetComponent<Assembly>().finishBuild();
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
       
    }

    private void calculateTouchDistance()
    {
        Vector2 touch0, touch1;
        touch0 = Input.GetTouch(0).position;
        touch1 = Input.GetTouch(1).position;
        touchDistance = Vector2.Distance(touch0, touch1);
    }

    private void zoomCheck()
    {
        if (Input.touchCount >= 2)
        {
            calculateTouchDistance();
            if (touchDistance > oldTouchDistance)
            {
                cameraObject.GetComponent<CameraController>().zoomIn();
                oldTouchDistance = touchDistance;
            }else if (touchDistance<oldTouchDistance)
            {
                cameraObject.GetComponent<CameraController>().zoomOut();
                oldTouchDistance = touchDistance;
            }
        }
    }
}
