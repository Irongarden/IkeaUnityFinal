using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Inputs : MonoBehaviour
{
    private float oldTouchDistance = 0;
    private float touchDistance;
    [FormerlySerializedAs("camera")] [SerializeField]
    private GameObject cameraObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        zoomCheck();
        // Screen Clicks
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Physics.Raycast(ray,out hit))
            {
                if (hit.collider.CompareTag("resetCam"))
                {
                    cameraObject.GetComponent<cameraController>().centerCamera();
                }
            }
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
                cameraObject.GetComponent<cameraController>().zoomin();
                oldTouchDistance = touchDistance;
            }else if (touchDistance<oldTouchDistance)
            {
                cameraObject.GetComponent<cameraController>().zoomOut();
                oldTouchDistance = touchDistance;
            }
        }
    }
}