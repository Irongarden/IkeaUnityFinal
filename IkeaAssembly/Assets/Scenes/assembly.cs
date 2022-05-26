using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class assembly : MonoBehaviour
{
    [FormerlySerializedAs("prefabs")] [SerializeField]
    private GameObject[] parts;
    [SerializeField]
    private GameObject cam;

    [SerializeField]
    private GameObject[] tools;

    [SerializeField]
    private GameObject finishPanel;
    
    private List<GameObject> instantiated;
    private List<GameObject> instantiatedTools;

    private int toolStep = 0;
    private int partStep = 0;
    private Boolean animate = false;
    private Boolean tool = false;
    private RaycastHit hit;
    private float timer;
    private float finishTime =0;

   private Vector3 a = new Vector3();
   private Vector3 b = new Vector3();
   private Quaternion rotation = new Quaternion();
    
   private int maxStep = 0;
       
    private int step = 0;

    private int point = 1;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        instantiated = new List<GameObject>();
        instantiatedTools = new List<GameObject>();
        a = transform.position; // + Vector3.up
        maxStep = parts.Length + tools.Length;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //Debug.Log("Step: "+step);
        //Debug.Log("Animate: "+ animate);
        
        b = Camera.main.transform.position + Vector3.forward;
        rotation = transform.rotation;
        //hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (step>=maxStep && !animate)
        {
            if (finishTime == 0)
            {
                finishTime = timer;
                finishPanel.SetActive(true);
            }
            click(ray);
            return;
            
        }
        click(ray);
        // if (Input.GetKeyDown((KeyCode.Mouse0)))
        // {
        //     
        //     if (Physics.Raycast(ray, out hit))
        //     {
        //         if (hit.collider.CompareTag("nextStep") && !animate)
        //         {
        //             instantiate();
        //             hit = new RaycastHit();
        //         }else if (hit.collider.CompareTag("nextStep") && animate)
        //         {
        //             
        //         }
        //     }
        //     
        // }

        

        try
        {
            if(animate && hit.collider.CompareTag("nextStep"))
            {
            
                //Debug.Log("POINT: "+point);
                //Debug.Log("Instantiated: "+instantiated.Count);
                //Debug.Log("InstantiatedTools: "+instantiatedTools.Count);
                //Debug.Log("Tool Step: " + toolStep);
                //Debug.Log("Part Step: " + partStep);
                if (tool)
                {
                    String temp = (point).ToString();
                    cam.GetComponent<cameraController>().setTarget(instantiated[0].transform.Find(temp).transform);
                    Transform t = instantiated[0].transform.Find(temp).transform;
                
                    instantiatedTools[toolStep-1].transform.position = Vector3.MoveTowards(instantiatedTools[toolStep-1].transform.position, t.position, 1*Time.deltaTime);
                    if (Vector3.Distance(t.position, instantiatedTools[toolStep - 1].transform.position) < 0.001f)
                    {
                        animate = false;
                        tool = false;
                    
                    } 
                }
                else
                {
                    String temp = (point).ToString();
                    Transform t = instantiated[0].transform.Find(temp).transform;
                    //instantiated[1].transform.position = Vector3.MoveTowards(instantiated[1].transform.position, instantiated[0].transform.position, 1*Time.deltaTime);
                    instantiated[partStep-1].transform.position = Vector3.MoveTowards(instantiated[partStep-1].transform.position, t.position, 1*Time.deltaTime);


                    if (Vector3.Distance(t.position, instantiated[partStep - 1].transform.position) < 0.001f)
                    {
                        animate = false;
                        tool = true;
                        point++;
                    
                    
                    }  
                }
            
            
            
            }
        }
        catch (Exception)
        {
            
        }
        
        
    }

    private void click(Ray ray)
    {
        if (Input.GetKeyDown((KeyCode.Mouse0)))
        {
            
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("nextStep") && !animate && step!= maxStep)
                {
                    instantiate();
                    hit = new RaycastHit();
                }else if (hit.collider.CompareTag("nextStep") && animate)
                {
                    
                }else if (hit.collider.CompareTag("Finish") && step == maxStep)
                {
                    // You completed the asssembly of the product in ... time:
                    Debug.Log("FINISH - Build Time: " + finishTime);
                }
            }
            
        }
    }

    private void instantiate()
    {
         
        //Debug.Log(parts[step]);
       // Debug.Log(Quaternion.identity);
        //instantiated.Add(Instantiate(prefabs[step],vectors[step],Quaternion.identity));
        if (partStep.Equals(0))
        {
            instantiated.Add(Instantiate(parts[partStep],a,Quaternion.identity));
            instantiated[partStep].transform.parent = transform;
            partStep++;
            tool = true;
        }
        else
        {
            if (!tool)
            {
                instantiated.Add(Instantiate(parts[partStep],b,rotation));
                instantiated[partStep].transform.parent = transform;
                partStep++;
                animate = true;
                //tool = true;
            }
            else
            {
                instantiatedTools.Add(Instantiate(tools[0],b,rotation));
                instantiatedTools[toolStep].transform.parent = transform;
                toolStep++;
                //instantiated.Add(Instantiate(tools[0],b,Quaternion.identity));
                animate = true;
            }
                
        }
            
        Debug.Log(step);
        step++;
        
    }

    private void previousStep()
    {
       // insta
    }
}