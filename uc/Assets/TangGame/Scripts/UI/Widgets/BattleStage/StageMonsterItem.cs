using UnityEngine;
using System.Collections;
using TangGame.Xml;

namespace TangGame.UI{
  /// <summary>
  /// 关卡信息界面中的怪物ITEM
  /// </summary>
  public class StageMonsterItem : ViewItem {

    public UISprite icon;
    public UISprite frame;
    public UISprite boss;
    public UISprite star;
    public UIEventListener frameBtn;

    private bool isShowBossIcon = false;

    public override void Start (){
      boss.gameObject.SetActive(isShowBossIcon);
      frameBtn.onPress += FrameBtnHandler;
      this.started = true;
      UpdateData();
    }

    public override void UpdateData (){
      if(!this.started){return;}
      if(this.data == null){return;}
      MonsterData monsterData = this.data as MonsterData;
      frame.spriteName = Global.GetHeroIconFrame(monsterData.upgrade);
      icon.spriteName = monsterData.avatar;
      star.width = 20 * Global.GetHeroStar(monsterData.evolve);
    }

    /// 显示BOSS图标
    public void ShowBossIcon(){
      isShowBossIcon = true;
      boss.gameObject.SetActive(isShowBossIcon);
    }

    public override void OnDestroy (){
      base.OnDestroy ();
      frameBtn.onPress -= FrameBtnHandler;
    }
    
    private void FrameBtnHandler(GameObject go, bool state){
      if(this.data == null){return;}
      if(state){
        MonsterData monsterData = this.data as MonsterData;
        MonsterTips.Show(go.transform.position, (int)(frame.height / 2 * this.transform.localScale.y), monsterData, isShowBossIcon);
      }else{
        MonsterTips.Hiddle();
      }
    }
  }
}