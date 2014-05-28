using UnityEngine;
using System.Collections;

namespace TangGame.UI{
  /// <summary>
  /// Battle stage item.
  /// </summary>
  public class BattleStageItem : ViewItem {

    public UISprite background;
    public UISprite icon;
    public ViewItemDelegate onClick;

    private BattleChapterStarItem starItem;//星级显示对象
    private bool isClick = true;

    public override void Start (){
      this.started = true;
      UpdateData();
    }
    
    public override void UpdateData (){
      if(!this.started){return;}
      if(this.data == null){return;}
      Level level = this.data as Level;
      isClick = true;
      if(level.data.repeat == 1){//打过的不显示
        if(level.net.star > 0){
          icon.spriteName = "stagecircle_passed";
          isClick = false;
        }else{
          icon.spriteName = level.data.resources;
        }
      }else{
        icon.spriteName = level.data.resources;
        if(level.net.star > 0){//创建星级
          GameObject go = UIUtils.Duplicate(BattleChaptersPanel.StarItem.gameObject, this.gameObject);
          starItem = go.GetComponent<BattleChapterStarItem>();
          go.transform.localPosition = new Vector3(0, -50, 0);
          starItem.data = level.net.star;
          icon.color = new Color32(255, 255, 255, 255);

          if(background == null){
            go = UIUtils.Duplicate(icon.gameObject, this.gameObject);
            background = go.GetComponent<UISprite>();
            background.transform.localPosition = icon.transform.localPosition;
            background.depth = icon.depth - 1;
            background.spriteName = icon.spriteName + "-current";
            background.MakePixelPerfect();
            TweenAlpha tween = go.AddComponent<TweenAlpha>();
            tween.to = 0;
            tween.style = UITweener.Style.PingPong;
            tween.duration = 1.2f;
          }
        }else{
          icon.color = new Color32(114, 114, 114, 255);
        }
      }

      icon.MakePixelPerfect();
    }

    /// 显示
    public void Show(){
      icon.color = new Color32(255, 255, 255, 255);
      if(isClick){
        BoxCollider bc = icon.gameObject.GetComponent<BoxCollider>();
        if(bc == null){
          icon.gameObject.AddComponent<BoxCollider>();
          icon.ResizeCollider();
        }
        UIEventListener.Get(icon.gameObject).onClick += OnClickHandler;
        UIButtonScale btn = icon.gameObject.AddComponent<UIButtonScale>();
        btn.hover = new Vector3(1, 1, 1);
        btn.pressed = new Vector3(0.95f, 0.95f, 0.95f);
      }
    }

    private void OnClickHandler(GameObject go){
      if(onClick != null){
        onClick(this);
      }
    }
    
    public override void OnDestroy (){
      base.OnDestroy ();
    }

  }
}