using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private Vector3 firstpoint = new Vector3();
    private Vector3 secondpoint = new Vector3();
    private float xAngle = 0.0f;
    private float yAngle = 0.0f;
    private float xAngTemp= 0.0f;
    private float yAngTemp= 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        xAngle = 0.0f;
        yAngle = 0.0f;
        transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0) {
            if(Input.GetTouch(0).phase == TouchPhase.Began) {
                firstpoint = Input.GetTouch(0).position;
                xAngTemp = xAngle;
                yAngTemp = yAngle;
            }
            if(Input.GetTouch(0).phase==TouchPhase.Moved) {
                secondpoint = Input.GetTouch(0).position;
                xAngle = xAngTemp - (secondpoint.x - firstpoint.x) * 180.0f / Screen.width;
                yAngle = yAngTemp + (secondpoint.y - firstpoint.y) *90.0f / Screen.height;
                transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
            }
        }
    }
}
