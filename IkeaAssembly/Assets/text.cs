using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using TMPro;
using UnityEngine;
using System.Net.Http.Headers;




public class text : MonoBehaviour
{
    //[SerializeField]
    private TextMeshProUGUI tex1;
    private string textFromDb;

    //private InstructionList list = new InstructionList();
    
    // Start is called before the first frame update
    void Start()
    {
        tex1 = gameObject.GetComponent<TextMeshProUGUI>();
        //tex1.text = "Hvad det nu er "; //textFromDb;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setInstruction(int instruction)
    {
        tex1.text = GetComponent<Api>().getInstruction(instruction);
    }

   
}
