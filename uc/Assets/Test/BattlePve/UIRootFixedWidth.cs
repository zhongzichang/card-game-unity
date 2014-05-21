using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class UIRootFixedWidth : MonoBehaviour {
  #region Variables
  // 想要固定的螢幕寬度
  public int m_width = 960;
  // 被指定的 Camera
  private UIRoot m_root;
  #endregion

  #region Behaviour
  private void Awake() {
    m_root = GetComponent<UIRoot> ();
  }

  void Start() {
    if(m_root == null)
      return;

    if (camera.aspect < 1.5f) {
      m_root.manualHeight = Screen.height * m_width / Screen.width;
    }
  }
  #endregion
}