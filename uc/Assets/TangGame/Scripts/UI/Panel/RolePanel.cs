﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TangGame.UI{
  /// <summary>
  /// 玩家信息面板
  /// </summary>
  public class RolePanel : ViewPanel {
    public const string NAME = "RolePanel";

    public UILabel nameLabel;
    public UILabel levelLabel;
    public UILabel expLabel;
    public UILabel heroLevelLabel;
    public UILabel idLabel;

    public UILabel nameBtnLabel;
    public UILabel avatarBtnLabel;
    public UILabel settingBtnLabel;

    public UIEventListener closeBtn;
    public UIEventListener nameBtn;
    public UIEventListener avatarBtn;
    public UIEventListener settingBtn;

    //===============================
    public GameObject nameGroup;
    public UILabel titleLabel;
    public UILabel changeNameLabel;
    public UILabel okLabel;
    public UILabel cancelLabel;
    public UIEventListener randomBtn;
    public UIEventListener okBtn;
    public UIEventListener cancelBtn;
    public UIEventListener backBtn;
    public TweenScale nameTween;

    private object mParam;
    private bool started;

    void Start(){
      closeBtn.onClick += CloseBtnClickHandler;
      nameBtn.onClick += NameBtnClickHandler;
      avatarBtn.onClick += AvatarBtnClickHandler;
      settingBtn.onClick += SettingBtnClickHandler;

      randomBtn.onClick += RandomBtnClickHandler;
      okBtn.onClick += OkBtnClickHandler;
      cancelBtn.onClick += CancelBtnClickHandler;
      backBtn.onClick += BackBtnClickHandler;


      nameTween.eventReceiver = this.gameObject;
      nameTween.callWhenFinished = "OnFinishedHandler";
      this.nameGroup.SetActive(false);

      this.started = true;
      this.UpdateData();
    }

    public object param{
      get{return this.mParam;}
      set{this.mParam = value; UpdateData();}
    }


    private void UpdateData(){
      if(!this.started){return;}
    }

    private void CloseBtnClickHandler(GameObject go){

    }

    private void NameBtnClickHandler(GameObject go){
      OpenNameGroup();
    }

    private void AvatarBtnClickHandler(GameObject go){
      
    }

    private void SettingBtnClickHandler(GameObject go){
      
    }

    private void RandomBtnClickHandler(GameObject go){
      changeNameLabel.text = NameCache.instance.GetRandomName();
    }

    private void OkBtnClickHandler(GameObject go){
      
    }

    private void CancelBtnClickHandler(GameObject go){
      CloseNameGroup();
    }

    private void BackBtnClickHandler(GameObject go){
      CloseNameGroup();
    }

    private void OnFinishedHandler(UITweener tweener){
      if(tweener.transform.localScale.x < 0.001){
        nameGroup.SetActive(false);
      }
    }

    /// 打开修改名称面板
    private void OpenNameGroup(){
      this.nameGroup.SetActive(true);
      nameTween.PlayForward();
    }

    /// 关闭修改名称面板
    private void CloseNameGroup(){
      nameTween.PlayReverse();
    }


  }
}