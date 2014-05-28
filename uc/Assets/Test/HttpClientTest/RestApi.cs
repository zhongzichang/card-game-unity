using UnityEngine;
using System.Collections;

using System.Text;

namespace ClientDemoTest
{
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

    private string host = "http://localhost:4004";
    public string Host{
      get{ return host; }
      set{ host = value; }
    }

    private string appId = "appId";
    private string appKey = "appKey";
    private string accessToken = "accessToken";

    public void HttpGet(string path, System.Action<string> getResponseHandler){
      string url = host + path;
      StartCoroutine(RestApi.Instance.WaitForResponse(new WWW(url), getResponseHandler));

    }

    public void HttpsGet(string path, System.Action<string> getResponseHandler){
      string url = host + path;
      StartCoroutine(RestApi.Instance.WaitForResponse(new WWW(url), getResponseHandler));
    }

    public void HttpPost(string path, string jsonData, System.Action<string> postResponseHandler){
      WWWForm form = new WWWForm();
      Hashtable headers = form.headers;
      headers["Content-Type"] = "application/json";
      headers["x-appid"] = appId;
      headers["x-appkey"] = appKey;
      if(accessToken != null)
        headers["Authorization"] = accessToken;

      form.AddBinaryData("data", Encoding.UTF8.GetBytes(jsonData));

      // string url = "https://api.kii.com/api/apps/" + appId + "/server-code/versions/current/" + endpoint;
//      string url = host + "hero/heroList?userId=1";
      string url = host + path;
      WWW www = new WWW(url, form);
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
  }
}

