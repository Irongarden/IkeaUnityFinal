using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class Api : MonoBehaviour
{
    
    private const string url = "https://batchelor-project-ikea.herokuapp.com/";
    private const string testUrl = "http://localhost:8080/";
    private const string instructions = "instructions/";
    private const string buildTime = "updateBuildTime";
    private string id = "1";
    private List<Instructions> list = new List<Instructions>();
    
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetRequest(url+instructions+id));
    }
    
    IEnumerator GetRequest(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            Debug.Log(url+instructions);
            yield return request.SendWebRequest();
            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(": Error: " + request.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(": HTTP Error: " + request.error);
                    break;
                case UnityWebRequest.Result.Success:
                    list = JsonConvert.DeserializeObject<List<Instructions>>(request.downloadHandler.text);
                    Debug.Log(list);
                    break;
            }
        }
    }

    IEnumerator postRequest(string url, string data)
    {
        data += ":"+id;
        using (UnityWebRequest request = UnityWebRequest.Put(url+buildTime,data))
        {
            yield return request.SendWebRequest();

            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(": Error: " + request.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(": HTTP Error: " + request.error);
                    break;
                case UnityWebRequest.Result.Success:
                    break;
            }
        }
    }

    public string getInstruction(int instruction)
    {
        Debug.Log(list[instruction].text);
        return list[instruction].text;
    }
    
    
    public void updateBuildTime(string bt)
    {
        StartCoroutine(postRequest(url, bt));
    }
}
[Serializable]
public class Instructions
{
    public int iD;
    public int stepNumber;
    public String text;
}
