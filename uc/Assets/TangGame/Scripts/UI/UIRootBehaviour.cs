using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 用于对UI的大小控制，随着分辨率的大小而设定UIRoot的height值
/// </summary>
[ExecuteInEditMode]
public class UIRootBehaviour : MonoBehaviour {
  /// UI设定的宽
  public const float WIDTH = 960;
  /// UI设定的高
  public const float HEIGHT = 640;

  static public List<UIRootBehaviour> list = new List<UIRootBehaviour>();
  
  /// 关联的UIROOT
  public UIRoot uiRoot = null;
  /// 屏幕大小
  public Vector2 screenSize = Vector2.zero;
  /// 用于UIAnchor设置Container的
  public UIWidget uiWidget = null;

	void Start () {
    screenSize = new Vector2(Screen.width, Screen.height);
    UIRoot temp = this.GetComponent<UIRoot>();
    if(temp != null){
      uiRoot = temp;
    }

    if(uiWidget == null){
      GameObject go = new GameObject("UIAnchorContainer");
      go.transform.parent = this.transform;
      go.transform.localPosition = Vector3.zero;
      go.transform.localScale = Vector3.one;
      uiWidget = go.AddComponent<UIWidget>();
    }

    UpdateUIRoot();
	}
	
	// Update is called once per frame
	void Update () {
    if(screenSize.x != Screen.width || screenSize.y != Screen.height){
      screenSize = new Vector2(Screen.width, Screen.height);
      UpdateUIRoot();
    }
	}

  protected virtual void OnEnable () { list.Add(this); }
  protected virtual void OnDisable () { list.Remove(this); }
  
  /// 更新UIRoot
  private void UpdateUIRoot(){
    if(uiRoot == null){return;}
    float defaultScale = WIDTH / HEIGHT;
    float scale = screenSize.x / screenSize.y;
    float height = HEIGHT;
    float width = height * scale;
    if(scale < defaultScale){
      height = WIDTH / scale;
      width = WIDTH;
    }

    uiRoot.manualHeight = (int)height;
    uiRoot.minimumHeight = (int)height;

    uiWidget.width = (int)width;
    uiWidget.height = (int)HEIGHT;//这里需要设定固定值

    UIAnchor[] anchors = this.GetComponentsInChildren<UIAnchor>();
    foreach(UIAnchor uiAnchor in anchors){
      uiAnchor.container = uiWidget.gameObject;
      uiAnchor.enabled = true;
    }

  }

  /// 给GameObject下所有的UIAnchor设置添加Container
  public void AddAnchorContainer(GameObject go){
    if(uiWidget == null){return;}
    UIAnchor[] anchors = go.GetComponentsInChildren<UIAnchor>();
    foreach(UIAnchor uiAnchor in anchors){
      uiAnchor.container = uiWidget.gameObject;
      uiAnchor.enabled = true;
    }
  }

  /// 给GameObject下所有的UIAnchor设置添加Container
  public static void AddContainer(GameObject go){
    foreach(UIRootBehaviour root in list){
      if(root != null){
        root.AddAnchorContainer(go);
        break;
      }
    }
  }
}
