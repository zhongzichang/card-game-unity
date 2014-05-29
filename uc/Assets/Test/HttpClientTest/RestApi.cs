using UnityEngine;
using System.Collections;

using System.Text;

namespace ClientDemoTest
{
  public class RestApiParam
  {
    private string appId = "appId";
    private string appKey = "appKey";
    private string accessToken = "accessToken";

    private WWWForm form =  new WWWForm();
    public WWWForm Form{
      get{ return form; }
    }

    public RestApiParam(){
      form.headers["Content-Type"] = "application/json";
      form.headers["x-appid"] = appId;
      form.headers["x-appkey"] = appKey;
      form.headers["Authorization"] = accessToken;
    }

    public void AddField(string key, string val){
      form.AddField (key, val);
    }

    public void AddBinaryData(string jsonData){
      form.AddBinaryData("data", Encoding.UTF8.GetBytes(jsonData));
    }
  }

  public class RestApi : MonoBehaviour{

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

    private string host = "http://192.168.1.101:4004";
    public string Host{
      get{ return host; }
      set{ host = value; }
    }

    public void HttpGet(string path, System.Action<string> getResponseHandler){
      string url = host + path;
      Debug.Log (url);
      StartCoroutine(RestApi.Instance.WaitForResponse(new WWW(url), getResponseHandler));
    }

    public void HttpsGet(string path, System.Action<string> getResponseHandler){
      string url = host + path;
      StartCoroutine(RestApi.Instance.WaitForResponse(new WWW(url), getResponseHandler));
    }

    public void HttpPost(string path, RestApiParam param, System.Action<string> postResponseHandler){
      string url = host + path;
      StartCoroutine(RestApi.Instance.WaitForResponse(new WWW(url, param.Form), postResponseHandler));
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

