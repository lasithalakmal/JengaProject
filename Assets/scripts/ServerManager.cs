using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    public static IEnumerator GetRequest(string uri) {
        
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)) {
            
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.ConnectionError) {
                string txt = webRequest.downloadHandler.text;
                GameController.g.analyzeJsonText(txt);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
