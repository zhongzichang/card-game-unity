﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PureMVC.Patterns;
using PureMVC.Interfaces;

namespace TangGame.UI{

  public class EnterGameMediator : Mediator{

    public new const string NAME = "EnterGameMediator";

    private EnterGamePanel panel;
    
    public EnterGameMediator (EnterGamePanel panel) : base (NAME)
    {
      this.panel = panel;
    }
    
    public override IList<string> ListNotificationInterests ()
    {
      return new List<string> (){ NtftNames.TG_PRELOAD_COMPLETED };
    }
    
    public override void HandleNotification (INotification notification)
    {
      switch (notification.Name) {
      case NtftNames.TG_PRELOAD_COMPLETED:
        this.panel.PreloadCompleted();
        break;
      }
    }
  }

  /// 进入游戏界面，包括登陆，和服务器选择
  public class EnterGamePanel : ViewPanel {
    public const string NAME = "EnterGamePanel";

    public GameObject loginGroup;
    public GameObject serverGroup;
    public GameObject serverListGroup;

    //============================================
    public UILabel versionLabel;
    public UIEventListener enterBtn;
    public UILabel enterBtnLabel;
    public UILabel loginIdLabel;
    public UILabel loginNameLabel;
    public UILabel loginChangeLabel;
    public UIEventListener serverChangeBtn;
    //============================================
    public UILabel idLabel;
    public UILabel nameLabel;
    public UILabel statusLabel;
    public UILabel loginLabel;
    public UIEventListener selectedBtn;

    public UILabel allServerLabel;
    public ServerInfoItem serverInfoItem;

    public UIEventListener loginBtn;
    public UILabel loginBtnLabel;
    public UIInput usernameInput;
    public UIInput pwdInput;

    private List<GameObject> items = new List<GameObject>();

    void Awake(){
      Facade.Instance.RegisterMediator(new EnterGameMediator (this));//注册
    }

    void Start(){
      serverInfoItem.gameObject.SetActive(false);
      loginGroup.SetActive(false);
      serverListGroup.SetActive(false);
      serverGroup.SetActive(false);
      loginIdLabel.text = "";
      loginNameLabel.text = "";
      serverChangeBtn.onClick += ServerChangeClickHandler;
      enterBtn.onClick += EnterBtnClickrHandler;
      selectedBtn.onClick += SelectedBtnClickrHandler;

      loginBtn.onClick += LoginBtnClickrHandler;

      StartCoroutine(LoadText(GameCache.instance.serverListUrl, ServerListLoadCompleted));

      CreateServerList();
    }

    void OnDestroy (){
      Facade.Instance.RemoveMediator (WelcomeMediator.NAME);
    }

    private void ServerChangeClickHandler(GameObject go){
      serverListGroup.SetActive(true);
      serverGroup.SetActive(false);
    }

    private void EnterBtnClickrHandler(GameObject go){
      Application.LoadLevel("Home");
    }

    private void LoginBtnClickrHandler(GameObject go){
      string username = usernameInput.value;
      string pwd = pwdInput.value;
      Debug.Log(username + "," + pwd);
    }

    private void SelectedBtnClickrHandler(GameObject go){
      serverListGroup.SetActive(false);
      serverGroup.SetActive(true);
    }

    public void PreloadCompleted(){
      //loginGroup.SetActive(true);
    }

    /// 服务器列表下载完成
    private void ServerListLoadCompleted(string text){
      
    }

    /// www下载
    IEnumerator LoadText(string url, System.Action<string> onComplete){
      WWW www = new WWW(url);
      yield return www;
      if (www.error == null) {
        onComplete(www.text); 
      } else {
        Global.LogError (">>LoadText www.url " + www.url);
        Global.LogError (">>LoadText www.error " + www.error);
      }
    }

    private void CreateServerList(){
      foreach(GameObject go in items){
        GameObject.Destroy(go);
      }
      items.Clear();

      Vector3 tempPosition = serverInfoItem.transform.localPosition;
      for(int i = 0; i < 10; i++){
        GameObject go = UIUtils.Duplicate(serverInfoItem.gameObject, serverInfoItem.transform.parent);
        go.transform.localPosition = tempPosition;
        tempPosition.y -= 50;
        ServerInfoItem item = go.GetComponent<ServerInfoItem>();
        items.Add(go);
      }

    }
  }
}