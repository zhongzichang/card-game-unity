using System;
using UnityEngine;
using TDB = TangDragonBones;
using DragonBones;
using DBE = DragonBones.Events;
using System.Collections.Generic;

namespace TangLevel
{
  /// <summary>
  /// 英雄的基本行为
  /// </summary>
  [RequireComponent (typeof(DirectedNavigable), typeof(HeroStatusBhvr))]
  public class HeroBhvr : MonoBehaviour
  {
    #region Events

    /// <summary>
    /// 死亡通知
    /// </summary>
    public event EventHandler RaiseDead;

    #endregion

    #region Attributes

    public Hero hero;
    // 英雄数据
    public Skill skill;
    // 技能
    private DirectedNavigable navigable;
    // 导航
    private DirectedNavAgent agent;
    // 导航代理
    private HeroStatusBhvr statusBhvr;
    // 状态
    private Transform myTransform;
    // 变换
    private TDB.DragonBonesBhvr dbBhvr;
    // Dragonbones
    private Armature armature;
    // Dragonbones armature
    private SkillBhvr skillBhvr;
    // 技能
    private BigMoveBhvr bmBhvr;
    // 大招
    private List<string> animationList;
    // DragonBone 动画列表
    private GameObject mTarget;
    // 当前目标
    private GroupBhvr groupBhvr;

    #endregion

    #region Properties

    // 当前目标
    public GameObject target {
      get{ return mTarget; }
      private set{ mTarget = value; }
    }

    #endregion

    #region MonoBehaviours

    void Start ()
    {
      // nav agent
      agent = GetComponent<DirectedNavAgent> ();
      if (agent == null) {
        agent = gameObject.AddComponent<DirectedNavAgent> ();
      }
      // navigable
      navigable = GetComponent<DirectedNavigable> ();
      if (navigable == null) {
        navigable = gameObject.AddComponent<DirectedNavigable> ();
      }
      // transform
      myTransform = transform;
      // skill behaviour
      skillBhvr = GetComponent<SkillBhvr> ();


    }


    void Update ()
    {
      if (hero.hp == 0 && statusBhvr.Status != HeroStatus.dead) {
        //statusBhvr.Status = HeroStatus.dead;
        Die ();
      }
    }

    void OnEnable ()
    { 
      // status behaviour
      if (statusBhvr == null) {
        statusBhvr = GetComponent<HeroStatusBhvr> ();
        if (statusBhvr == null) {
          statusBhvr = gameObject.AddComponent<HeroStatusBhvr> ();
        }
        statusBhvr.statusChangedHandler += OnStatusChanged;
        statusBhvr.pauseChangedHandler += OnPauseChanged;
      }

      // DragonBonesBhvr
      if (dbBhvr == null) {
        // dragonbones behaviour
        dbBhvr = GetComponent<TDB.DragonBonesBhvr> ();
        dbBhvr.GotoAndPlay (statusBhvr.Status.ToString ());
        armature = dbBhvr.armature;
      }

      // armature
      if (armature != null) {
        armature.AddEventListener (DBE.AnimationEvent.MOVEMENT_CHANGE, OnMovementChange);
        armature.AddEventListener (DBE.AnimationEvent.LOOP_COMPLETE, OnAnimationLoopComplete);
        armature.AddEventListener (DBE.AnimationEvent.COMPLETE, OnAnimationComplete);
        armature.AddEventListener (DBE.FrameEvent.ANIMATION_FRAME_EVENT, OnAnimationFrameEvent);
        animationList = armature.Animation.AnimationList;
      }

      // 大招行为
      if (bmBhvr == null) {
        bmBhvr = GetComponent<BigMoveBhvr> ();
        if (bmBhvr == null) {
          bmBhvr = gameObject.AddComponent<BigMoveBhvr> ();
        }
      }

      // 战队状态行为
      if (groupBhvr == null) {
        groupBhvr = GetComponent<GroupBhvr> ();
        if (groupBhvr == null) {
          groupBhvr = gameObject.AddComponent<GroupBhvr> ();
        }
        groupBhvr.statusChangedHandler += OnGroupStatusChanged;
      }

      // 重置
      statusBhvr.IsPause = false;
      statusBhvr.Status = HeroStatus.idle;
      if (dbBhvr != null) {
        dbBhvr.Resume ();
      }

      //

      // 关卡控制
      LevelController.RaisePause += OnPause;
      LevelController.RaiseResume += OnResume;

    }

    void OnDisable ()
    {

      if (armature != null) {
        armature.RemoveEventListener (DBE.AnimationEvent.MOVEMENT_CHANGE, OnMovementChange);
        armature.RemoveEventListener (DBE.AnimationEvent.LOOP_COMPLETE, OnAnimationLoopComplete);
        armature.RemoveEventListener (DBE.AnimationEvent.COMPLETE, OnAnimationComplete);
        armature.RemoveEventListener (DBE.FrameEvent.ANIMATION_FRAME_EVENT, OnAnimationFrameEvent);
      }

      // 关卡控制
      LevelController.RaisePause -= OnPause;
      LevelController.RaiseResume -= OnResume;
    
    }

    #endregion

    #region DragonBones Events

    private void OnMovementChange (Com.Viperstudio.Events.Event e)
    {

      switch (statusBhvr.Status) {
      case HeroStatus.charge:
      case HeroStatus.release:
        // 发大招通知
        if (skill.bigMove && !statusBhvr.IsBigMove) {
          bmBhvr.StartBigMove ();
        }
        break;
      }

    }
    //private void
    private void OnAnimationLoopComplete (Com.Viperstudio.Events.Event e)
    {
      string movementId = armature.Animation.MovementID;

      //
      switch (statusBhvr.Status) {

      case HeroStatus.beat:
        statusBhvr.Status = HeroStatus.idle;
        break;
      case HeroStatus.charge:
        if (skill.loopTimes == 0) {
          // 不做处理，继续循环
        } else if (skill.loopTimes == 1) {
          // 只播放一次
          statusBhvr.Status = HeroStatus.release;
        } else if (skill.loopTimes > 0) {
          // 播放多次
          // 播放索引增加
          skill.loopIndex++;
          if (skill.loopIndex >= skill.loopTimes) {
            // 播放的次数已经足够，转换成释放状态
            statusBhvr.Status = HeroStatus.release;
          }
        }
        break;
      case HeroStatus.release:
        statusBhvr.Status = HeroStatus.idle;
        break;
      }
    }

    private void OnAnimationComplete (Com.Viperstudio.Events.Event e)
    {

      switch (statusBhvr.Status) {
      case HeroStatus.release:
        statusBhvr.Status = HeroStatus.idle;
        break;
      }
    }

    private void OnAnimationFrameEvent (Com.Viperstudio.Events.Event e)
    {

      DBE.FrameEvent evt = e as DBE.FrameEvent;
      if (evt != null) {

        // 如果是投射事件
        if (Config.DEFAULt_CAST_LABEL.Equals (evt.FrameLabel)) {

          // 抛出作用器s
          if (skill != null && skill.effectors != null && skill.effectors.Length > 0) {
            if (skill.loopTimes == 1) {
              // 播放一次
              foreach (Effector effect in skill.effectors) {
                EffectorWrapper w = EffectorWrapper.W (effect, skill, gameObject, target);
                skillBhvr.Cast (w);
              }
            } else if (skill.loopTimes == 0 || skill.loopTimes > 1) {
              // 播放多次
              int remain = skill.loopIndex % skill.effectors.Length;
              EffectorWrapper w = EffectorWrapper.W (skill.effectors [remain], skill, gameObject, target);
              skillBhvr.Cast (w);
            }
          }

          if (statusBhvr.IsBigMove) {
            // 大招结束
            bmBhvr.StopBigMove ();
          }
        }
      }
    }

    #endregion

    #region Tang Callback

    /// <summary>
    /// 暂停回调
    /// </summary>
    /// <param name="pause">If set to <c>true</c> pause.</param>
    private void OnPauseChanged (bool pause)
    {
      if (pause) {
        PauseSelf ();
      } else {
        if (hero.hp == 0) {
          Die ();
        } else {
          ResumeSelf ();
        }
      }
    }

    /// <summary>
    /// 状态回调
    /// </summary>
    /// <param name="status">Status.</param>
    private void OnStatusChanged (HeroStatus status)
    {
      // 动画剪辑
      string clip = null;

      // 如果前一个状态是僵直，首先恢复动作播放
      if (statusBhvr.beforeStatus == HeroStatus.rigid && !statusBhvr.IsPause) {
        dbBhvr.Resume ();
      }

      switch (status) {

      case HeroStatus.charge: // 起手 ----

        // 设置技能下一次可以使用的时间
        skill.nextFire = Time.time + skill.cd;

        // 有则播放，无则转到释放状态

        if (skill.chargeClip != null) {
          if (animationList.Contains (skill.chargeClip)) {
            clip = skill.chargeClip;
          } else {
            clip = null;
          }
        }
        if (clip == null) {
          statusBhvr.Status = HeroStatus.release;
        } else {
          // 播放起手动作
          dbBhvr.GotoAndPlay (clip);
        }

        // 播放起手特效
        if (skill.chargeSpecials != null) {
          skillBhvr.CastChargeSpecial (skill, gameObject, target);
        }

        // 抛出作用器s
        if (skill != null && skill.chargeEffectors != null) {
          foreach (Effector effect in skill.chargeEffectors) {
            EffectorWrapper w = EffectorWrapper.W (effect, skill, gameObject, target);
            skillBhvr.Cast (w);
          }
        }

        break;
      
      case HeroStatus.release: // 释放 ----


        if (skill.releaseClip != null && animationList.Contains (skill.releaseClip)) {
          clip = skill.releaseClip;
        } else {
          clip = Config.DEFAULT_ATTACK_CLIP;
        }
        // 播放施放动作
        armature.Animation.GotoAndPlay (clip, -1, -1, 1);

        // 播放施放特效
        if (skill.releaseSpecials != null) {
          skillBhvr.CastReleaseSpecial (skill, gameObject, target);
        }

        // 抛出作用器s
        if (skill != null && skill.releaseEffectors != null) {
          foreach (Effector effect in skill.releaseEffectors) {
            EffectorWrapper w = EffectorWrapper.W (effect, skill, gameObject, target);
            skillBhvr.Cast (w);
          }
        }

        break;

      case HeroStatus.dead: // 死亡 ----

        // 发出死亡通知
        if (RaiseDead != null) {
          RaiseDead (this, EventArgs.Empty);
        }

        //dbBhvr.Stop ();
        armature.Animation.GotoAndPlay (status.ToString (), -1, -1, 1);
        FadeOut ();
        break;

      case HeroStatus.vertigo: // 晕掉 ----
        dbBhvr.GotoAndPlay (HeroStatus.vertigo.ToString ());
        break;

      case HeroStatus.rigid: // 僵直 ----
        dbBhvr.Pause ();
        break;

      default: // 其他 ----

        //dbBhvr.Stop ();
        dbBhvr.GotoAndPlay (status.ToString ());
        break;
      }
    }

    /// <summary>
    /// 战队状态回调
    /// </summary>
    /// <param name="status">Status.</param>
    private void OnGroupStatusChanged (GroupStatus status)
    {

      switch (status) {
      case GroupStatus.relax:
        break;
      case GroupStatus.walk:
        break;
      case GroupStatus.embattle:
        break;
      case GroupStatus.battle:
        break;
      }

    }

    #endregion

    #region LevelController Events

    /// <summary>
    /// 战斗暂停
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    private void OnPause (object sender, EventArgs args)
    {
      Pause ();
    }

    /// <summary>
    /// 战斗恢复
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    private void OnResume (object sender, EventArgs args)
    {
      Resume ();
    }

    #endregion

    #region Private Methods

    private void FadeOut ()
    {

      // 死亡动画播放完毕
      // 淡出
      FadeOut fadeout = GetComponent<FadeOut> ();
      if (fadeout == null) {
        fadeout = gameObject.AddComponent<FadeOut> ();
      }
      fadeout.Play ();
    }

    /// <summary>
    /// 暂停自己
    /// </summary>
    private void PauseSelf ()
    {

      // 暂停动画
      if (dbBhvr != null) {
        dbBhvr.Pause ();
      }

      // 暂停行走

      if (agent.enabled) {
        agent.enabled = false;
      }
    }

    /// <summary>
    /// 恢复自己
    /// </summary>
    private void ResumeSelf ()
    {

      // 恢复动画
      if (dbBhvr != null) {
        dbBhvr.Resume ();
      }
      // 恢复行走

      if (!agent.enabled) {
        agent.enabled = true;
      }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// 攻击指定目标
    /// </summary>
    /// <param name="target">Target.</param>
    public void Attack (GameObject target, Skill skill)
    {

      if (statusBhvr.Status == HeroStatus.running) {
        agent.ResetPath ();
      }

      if (statusBhvr.Status == HeroStatus.idle || statusBhvr.Status == HeroStatus.running
          ) {

        skill.Reset ();
        this.target = target;
        this.skill = skill;

        statusBhvr.Status = HeroStatus.charge;

        if (skill.bigMove) {
          hero.mp = 0;
        }
      }

    }

    /// <summary>
    /// 使用技能攻击，攻击目标由技能决定
    /// </summary>
    /// <param name="skill">Skill.</param>
    public void Attack (Skill skill)
    {
      Attack (null, skill);
    }

    /// <summary>
    /// 死亡
    /// </summary>
    public void Die ()
    {

      // 正在走路，停止走路
      if (statusBhvr.Status == HeroStatus.running) {
        agent.ResetPath ();
      }

      statusBhvr.Status = HeroStatus.dead;
    }

    /// <summary>
    /// 被击打
    /// </summary>
    public void BeBeat ()
    {
      // 下面的行为会被打断
      switch (statusBhvr.Status) {

      case HeroStatus.charge:
      case HeroStatus.release:
        // 不是在施放大招，并且技能可以被打断
        if (!statusBhvr.IsBigMove && skill.breakable) {
          statusBhvr.Status = HeroStatus.beat;
        }
        break;
      case HeroStatus.running:
        agent.ResetPath ();
        statusBhvr.Status = HeroStatus.beat;
        break;
      default:
        statusBhvr.Status = HeroStatus.beat;
        break;
      }
    }

    /// <summary>
    /// 被打晕
    /// </summary>
    public void BeStun (float time)
    {

      // 只要不是在放大招都会晕掉
      if (!statusBhvr.IsBigMove && statusBhvr.Status != HeroStatus.vertigo) {

        agent.ResetPath ();
        statusBhvr.Status = HeroStatus.vertigo;

        VertigoBhvr vertigoBhvr = GetComponent<VertigoBhvr> ();
        if (vertigoBhvr == null) {
          vertigoBhvr = gameObject.AddComponent<VertigoBhvr> ();
        }

        if (!vertigoBhvr.enabled) {
          vertigoBhvr.enabled = true;
        }
      }
    }

    /// <summary>
    /// 僵直
    /// </summary>
    public void BeRigid ()
    {

      // 只要不是在放大招都会晕掉
      if (!statusBhvr.IsBigMove) {
        agent.ResetPath ();
        statusBhvr.Status = HeroStatus.rigid;

      }
    }

    /// <summary>
    /// 取消僵直
    /// </summary>
    /*
    public void UnRigid ()
    {
      if (statusBhvr.Status == HeroStatus.rigid && !statusBhvr.IsPause) {
        statusBhvr.Status = HeroStatus.idle;
        dbBhvr.Resume ();
      }
    }*/

    /// <summary>
    /// 庆祝胜利
    /// </summary>
    public void CelebrateVictory ()
    {

      statusBhvr.Status = HeroStatus.victory;

    }

    /// <summary>
    /// 暂停
    /// </summary>
    public void Pause ()
    {
      statusBhvr.IsPause = true;
    }

    /// <summary>
    /// 恢复
    /// </summary>
    public void Resume ()
    {

      statusBhvr.IsPause = false;

    }

    /// <summary>
    /// 找距离最近的目标
    /// </summary>
    /// <returns>The closest target.</returns>
    public GameObject FindClosestTarget ()
    {
      return HeroSelector.FindClosestTarget (this);
    }

    /// <summary>
    /// 使用大招攻击
    /// </summary>
    public void BigMove ()
    {
      // 获取大招
      Skill bs = BigMoveSkill ();

      // 使用大招攻击
      if (bs != null) {

        // 如果被其他大招暂停，恢复活动
        if (LevelController.BigMoveCounter > 0) {
          bmBhvr.BreakLock ();
        }

        // 转换状态为 idle
        statusBhvr.Status = HeroStatus.idle;

        bs.cd = 0; // 设置大招cd为0，马上攻击
        AutoFire autoFire = GetComponent<AutoFire> ();
        if (autoFire == null) {
          autoFire = gameObject.AddComponent<AutoFire> ();
        }
        autoFire.nextSkill = bs; // 将自动攻击的技能设置为大招技能


      }
    }

    public Skill BigMoveSkill ()
    {

      foreach (Skill s in hero.skills.Values) {
        if (s.bigMove) {
          return s;
        }
      }

      return null;
    }

    #endregion
  }
}

