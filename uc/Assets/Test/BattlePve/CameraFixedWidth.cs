using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraFixedWidth : MonoBehaviour {
  #region Variables
  // 想要固定的螢幕寬度
  public int m_width = 960;
  // 被指定的 Camera
  private Camera m_camera;
  #endregion

  #region Behaviour
  private void Awake() {
    m_camera = camera;
  }

  void Start() {
    if( m_camera == null )
      return ;

    if (m_camera.aspect < 1.5f)
    {
      float size = (float)m_width * Screen.height / Screen.width/2;
      m_camera.orthographicSize = size;
    }
  }
  #endregion
}