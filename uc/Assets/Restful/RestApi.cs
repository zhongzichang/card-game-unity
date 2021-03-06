using UnityEngine;
using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

using Newtonsoft.Json;
namespace Restful
{
  public class RestApiParam
  {

    private WWWForm form = new WWWForm ();

    public WWWForm Form {
      get{ return form; }
    }

    public RestApiParam ()
    {
      //form.headers ["Content-Type"] = "application/json";
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
    public static readonly string SET_COOKIE_KEY = "SET-COOKIE";
    public static readonly string COOKIE_KEY = "Cookie";
    //private static string host = "http://115.28.229.143";
    private static string host = "http://localhost:8080";
    private static RestApi _instance;
    private static Hashtable cookieTable = new Hashtable ();

    public static RestApi Instance {
      get {
        if (_instance == null) {

          var go = new GameObject ("RestApi");
          _instance = go.AddComponent<RestApi> ();
          DontDestroyOnLoad (go);

          baseUrl = host + prefix;

          // init cookie table
          if (PlayerPrefs.HasKey (COOKIE_KEY)) {
            string[] items = PlayerPrefs.GetString(COOKIE_KEY).Split (new char[]{ ';' });
            foreach (string item in items) {
              string[] kv = item.Split (new char[]{ '=' });
              cookieTable [kv [0]] = kv [1];
            }
          }
          RestApi.header ["Accept"] = "application/json";
          RestApi.header ["Content-Type"] = "application/json";
        }
        return _instance;
      }
    }

    public static System.Action<string> Handle<T>(System.Action<T> responseHandler) 
    {

      System.Action<string> handler = delegate(string jsonData) {
        T result = JsonConvert.DeserializeObject<T> (jsonData);
        responseHandler (result);
      };
      return handler;
    }

    public string Host {
      get{ return host; }
      set{ host = value; }
    }

    private static string prefix = "/api";

    private static string baseUrl = null;


    private static Hashtable header = new Hashtable ();
    private static string requestUrl = null;
    private static byte[] requestData = null;
    private static System.Action<string> m_responseHandler;

    public void HttpGet (string path, System.Action<string> responseHandler)
    {
      header[COOKIE_KEY] = PlayerPrefs.GetString(COOKIE_KEY);
      requestUrl = baseUrl + path;
      requestData = null;
      m_responseHandler = responseHandler;
      StartCoroutine (RestApi.Instance.WaitForResponse (
        new WWW (requestUrl, requestData, header), m_responseHandler));
    }

    public void HttpFormPost (string path, RestApiParam param, System.Action<string> responseHandler)
    {
      header[COOKIE_KEY] = PlayerPrefs.GetString(COOKIE_KEY);
      requestUrl = baseUrl + path;
      requestData = param.Form.data;
      m_responseHandler = responseHandler;
      StartCoroutine (RestApi.Instance.WaitForResponse (
        new WWW (requestUrl, requestData, header), m_responseHandler));
    }

    public void HttpPost (string path, object obj, System.Action<string> responseHandler)
    {
      header[COOKIE_KEY] = PlayerPrefs.GetString(COOKIE_KEY);
      requestUrl = baseUrl + path;
      requestData = TangUtils.StringUtil.GetBytes(JsonConvert.SerializeObject(obj));
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


      if (String.IsNullOrEmpty (www.error)) {

        // 保存 cookies
        if (www.responseHeaders.ContainsKey (SET_COOKIE_KEY)) {
          String[] cookieSplits = Regex.Split (www.responseHeaders ["SET-COOKIE"], ";");
          if (cookieSplits != null && cookieSplits.Length > 0) {

            string[] kv = cookieSplits [0].Split (new char[]{ '=' });
            cookieTable [kv [0]] = kv [1];

            StringBuilder sb = new StringBuilder ();
            IDictionaryEnumerator iter = cookieTable.GetEnumerator ();
            while (iter.MoveNext ()) {
              sb.Append (iter.Key).Append ("=").Append (iter.Value).Append (";");
            }

            string cookie4save = sb.Remove (sb.Length - 1, 1).ToString ();
            PlayerPrefs.SetString (COOKIE_KEY, cookie4save);

            //Debug.Log ("cookie4save : " + cookie4save);
          }
        }

        // 处理
        onComplete (www.text);
        www.Dispose ();


      } else {


        Debug.LogError ("fail to open "+ www.url + " with error " + www.error);

        // TODO 显示重试对话框

      }
    }
  }
}

