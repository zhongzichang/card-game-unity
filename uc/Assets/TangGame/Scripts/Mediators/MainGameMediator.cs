/**
 * Main Game Mediator
 * Author: zzc
 *
 * Edit: HuangXiaoBo
 *
 * Date: 2013/11/11
 * Edit: zzc
 */
using UnityEngine;
using System.Collections;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using System.Collections.Generic;
using TS = TangScene;
using TGN = TangGame.Net;
using TE = TangEffect;

namespace TangGame.View
{
  public class MainGameMediator : Mediator
  {
    public static GameObject gameObject;
    public static GameObject uiRoot;
    public new const string NAME = "MAIN_GAME_MEDIATOR";
    //private IFacade facade = PureMVC.Patterns.Facade.Instance;
    private delegate void Handle (INotification notification);

    /// <summary>
    /// 我的消息处理映射方法
    /// </summary>
    private Dictionary<string, Handle> handleTable = new Dictionary<string, Handle> ();

    /// <summary>
    ///   感兴趣的消息
    /// </summary>
    private List<string> interests = new List<string> ();

    /// <summary>
    /// 我的构造方法
    /// </summary>
    /// <param name="gameObject"></param>
    public MainGameMediator (GameObject gameObject) : base(NAME)
    {
      MainGameMediator.gameObject = gameObject;

      handleTable.Add (TangNet.NtftNames.TN_EXCEPTION, HandleNetWorkExcption);
      handleTable.Add (TangNet.NtftNames.TN_CONNECTION_CLOSE, HandleNetWorkClose);

      handleTable.Add (TS.NtftNames.SCENE_LOAD_COMPLETED, HandleSceneLoadCompleted);
      handleTable.Add (TS.NtftNames.LEADING_ACTOR_MOVE, HandleLeadingHeroMove);

      handleTable.Add (TS.NtftNames.SCENE_LOAD_START, HandleSceneLoadStart);
      handleTable.Add (NtftNames.TG_LEADING_ACTOR_READY, HandleLeadingActorReady);
    }
    
    /// <summary>
    /// 我感兴趣的消息
    /// </summary>
    /// <returns></returns>
    public override IList<string> ListNotificationInterests ()
    {
      if (interests.Count == 0) {

        interests.Add (NtftNames.ON_REGISTER_GESTURE_INPUT);
        interests.Add (NtftNames.UN_REGISTER_GESTURE_INPUT);

        interests.Add (TangNet.NtftNames.TN_EXCEPTION);
        interests.Add (TangNet.NtftNames.TN_CONNECTION_CLOSE);

        interests.Add (TS.NtftNames.SCENE_LOAD_COMPLETED);
        interests.Add (TS.NtftNames.LEADING_ACTOR_MOVE);
        interests.Add (TS.NtftNames.TOUCH_ACTOR);
        interests.Add (TS.NtftNames.TOUCH_MAP);
        interests.Add (TS.NtftNames.ACTOR_CREATED);
        interests.Add (TS.NtftNames.SCENE_LOAD_START);
        interests.Add (NtftNames.TG_LEADING_ACTOR_READY);
      }

      return interests;
    }


    /// <summary>
    /// 我注册的命令
    /// </summary>
    public override void OnRegister ()
    {
      this.OnRegisterMediator ();
      this.OnRegisterCommand ();
    }
    
    private void OnRegisterMediator ()
    {
      //facade.RegisterMediator (new MainGamePanelMediator ());

    }

    private void OnRegisterCommand ()
    {

      // the notification from network ---
     
      
      // 脉搏
      //facade.RegisterCommand (PulseCmd.NAME, typeof(PulseCmd));

      // 场景
      //facade.RegisterCommand (ToMapResultCmd.NAME, typeof(ToMapResultCmd));
      
      //获取npc详细信息
      //任务相关

      // 战斗
      // 英雄


      // 背包相关
      
  
      // 角色面板相关
    
      // the notification from self
      // 注册场景手势输入
      SendNotification (NtftNames.ON_REGISTER_GESTURE_INPUT);
      // TangScene
      
      // TangGame


      // TangEffect
      
    }
    
    /// <summary>
    /// 处理通知
    /// </summary>
    /// <param name="notification"></param>
    public override void HandleNotification (INotification notification)
    {
      if (handleTable.ContainsKey (notification.Name))
        handleTable [notification.Name] (notification);
    }
    /// <summary>
    /// 别人告诉我场景加载完毕了，我就去做点事情
    /// </summary>
    /// <param name="notification"></param>
    private void HandleSceneLoadCompleted (INotification notification)
    {

      if (uiRoot == null) {
        uiRoot = GameObject.Instantiate (Resources.Load ("Prefabs/UI Root (2D)")) as GameObject;
        uiRoot.AddComponent<DontDestroyOnLoad> ();
      }

      //SendNotification (NotificationIDs.ID_EnableSceneClick);

      //SendNotification (LeadingActorEnterSceneCmd.NAME);

      TangNet.TN.Send (new TGN.SceneHeroRequest ());
      TangNet.TN.Send (new TGN.SceneMonsterRequest ());
      TangNet.TN.Send (new TGN.SceneNpcRequest ());

      SendNotification (NtftNames.TG_LOADING_END);
    }



    /// <summary>
    /// 别人告诉我网络有异常，我就去做点事情
    /// </summary>
    /// <param name="notification"></param>
    private void HandleNetWorkExcption (INotification notification)
    {
      Debug.LogWarning (notification.Body as System.Exception);
    }

    /// <summary>
    ///   处理网络连接关闭的消息
    /// </summary>
    private void HandleNetWorkClose (INotification notification)
    {
      Debug.LogError ("Sokcet 连接断开~~~");
      //GlobalFunction.SendPopMessage ("Socket 连接断开");
    }

    /// <summary>
    /// 别人告诉我我自己在移动，所以我要去做点事情
    /// </summary>
    /// <param name="notification"></param>
    private void HandleLeadingHeroMove (INotification notification)
    {
      Vector3 posi = (Vector3)notification.Body;
      TangUtils.Point point = TangUtils.GridUtil.Vector3ToPoint (posi);
      TangNet.TN.Send (new TGN.HeroMoveRequest (point));
    }


    private void HandleSceneLoadStart (INotification notification)
    {
//      SendNotification(NtftNames.TG_LOADING_START);
    }

    private void HandleLeadingActorReady (INotification notification)
    {
//      SendNotification(NtftNames.TG_LOADING_END);
    }

  }
}