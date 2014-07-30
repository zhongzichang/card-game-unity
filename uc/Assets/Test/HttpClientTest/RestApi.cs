using UnityEngine;
using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace ClientDemoTest
{
  public class RestApiParam
  {
    private string appId = "appId";
    private string appKey = "appKey";
    private string accessToken = "accessToken";



    private WWWForm form = new WWWForm ();

    public WWWForm Form {
      get{ return form; }
    }

    public RestApiParam ()
    {
      form.headers ["Content-Type"] = "application/json";
      form.headers ["x-appid"] = appId;
      form.headers ["x-appkey"] = appKey;
      form.headers ["Authorization"] = accessToken;
    }

    public void AddField (string key, string val)
    {
      form.AddField (key, val);
    }

    public void AddBinaryData (string jsonData)
    {
      form.AddBinaryData ("data", Encoding.UTF8.GetBytes (jsonData));
    }
  }

  public class RestApi : MonoBehaviour
  {

    private static RestApi _instance;

    public static RestApi Instance {
      get {
        if (_instance == null) {
          var go = new GameObject ("RestApi");
          _instance = go.AddComponent<RestApi> ();
          DontDestroyOnLoad (go);
          baseUrl = host + prefix;
        }
        return _instance;
      }
    }

    private static string host = "http://115.28.229.143";

    public string Host {
      get{ return host; }
      set{ host = value; }
    }

    private static string prefix = "/card-game";

    private static string baseUrl = null;


    private static Hashtable header = new Hashtable ();
    private static string requestUrl = null;
    private static byte[] requestData = null;
    private static System.Action<string> m_responseHandler;

    public void HttpGet (string path, System.Action<string> responseHandler)
    {
      header["Cookie"] = PlayerPrefs.GetString("cookies");
      requestUrl = baseUrl + path;
      requestData = null;
      m_responseHandler = responseHandler;
      StartCoroutine (RestApi.Instance.WaitForResponse (
        new WWW (requestUrl, requestData, header), m_responseHandler));
    }

    public void HttpsGet (string path, System.Action<string> responseHandler)
    {
      header["Cookie"] = PlayerPrefs.GetString("cookies");
      requestUrl = baseUrl + path;
      requestData = null;
      m_responseHandler = responseHandler;
      StartCoroutine (RestApi.Instance.WaitForResponse (
        new WWW (requestUrl, requestData, header), m_responseHandler));
    }

    public void HttpPost (string path, RestApiParam param, System.Action<string> responseHandler)
    {
      header["Cookie"] = PlayerPrefs.GetString("cookies");
      requestUrl = baseUrl + path;
      requestData = param.Form.data;
      m_responseHandler = responseHandler;
      StartCoroutine (RestApi.Instance.WaitForResponse (
        new WWW (requestUrl, requestData, header), m_responseHandler));
    }

    public void ReTry(){
      StartCoroutine (RestApi.Instance.WaitForResponse (
        new WWW (requestUrl, requestData, header), m_responseHandler));
    }

    public IEnumerator WaitForResponse (WWW www, System.Action<string> onComplete)
    {

      yield return www;


      if (!String.IsNullOrEmpty (www.error)) {

        // 保存 cookies
        String[] cookieSplits = Regex.Split(www.responseHeaders["SET-COOKIE"],";");
        if( cookieSplits != null && cookieSplits.Length > 0 ){
          PlayerPrefs.SetString ("Cookie", cookieSplits[0]);
        }

        // 处理
        onComplete (www.text);

        www.Dispose ();



      } else {


        Debug.LogError (www.error);

        // 显示重试对话框



      }
    }
  }
}

