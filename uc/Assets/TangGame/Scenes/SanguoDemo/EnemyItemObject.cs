﻿using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class EnemyItemObject : MonoBehaviour{

    public UISprite icon;
    public UISprite iconFrame;
    public UIGrid stars;
    public UISprite boss;

    private HeroItemData heroData;
    public HeroItemData HeroData{
      get { return heroData; } 
      set { heroData=value; } 
    }

    public void Start(){
      UIEventListener.Get (icon.gameObject).onClick += OnItemClicked;
    }

    public void Refresh(HeroItemData data){
      HeroData = data;
      icon.spriteName = GetIconName(data);
      iconFrame.spriteName = GetIconFrameName(data);
      stars.maxPerLine = data.stars;
      stars.Reposition ();
    }

    private void OnItemClicked(GameObject obj){
      Debug.Log ("OnItemClicked" + heroData.id );
    }

    public void ShowBoss(bool enabled){
      boss.enabled = enabled;
    }

    private string GetIconName(HeroItemData data){
      return data.id;
    }

    private string GetIconFrameName(HeroItemData data){
      return string.Format("hero_icon_frame_{0}", data.rank);
    }
  }
}