using UnityEngine;
using System.Collections;

namespace TangGame.UI{
  /// <summary>
  /// Battle stage item.
  /// </summary>
  public class BattleStageItem : ViewItem {

    public UISprite icon;
    private BattleChapterStarItem starItem;//星级显示对象

    public override void Start (){
      this.started = true;
      UpdateData();
    }
    
    public override void UpdateData (){
      if(!this.started){return;}
      if(this.data == null){return;}
      Level level = this.data as Level;
      if(level.data.type == 1 && level.net.star > 0){//打过的不显示
        icon.spriteName = "stagecircle_passed";
      }else{
        icon.spriteName = level.data.resources;
        if(level.net.star > 0){//创建星级
          GameObject go = UIUtils.Duplicate(BattleChaptersPanel.StarItem.gameObject, this.gameObject);
          starItem = go.GetComponent<BattleChapterStarItem>();
          starItem.data = level.net.star;
        }
      }
      icon.MakePixelPerfect();
    }

    
    public override void OnDestroy (){
      base.OnDestroy ();
    }

  }
}