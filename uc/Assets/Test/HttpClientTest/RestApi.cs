using UnityEngine;
using System.Collections;

using System.Text;

namespace Test{
  public class RestApi : MonoBehaviour {

    private static RestApi _instance;
    public static RestApi Instance {
      get {
        if (_instance == null) {
          var go = new GameObject("RestApi");
          _instance = go.AddComponent<RestApi>();
          DontDestroyOnLoad(go);
        }
        return _instance;
      }
    }

    private string appId = "appId";
    private string appKey = "appKey";
    private string accessToken = "accessToken";

    public void HttpGet(string endpoint, System.Action<string> getResponseHandler){
      string url = "http://localhost:4004/hero/heroList?userId=1";
      StartCoroutine(RestApi.Instance.WaitForResponse(new WWW(url), getResponseHandler));
    }

    public void HttpPost(string endpoint, string jsonData, System.Action<string> postResponseHandler){
      WWW www = HttpRequest(endpoint, jsonData);
      StartCoroutine(RestApi.Instance.WaitForResponse(www, postResponseHandler));
    }

    public IEnumerator WaitForResponse(WWW www, System.Action<string> onComplete)
    {
      yield return www;

      if (www.error == null) {
        onComplete(www.text); 
      } else {
        Debug.LogError (www.error);
      }
    }

    private WWW HttpRequest(string endpoint, string jsonString){
      WWWForm form = new WWWForm();
      Hashtable headers = form.headers;
      headers["Content-Type"] = "application/json";
      headers["x-appid"] = appId;
      headers["x-appkey"] = appKey;
      if(accessToken != null)
        headers["Authorization"] = accessToken;

      form.AddBinaryData("data", Encoding.UTF8.GetBytes(jsonString));

      // string url = "https://api.kii.com/api/apps/" + appId + "/server-code/versions/current/" + endpoint;
      string url = "http://localhost:4004/hero/heroList?userId=1";
      return new WWW(url, form);
    }
  }
}

