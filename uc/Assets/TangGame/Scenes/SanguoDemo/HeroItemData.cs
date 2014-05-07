using UnityEngine;
using System.Collections;

namespace TangGame.UI
{
  public class HeroItemData{

    public string id;
    /// <summary>
    /// 出场的序列
    /// </summary>
    public int order; 
    /// <summary>
    /// 等级
    /// </summary>
    public int level;
    /// <summary>
    /// 品阶 进阶加品阶，白，绿，蓝，紫
    /// </summary>
    public int rank; 
    /// <summary>
    /// 进化-加星，1-5
    /// </summary>
    public int stars; 
    /// <summary>
    /// 前排，中排，后排
    /// </summary>
    public int lineType;

    public delegate void ToggleChanged (string key);
    public ToggleChanged onToggleChanged;
    public bool toggled;

    public bool IsFront(){
      return 0 == this.lineType;
    }

    public bool IsMiddle(){
      return 1 == this.lineType;
    }

    public bool IsBack(){
      return 2 == this.lineType;
    }

    public void Toggle(){
      this.toggled = !this.toggled;
      if (onToggleChanged != null) {
        onToggleChanged (this.id); 
      }
    }
  }
}