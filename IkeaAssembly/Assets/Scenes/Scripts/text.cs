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
    private TextMeshProUGUI textField;
    [SerializeField]
    private GameObject assemblyManager;

    // Start is called before the first frame update
    void Start()
    {
        textField = gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void setInstruction(int instruction)
    {
        textField.text = assemblyManager.GetComponent<Api>().getInstruction(instruction);
    }

   
}
