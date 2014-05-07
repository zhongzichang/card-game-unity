using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class HeroItemData{

    public string icon;
    public string iconFrame;
    public string level;
    public int stars;
    public int camp;

    public delegate void ToggleChanged (string key);
    public ToggleChanged onToggleChanged;
    public bool toggled;

    public bool IsFront(){
      return HERO_TYPE_FRONT == camp;
    }

    public bool IsMiddle(){
      return HERO_TYPE_MIDDLE == camp;
    }

    public bool IsBack(){
      return HERO_TYPE_BACK == camp;
    }

    public void Toggle(){
      this.toggled = !this.toggled;
      if (onToggleChanged != null) {
        onToggleChanged (icon); 
      }
    }

    private const int HERO_TYPE_FRONT = 0;
    private const int HERO_TYPE_MIDDLE = 1;
    private const int HERO_TYPE_BACK = 2;
  }
}