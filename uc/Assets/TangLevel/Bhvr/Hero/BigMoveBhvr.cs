using System;
using UnityEngine;
using System.Collections.Generic;
using DBE = DragonBones.Events;
using TDB = TangDragonBones;
using DragonBones;

namespace TangLevel
{
  public class BigMoveBhvr : MonoBehaviour
  {
    public static readonly Vector3 OFFSET = new Vector3 (0F, 0F, -100F);
    public const float SCALE = 1.3F;
    private HeroStatusBhvr statusBhvr;
    private TDB.DragonBonesBhvr dbBhvr; // Dragonbones
    private HashSet<BigMoveBhvr> bigMoveSenders = new HashSet<BigMoveBhvr> ();
    private bool inited = false;
    private Transform myTransform;
    private Vector3 backupPos = Vector3.zero;
    private DirectedNavAgent agent;
    private HeroBhvr heroBhvr;
    private Vector3 backupScale = Vector3.zero;
    private Armature armature;
    private Skill skill;

    #region MonoMethods

    void Start ()
    {
      myTransform = transform;
      if (statusBhvr == null) {
        statusBhvr = GetComponent<HeroStatusBhvr> ();
      }
      agent = GetComponent<DirectedNavAgent> ();
      heroBhvr = GetComponent<HeroBhvr> ();

      // 原始人物比例
      backupScale = myTransform.localScale;

      // 获取大招技能
      skill = heroBhvr.BigMoveSkill ();

    }

    void Update ()
    {
      // 如果能量足够，能不能施放大招
      if (skill != null && heroBhvr.hero.maxMp == heroBhvr.hero.mp) {



      }
    }

    void OnEnable ()
    {
      if (!inited) {
        if (statusBhvr == null) {
          statusBhvr = GetComponent<HeroStatusBhvr> ();
        }
        LevelController.BigMoveStart += OnBigMoveStart;
        LevelController.BigMoveEnd += OnBigMoveEnd;

        // DragonBonesBhvr
        if (dbBhvr == null) {
          // dragonbones behaviour
          dbBhvr = GetComponent<TDB.DragonBonesBhvr> ();
          dbBhvr.GotoAndPlay (statusBhvr.Status.ToString ());
          armature = dbBhvr.armature;
        }
        // armature
        if (armature != null) {
          armature.AddEventListener (DBE.FrameEvent.ANIMATION_FRAME_EVENT, OnAnimationFrameEvent);
        }

        inited = true;
      }
    }

    void OnDisable ()
    {

      LevelController.BigMoveStart -= OnBigMoveStart;
      LevelController.BigMoveEnd -= OnBigMoveEnd;

      // armature
      if (armature != null) {
        armature.RemoveEventListener (DBE.FrameEvent.ANIMATION_FRAME_EVENT, OnAnimationFrameEvent);
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

    #region DragonBonesEvents

    private void OnAnimationFrameEvent (Com.Viperstudio.Events.Event e)
    {

      DBE.FrameEvent evt = e as DBE.FrameEvent;
      if (evt != null) {

        // 如果是人物比例还原
        if (Config.SCALE_RESUME_LABEL.Equals (evt.FrameLabel)) {
          // 人物比例还原
          myTransform.localScale = backupScale;
        }
      }
    }

    #endregion

    #region PublicMethods

    /// <summary>
    /// 开始施放大招
    /// </summary>
    public void StartBigMove ()
    {

      backupPos = myTransform.localPosition;
      myTransform.localPosition += OFFSET;

      backupScale = myTransform.localScale;
      myTransform.localScale = new Vector3 (backupScale.x * SCALE, backupScale.y * SCALE, 1);

      statusBhvr.IsBigMove = true;
      LevelController.BigMoveCounter++;

      // 如果被暂停，则恢复
      if (statusBhvr.IsPause)
        statusBhvr.IsPause = false;
      agent.enabled = false;
    }

    /// <summary>
    /// 大招施放完毕
    /// </summary>
    public void StopBigMove ()
    {
      myTransform.localPosition = backupPos;

      statusBhvr.IsBigMove = false;

      LevelController.BigMoveCounter--;

      heroBhvr.hero.mp = 0;
      agent.enabled = true;

      // 人物比例还原
      myTransform.localScale = backupScale;
    }

    #endregion
  }
}

