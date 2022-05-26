using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using TMPro;
using UnityEngine;
using System.Net.Http.Headers;
public class text : MonoBehaviour
{
    private TextMeshPro tex1;
    private const string URL = "https://batchelor-project-ikea.herokuapp.com";
    private string urlParameters = "/";
    private string textFromDb;

    public void connec()
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(URL);
        
        // Add an Accept header for JSON format.
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        
        // List data response.
        HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
        if (response.IsSuccessStatusCode)
        {
            Debug.Log("Responce is successful");
            Debug.Log(response.Content.ReadAsStringAsync().Result);
            textFromDb = response.Content.ReadAsStringAsync().Result;
            // Parse the response body.
            var dataObjects = response.Content.ReadAsStreamAsync().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
           
        }
        else
        {
            Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            Debug.Log(response.StatusCode);
            textFromDb = "Error: "+response.StatusCode.ToString();
        }

        client.Dispose();
    }
    
    

    

    
    
    // Start is called before the first frame update
    void Start()
    {
        connec();
        tex1 = gameObject.GetComponent<TextMeshPro>();
        tex1.text = "Hvad det nu er "; //textFromDb;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
