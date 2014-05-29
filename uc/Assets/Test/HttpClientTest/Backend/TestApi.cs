using UnityEngine;
using System.Collections;

using Newtonsoft.Json;

namespace ClientDemoTest
{
  public class GithubApiResult{
    public string user_url;
  }

  public class TestApi {

    public void httpsLocal(System.Action<GithubApiResult> responseHandler) {
      System.Action<string> handler = delegate(string jsonData){
        GithubApiResult result = JsonConvert.DeserializeObject<GithubApiResult> (jsonData);
        responseHandler (result);
      };

      RestApi.Instance.Host = "https://192.168.1.101:4443";
      RestApi.Instance.HttpsGet ("/", handler);
    }

    public void httpLocal(System.Action<GithubApiResult> responseHandler) {
      System.Action<string> handler = delegate(string jsonData){
        GithubApiResult result = JsonConvert.DeserializeObject<GithubApiResult> (jsonData);
        responseHandler (result);
      };
      RestApi.Instance.Host = "http://192.168.1.101:4004";
      RestApi.Instance.HttpGet ("/", handler);
    }


    public void httpsGithub(System.Action<GithubApiResult> responseHandler) {
      System.Action<string> handler = delegate(string jsonData){
        GithubApiResult result = JsonConvert.DeserializeObject<GithubApiResult> (jsonData);
        responseHandler (result);
      };
      RestApi.Instance.Host = "https://api.github.com";
      RestApi.Instance.HttpsGet ("/", handler);
    }
  }
}