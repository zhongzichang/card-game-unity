using UnityEngine;
using System.Collections;


/// <summary>
/// 处理平台相关，不需要拖脚本
/// </summary>
public class PlatformMoudle : MonoBehaviour {

  private static PlatformMoudle mInstance;
  public static PlatformMoudle instance {
    get {
      if (mInstance == null) {
        var go = new GameObject("PlatformMoudle");
        mInstance = go.AddComponent<PlatformMoudle>();
        DontDestroyOnLoad(go);
      }
      return mInstance;
    }
  }



  public delegate void PBackCall(object result);






  public event PBackCall HandlePlatformData;

  /// 初始化相关信息
  public void Init(){

  }

  /// 处理平台的数据
  public void _HandlePlatformData(string jsonString){

  }
  
  /// 向平台发送数据
  public void SendPlatformData(string options){
    #if (UNITY_ANDROID) && !UNITY_EDITOR
    _SendPlatformData(options);
    #endif
  }

  //============================IPHONE============================
  #if UNITY_IPHONE && !UNITY_EDITOR
  //[DllImport("__Internal")]
  //public static extern bool sendPlatformData(string jsonString);
  #endif

  //============================ANDROID============================
  #if UNITY_ANDROID && !UNITY_EDITOR
  private static AndroidJavaClass mModuleClass = null;
  private static AndroidJavaObject mModule = null;
  private static void initializeClass(){
    if (mModuleClass == null){
      mModuleClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    }
    mModule = mModuleClass.GetStatic<AndroidJavaObject>("currentActivity");
  }
  
  public static void _SendPlatformData(string jsonString){
    initializeClass();
    mModule.CallStatic("handleUnityData", new object[] {jsonString});
  }
  #endif

}
