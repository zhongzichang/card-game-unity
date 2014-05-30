using UnityEngine;
using System.Collections;
using TangGame.Xml;

namespace TangGame.UI
{
  public class HeroSelectedItem : ViewItem{

    public UISprite icon;
    public UISprite iconFrame;
    public UILabel level;
    public UIGrid stars;
    public UISprite mp;
    public UISprite hp;
    public GameObject bucket;
    public GameObject tick;
    public UISprite cast;

    public TweenPosition tweenArrive;
    public TweenAlpha tweenCancel;
    private TweenPosition tweenMove;

    public ViewItemDelegate cancelCompleted;

    private HeroItemData mData;
    public HeroItemData data{
      get { return mData; } 
      set { mData=value; this.UpdateData();} 
    }

    private bool showLevel = true;
    public bool ShowLevel {
      get { return showLevel; }
      set { showLevel = value; }
    }

    public string HeroId{
      get { return mData.id; } 
    }

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


    public void UpdateData(){
      if(!this.started){return;}
      if(this.mData == null){return;}

      HeroData heroData = HeroCache.instance.GetHeroData(int.Parse(this.mData.id));
      if(heroData != null){
        icon.spriteName = heroData.avatar;
      }else{
        icon.spriteName = "";
      }
      iconFrame.spriteName = Global.GetHeroIconFrame(data.rank_color);
      if (showLevel) {
        level.text = data.level.ToString ();
      } else {
        level.enabled = false;
      }
      mp.fillAmount = data.mp / data.mpMax;
      hp.fillAmount = data.hp / data.hpMax;
      stars.maxPerLine = data.stars;
      tick.SetActive (false);

      stars.Reposition ();
    }

    public void UpdateMp(int mpValue){
      mp.fillAmount = mpValue *100 / data.mpMax;
      mp.gameObject.SetActive (true);
      if (data.mpMax == mpValue) {
        cast.gameObject.SetActive (true);
      } else {
        cast.gameObject.SetActive (false);
      }
    }

    public void ToggleActive(){
      gameObject.SetActive (!gameObject.activeSelf);
    }

    public void ToggleTick(){
      bool toggled = tick.activeSelf;
      if (toggled) {
        icon.color = ColorUtil.WHITE;
        iconFrame.color = ColorUtil.WHITE;
      } else {
        icon.color = ColorUtil.GRAY;
        iconFrame.color = ColorUtil.GRAY;
      }
      tick.SetActive (!toggled);
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