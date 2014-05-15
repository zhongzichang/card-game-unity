using System;
using UnityEngine;
using System.Collections.Generic;

namespace TangLevel
{
  public class BigMoveBhvr : MonoBehaviour
  {
    public static readonly Vector3 OFFSET = new Vector3 (0F, 0F, -100F);
    public float pauseTime = 2F;
    private float remainTime = 0;
    private HeroStatusBhvr statusBhvr;
    private HashSet<BigMoveBhvr> bigMoveSenders = new HashSet<BigMoveBhvr> ();
    private bool inited = false;
    private Transform myTransform;
    private Vector3 backupPos = Vector3.zero;
    private DirectedNavAgent agent;

    #region MonoMethods

    void Start ()
    {
      myTransform = transform;
      if (statusBhvr == null) {
        statusBhvr = GetComponent<HeroStatusBhvr> ();
      }
      agent = GetComponent<DirectedNavAgent> ();
      LevelController.BigMoveStart += OnBigMoveStart;
      LevelController.BigMoveEnd += OnBigMoveEnd;
    }

    /*
    void Update ()
    {
      if (statusBhvr.IsBigMove) {
        remainTime -= Time.deltaTime;
        if (remainTime < 0) {
          StopBigMove ();
        }
      }
    }*/

    /*
    void OnEnable ()
    {
      if (!inited) {
        if (statusBhvr == null) {
          statusBhvr = GetComponent<HeroStatusBhvr> ();
        }

        LevelController.BigMoveStart += OnBigMoveStart;
        LevelController.BigMoveEnd += OnBigMoveEnd;
        inited = true;
      }
    }*/

    #endregion

    #region SceneEvents

    /// <summary>
    /// 大招开始
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    private void OnBigMoveStart (object sender, EventArgs args)
    {
      if (!statusBhvr.IsBigMove) { // 本人没有放大招
        statusBhvr.IsPause = true;
      }
    }

    /// <summary>
    /// 大招开始
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    private void OnBigMoveEnd (object sender, EventArgs args)
    {
      if (statusBhvr.IsPause) {
        statusBhvr.IsPause = false;
      }
    }

    #endregion

    #region PublicMethods

    public void StartBigMove (float pauseTime)
    {

      remainTime = pauseTime;
      backupPos = myTransform.localPosition;
      myTransform.localPosition += OFFSET;

      //Debug.Log ("StartBigMove");
      //this.enabled = true;
      statusBhvr.IsBigMove = true;
      LevelController.BigMoveCounter++;

      // 如果被暂停，则恢复
      if (statusBhvr.IsPause)
        statusBhvr.IsPause = false;
      //agent.enabled = false;
    }

    public void StopBigMove ()
    {
      myTransform.localPosition = backupPos;
      //Debug.Log ("StopBigMove");
      //this.enabled = false;
      statusBhvr.IsBigMove = false;

      LevelController.BigMoveCounter--;

      //agent.enabled = true;
    }

    #endregion
  }
}

