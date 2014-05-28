using UnityEngine;
using System.Collections;

//using Pathfinding.Serialization.JsonFx;
using Newtonsoft.Json;

namespace ClientDemoTest
{
  public class GithubApiResult{
    public string user_url;
  }

  public class HttpsTestService {

    public void httpsLocal(System.Action<GithubApiResult> responseHandler) {
      System.Action<string> handler = delegate(string jsonData){
  //      GithubApiResult result = JsonReader.Deserialize<GithubApiResult> (jsonData);
        GithubApiResult result = JsonConvert.DeserializeObject<GithubApiResult> (jsonData);
        responseHandler (result);
      };
      string url = "http://192.168.1.101:4443/";
      RestApi.Instance.HttpsGet (url, handler);
    }

    public void httpLocal(System.Action<GithubApiResult> responseHandler) {
      System.Action<string> handler = delegate(string jsonData){
  //      GithubApiResult result = JsonReader.Deserialize<GithubApiResult> (jsonData);
        GithubApiResult result = JsonConvert.DeserializeObject<GithubApiResult> (jsonData);
        responseHandler (result);
      };
      string url = "http://192.168.1.101:4004/";
      RestApi.Instance.HttpGet (url, handler);
    }


    public void httpsGithub(System.Action<GithubApiResult> responseHandler) {
      System.Action<string> handler = delegate(string jsonData){
  //      GithubApiResult result = JsonReader.Deserialize<GithubApiResult> (jsonData);
        GithubApiResult result = JsonConvert.DeserializeObject<GithubApiResult> (jsonData);
        responseHandler (result);
      };
      string url = "https://api.github.com/";
      RestApi.Instance.HttpsGet (url, handler);
    }
  }
}