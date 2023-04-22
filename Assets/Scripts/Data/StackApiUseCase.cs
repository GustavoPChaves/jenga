using System.Collections;
using UnityEngine.Networking;
using System;
public class StackApiUseCase
{
    /// <summary>
    /// Fetch API Data, decode JSON into StackModel and calls completion callback when finish
    /// </summary>
    /// <param name="callback"></param>
    /// <returns></returns>
    public IEnumerator FetchData(System.Action<StackModel> callback)
    {
        using UnityWebRequest request = UnityWebRequest.Get(JengaNetworkSettings.URL);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            StackModel stackModel = StackModel.FromJson(request.downloadHandler.text);
            callback(stackModel);
            yield return null;
        } else {
            throw new Exception(request.error);
        }
    }
}