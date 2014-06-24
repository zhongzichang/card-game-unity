using UnityEngine;
using System.Collections;
using System.Text;
using TangGame;

/// Post请求的表单参数
public class NetWorkParam{

  private string appId = "appId";
  private string appKey = "appKey";
  private string accessToken = "accessToken";
  
  private WWWForm form =  new WWWForm();
  public WWWForm Form{
    get{ return form; }
  }
  
  public NetWorkParam(){
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


/// <summary>
/// 网络通信，不需要外部拖脚本
/// </summary>
public class NetWork : MonoBehaviour {

  private static NetWork mInstance;
  public static NetWork instance {
    get {
      if (mInstance == null) {
        var go = new GameObject("NetWork");
        mInstance = go.AddComponent<NetWork>();
        DontDestroyOnLoad(go);
      }
      return mInstance;
    }
  }

  private string mHost = "http://192.168.1.101:4004";

  /// 地址,例：http://192.168.1.101:4004
  public string host{
    get{ return mHost; }
    set{ mHost = value; }
  }

  /// Http请求
  public void HttpGet(string path, System.Action<string> getResponseHandler){
    string url = host + path;
    Global.Log(">> HttpGet url = " + url);
    StartCoroutine(WaitForResponse(new WWW(url), getResponseHandler));
  }

  /// Http请求
  public void HttpPost(string path, NetWorkParam param, System.Action<string> postResponseHandler){
    string url = host + path;
    Global.Log(">> HttpPost url = " + url);
    StartCoroutine(WaitForResponse(new WWW(url, param.Form), postResponseHandler));
  }
  
  IEnumerator WaitForResponse(WWW www, System.Action<string> onComplete){
    yield return www;
    if (www.error == null) {
      onComplete(www.text); 
    } else {
      Global.LogError (">> www.url " + www.url);
      Global.LogError (">> www.error " + www.error);
    }
  }

}
