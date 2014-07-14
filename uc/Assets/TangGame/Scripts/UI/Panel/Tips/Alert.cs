using UnityEngine;
using System.Collections;

namespace TangGame.UI{

  /// 提示返回类型
  public enum AlertType{
    Ok,
    Cancel,
  }
  /// 提示回调
  public delegate void AlertCallback (AlertType type, object obj);

  /// 提示
  public class Alert : MonoBehaviour {
    
    /// 该对象实例
    private static Alert mInstance;
    
    private AlertPanel panel;
    private TweenScale tween;
    private bool started;
    /// 信息文本
    public string msg;
    /// 传递的参数
    public object param;
    /// 回调
    public AlertCallback callback;

    void Start () {
      Object obj = Resources.Load("Prefabs/Tips/AlertPanel", typeof(GameObject));
      GameObject go = GameObject.Instantiate(obj) as GameObject;
      go.transform.parent = this.gameObject.transform;
      go.transform.localScale = Vector3.one;
      go.transform.localPosition = Vector3.zero;
      panel = go.GetComponent<AlertPanel>();
      panel.okDelegate += OkClickHandler;
      panel.cancelDelegate += CancelClickHandler;
      tween = this.gameObject.AddComponent<TweenScale>();
      tween.from = new Vector3(0, 0, 1);
      tween.duration = 0.1f;
      started = true;
      UpdateDisplay();
    }

    private void Open(){
      if(started){
        this.gameObject.SetActive(true);
        UpdateDisplay();
      }
    }
    
    /// 关闭隐藏Tips
    private void Close(){
      this.gameObject.SetActive(false);
      callback = null;
      param = null;
    }

    /// 更新数据的显示
    private void UpdateDisplay(){
      if(panel == null){return;}
      tween.ResetToBeginning();
      tween.Play();
      panel.SetAlert(msg);
    }

    /// 点击OK按钮
    private void OkClickHandler(GameObject go){
      if(callback != null){
        callback(AlertType.Ok, param);
      }
      Close();
    }

    /// 点击取消按钮
    private void CancelClickHandler(GameObject go){
      if(callback != null){
        callback(AlertType.Cancel, param);
      }
      Close();
    }
    //=====================================================================================
    //=====================================================================================

    /// 显示Alert
    public static Alert Show(string msg, AlertCallback callback, object param){
      if(mInstance == null){
        GameObject go = new GameObject("Alert");
        go.layer = Global.UILayer;
        mInstance = go.AddComponent<Alert>();
      }
      mInstance.transform.parent = UICamera.current.transform.parent;
      mInstance.transform.localPosition = new Vector3(0, 0, 0);
      mInstance.transform.localScale = Vector3.one;
      mInstance.msg = msg;
      mInstance.callback = callback;
      mInstance.param = param;
      mInstance.Open();
      return mInstance;
    }
    
    /// 隐藏
    public static void Hiddle(){
      if(mInstance != null){
        mInstance.Close();
      }
    }


  }
}

