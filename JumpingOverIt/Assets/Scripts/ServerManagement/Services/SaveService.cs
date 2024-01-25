using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class SaveService : MonoBehaviour
{
    private readonly string URL = "http://localhost:8080/api/saves";

    public delegate void SavesCallback(Save[] saves);

    public void GetSaves(SavesCallback callback)
    {
        StartCoroutine(RestGetAll(callback));
    }

    public void CreateSave(Save save)
    {
        StartCoroutine(RestCreate(save));
    }

    public void UpdateSave(string saveName,Save save)
    {
        StartCoroutine(RestUpdate(saveName, save));
    }

    public void DeleteSave(string saveName)
    {
        StartCoroutine(RestDelete(saveName));
    }
    IEnumerator RestGetAll(SavesCallback callback)
    {
        UnityWebRequest request = UnityWebRequest.Get(URL);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
        }
        else
        {
            string result = request.downloadHandler.text;

            Debug.Log(result);

            var saves = JsonHelper.getJsonArray<Save>(result);
            callback?.Invoke(saves);
        }

        request.Dispose();
    }

    IEnumerator RestCreate(Save save)
    {
        var request = new UnityWebRequest(URL, "POST");

        var bodyJsonString = JsonUtility.ToJson(save);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(bodyJsonString);

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Save upload complete!");
        }

        Debug.Log("Status Code: " + request.responseCode);

        request.Dispose();
    }

    IEnumerator RestUpdate(string saveName, Save save)
    {
        var request = new UnityWebRequest(URL + "/" + saveName, "PUT");

        var bodyJsonString = JsonUtility.ToJson(save);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(bodyJsonString);

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Save upload complete!");
        }

        Debug.Log("Status Code: " + request.responseCode);

        request.Dispose();
    }

    IEnumerator RestDelete(string saveName)
    {
        string URI = URL + "/" + saveName;
        UnityWebRequest request = UnityWebRequest.Delete(URI);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Save Deleted successfully!");
        }

        Debug.Log("Status Code: " + request.responseCode);

        request.Dispose();
    }
}
