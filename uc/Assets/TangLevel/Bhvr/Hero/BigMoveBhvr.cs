using System;
using UnityEngine;
using System.Collections.Generic;

namespace TangLevel
{
  public class BigMoveBhvr : MonoBehaviour
  {
    /// <summary>
    /// 大招开始
    /// </summary>
    public static event EventHandler BigMoveStart;
    /// <summary>
    /// 大招结束
    /// </summary>
    public static event EventHandler BigMoveEnd;

    public float pauseTime = 1F;
    private float remainTime = 0;
    private HeroStatusBhvr statusBhvr;
    private HashSet<BigMoveBhvr> bigMoveSenders = new HashSet<BigMoveBhvr> ();
    private bool inited = false;

    #region MonoMethods

    void Update ()
    {
      remainTime -= Time.deltaTime;
      if (remainTime < 0) {
        StopBigMove ();
      }
    }

    void OnEnable ()
    {
      remainTime = pauseTime;
      if (!inited) {
        inited = true;
        if (statusBhvr == null) {
          statusBhvr = GetComponent<HeroStatusBhvr> ();
        }

        BigMoveStart += OnBigMoveStart;
        BigMoveEnd += OnBigMoveEnd;
      }
    }

    #endregion

    #region SceneEvents

    /// <summary>
    /// 大招开始
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    private void OnBigMoveStart (object sender, EventArgs args)
    {
      //Debug.Log ("OnBigMoveStart");
      BigMoveBhvr hb = sender as BigMoveBhvr;
      if (hb != null && hb != this) {
        // 别人放的大招
        if (!statusBhvr.IsBigMove) { // 本人没有放大招
          Debug.Log ("OnBigMoveStart1");
          if (!bigMoveSenders.Contains (hb)) {
            Debug.Log ("OnBigMoveStart2");
            bigMoveSenders.Add (hb);
          }
          statusBhvr.IsPause = true;
        }
      }
    }

    /// <summary>
    /// 大招结束
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    private void OnBigMoveEnd (object sender, EventArgs args)
    {
      //Debug.Log ("OnBigMoveEnd");
      BigMoveBhvr hb = sender as BigMoveBhvr;
      if (hb != null && hb != this) { 
        // 别人放的大招
        if (bigMoveSenders.Contains (hb)) {
          bigMoveSenders.Remove (hb);
        }
        if (bigMoveSenders.Count == 0 && statusBhvr.IsPause) { // 如果没有其他人放大招，恢复动画
          statusBhvr.IsPause = false;
        }
      }
    }

    #endregion

    #region PublicMethods

    public void StartBigMove ()
    {
      //Debug.Log ("StartBigMove");
      this.enabled = true;
      statusBhvr.IsBigMove = true;

      if (BigMoveStart != null) {
        BigMoveStart (this, EventArgs.Empty);
      }
    }

    public void StopBigMove ()
    {
      //Debug.Log ("StopBigMove");
      this.enabled = false;
      statusBhvr.IsBigMove = false;

      if (BigMoveEnd != null) {
        BigMoveEnd (this, EventArgs.Empty);
      }
    }

    #endregion
  }
}

