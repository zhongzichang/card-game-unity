using UnityEngine;
using System.Collections;

namespace TangGame.UI{
  /// <summary>
  /// 竞技场调整界面中选中英雄项
  /// </summary>
  public class ArenaAdjustHeroSelectedItem : ViewItem {

    public UISprite frame;
    public UISprite icon;
    public UISprite star;
    public UILabel levelLabel;

    public TweenPosition tweenArrive;
    public TweenAlpha tweenCancel;
    private TweenPosition tweenMove;
    
    public ViewItemDelegate cancelCompleted;

    void Awake(){
      tweenCancel.SetOnFinished(CancelOnFinished);
      TweenPosition[] list = GetComponents<TweenPosition>();
      foreach(TweenPosition tween in list){
        if(tween.tweenGroup == 1){
          tweenMove = tween;
          break;
        }
      }
    }

    public override void Start (){
      this.started = true;
      UpdateData();
    }
    
    public override void UpdateData (){
      ArenaHero hero = this.data as ArenaHero;
      this.index = hero.sortOrder;
      
      if(!this.started){return;}
      HeroBase heroBase = hero.heroBase;
      if(heroBase == null){
        Global.LogError(">> ArenaAdjustHeroSelectedItem heroBase is null, id = " + hero.id);
        return;
      }
      
      frame.spriteName = Global.GetHeroIconFrame(heroBase.Net.rank);
      star.width = Global.GetHeroStar(heroBase.Net.star) * 16;
      icon.spriteName = heroBase.Xml.avatar;
      levelLabel.text = heroBase.Net.level.ToString();
    }
    
    public override void UpdateSelected (){
      if(this.selected){
        frame.color = Color.gray;
        icon.color = Color.gray;
      }else{
        frame.color = Color.white;
        icon.color = Color.white;
      }
    }

    public void Cancel(Vector3 curr, Vector3 position){
      
      tweenMove.enabled = false;
      
      tweenArrive.from = curr;
      tweenArrive.to = position;
      tweenArrive.ResetToBeginning();
      tweenArrive.Play();
      
      tweenCancel.from = 1;
      tweenCancel.to = 0;
      tweenCancel.ResetToBeginning();
      tweenCancel.Play();
    }
    
    public void Move(Vector3 position){
      tweenMove.from = this.gameObject.transform.localPosition;
      tweenMove.to = position;
      tweenMove.ResetToBeginning();
      tweenMove.Play();
    }
    
    public void Arrive(Vector3 position){
      tweenArrive.from = this.gameObject.transform.localPosition;
      tweenArrive.to = position;
      tweenArrive.ResetToBeginning();
      tweenArrive.Play();
    }
    
    public void CancelOnFinished(){
      if(this.cancelCompleted != null){
        this.cancelCompleted(this);
      }
    }


  }
}