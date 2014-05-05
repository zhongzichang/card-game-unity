using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class HeroItemData{

    public string icon;
    public string iconFrame;
    public string level;
    public int stars;

    public bool selected;
    public int camp;

    public bool IsSelected() {
      return selected;
    }

    public bool IsFront(){
      return HERO_TYPE_FRONT == camp;
    }

    public bool IsMiddle(){
      return HERO_TYPE_MIDDLE == camp;
    }

    public bool IsBack(){
      return HERO_TYPE_BACK == camp;
    }

    private const int HERO_TYPE_FRONT = 0;
    private const int HERO_TYPE_MIDDLE = 1;
    private const int HERO_TYPE_BACK = 2;
  }
}