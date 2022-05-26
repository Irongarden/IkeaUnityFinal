using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
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
    private TextMeshPro assemblyFinishText;

    [SerializeField]
    private GameObject finishPanel;
    
    private List<GameObject> instantiated;
    private List<GameObject> instantiatedTools;

    private int toolStep = 0;
    private int partStep = 0;
    private Boolean animationRunning = false;
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
        assemblyFinishText.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log(animationRunning);
        timer += Time.deltaTime;
        //Debug.Log("Step: "+step);
        //Debug.Log("Animate: "+ animate);
        
        b = Camera.main.transform.position + Vector3.forward;
        rotation = transform.rotation;
        //hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (step>=maxStep && !animationRunning)
        {
            
            if (finishTime == 0)
            {
                finishTime = timer;
                finishPanel.SetActive(true);
                Debug.Log("Finished - " +finishTime);
            }
            click(ray);
            return;
            
        }
        click(ray);


        try
        {
            if(animationRunning && hit.collider.CompareTag("nextStep"))
            {
                animate();
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
            Debug.Log("Step: "+step);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("nextStep") && !animationRunning && step!= maxStep)
                {
                    instantiate();
                    hit = new RaycastHit();
                }else if (hit.collider.CompareTag("Finish") && step == maxStep)
                {
                    // You completed the asssembly of the product in ... time:
                    //Debug.Log("FINISH - Build Time: " + finishTime);
                }else if (hit.collider.CompareTag("prevStep") && !animationRunning && step > 0)
                {
                    Debug.Log("Prev step");
                    previousStep();
                    hit = new RaycastHit();
                }
            }
            
        }
    }

    public void animate()
    {
        if (tool)
        {
            String temp = (point).ToString();
            cam.GetComponent<cameraController>().setTarget(instantiated[0].transform.Find(temp).transform);
            Transform t = instantiated[0].transform.Find(temp).transform;
                
            instantiatedTools[toolStep-1].transform.position = Vector3.MoveTowards(instantiatedTools[toolStep-1].transform.position, t.position, 1*Time.deltaTime);
            if (Vector3.Distance(t.position, instantiatedTools[toolStep - 1].transform.position) < 0.001f)
            {
                animationRunning = false;
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
                animationRunning = false;
                tool = true;
                point++;
                    
                    
            }  
        }
    }

    public void nextStep(Ray ray, RaycastHit hits)
    {
        if (step != maxStep)
        {
            instantiate(); 
        }
        
    }

    public void finishBuild()
    {
        if (step == maxStep)
        {
            Debug.Log("FINISH - Build Time: " + finishTime);
            assemblyFinishText.text = "Congrats! You completed the assembly of ProductName in " + finishTime + " Seconds";

            assemblyFinishText.enabled = true;
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
                animationRunning = true;
                //tool = true;
                
            }
            else
            {
                instantiatedTools.Add(Instantiate(tools[0],b,rotation));
                instantiatedTools[toolStep].transform.parent = transform;
                toolStep++;
                //instantiated.Add(Instantiate(tools[0],b,Quaternion.identity));
                animationRunning = true;
            }
                
        }
        step++;
        
    }

    private void previousStep()
    {
        
        
        Debug.Log("Previous step?");
        if (tool)
        {
            partStep--;
            step--;
            Debug.Log("Instantiated Parts: "+instantiated);
            Destroy(instantiated[partStep]);
            instantiated.RemoveAt(partStep);
            tool = false;
            if (step >0)
            {
                point--;
            }
        }
        else
        {
            toolStep--;
            step--;
            Debug.Log("Instantiated Tools: "+instantiatedTools);
            Destroy(instantiatedTools[toolStep]);
            instantiatedTools.RemoveAt(toolStep);
            tool = true;
        }
    }

    public Boolean getAnimationBool()
    {
        return animationRunning;
    }
    
}