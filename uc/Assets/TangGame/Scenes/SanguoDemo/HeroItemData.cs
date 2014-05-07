using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class HeroItemData{

    public string id;
    public int level; //等级
    public int rank; //品阶 进阶加品阶
    public int stars; //进化-加星
    public int camp;

    public delegate void ToggleChanged (string key);
    public ToggleChanged onToggleChanged;
    public bool toggled;

    public bool IsFront(){
      return HERO_TYPE_FRONT == this.camp;
    }

    public bool IsMiddle(){
      return HERO_TYPE_MIDDLE == this.camp;
    }

    public bool IsBack(){
      return HERO_TYPE_BACK == this.camp;
    }

    public void Toggle(){
      Debug.Log ("Toggle");
      this.toggled = !this.toggled;
      if (onToggleChanged != null) {
        onToggleChanged (this.id); 
      }
    }

    private const int HERO_TYPE_FRONT = 0;
    private const int HERO_TYPE_MIDDLE = 1;
    private const int HERO_TYPE_BACK = 2;
  }
}