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
    private HeroBhvr heroBhvr;
    private float max_scale = 1F;
    private float backup_scale = 1F;
    private float currentScale = 1;
    private float scaleStep = 1F; // 每秒增加
    private bool scaling = false; // 是否要缩小

    #region MonoMethods

    void Start ()
    {
      myTransform = transform;
      if (statusBhvr == null) {
        statusBhvr = GetComponent<HeroStatusBhvr> ();
      }
      agent = GetComponent<DirectedNavAgent> ();

      heroBhvr = GetComponent<HeroBhvr> ();

      LevelController.BigMoveStart += OnBigMoveStart;
      LevelController.BigMoveEnd += OnBigMoveEnd;
    }

    void Update ()
    {
      if (statusBhvr.IsBigMove) {
        if (currentScale < max_scale) {
          currentScale += Time.deltaTime * scaleStep;
          myTransform.localScale = new Vector3 (currentScale, currentScale, 1F);
        }

      } else if (scaling) {

        if (backup_scale < currentScale) {
          currentScale -= Time.deltaTime * scaleStep * 2;
          myTransform.localScale = new Vector3 (currentScale, currentScale, 1F);
        } else {
          scaling = false;
        }
      }
    }
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
      agent.enabled = false;

      backup_scale = myTransform.localScale.x;
      max_scale = backup_scale * 1.5F;
      currentScale = backup_scale;
    }

    public void StopBigMove ()
    {
      myTransform.localPosition = backupPos;
      //Debug.Log ("StopBigMove");
      //this.enabled = false;
      statusBhvr.IsBigMove = false;

      LevelController.BigMoveCounter--;

      heroBhvr.hero.mp = 0;
      agent.enabled = true;

      // 进行缩小
      scaling = true;
    }

    #endregion
  }
}

