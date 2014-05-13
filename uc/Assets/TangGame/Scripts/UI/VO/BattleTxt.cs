using UnityEngine;
using System.Collections;

namespace TangGame
{
  public enum BattleTxtType
  {
	///伤害，就是HP相关的文字类型
    Hurt = 0,
	///能量，每场战斗结束的时候回复的值
	Energy,
	///文字，包含Buff等文字
    Text,
    
  }

  public class BattleTxt
  {
    /// 文字类型
    public BattleTxtType type = BattleTxtType.Hurt;
    /// 显示坐标，为转换后的屏幕坐标
    public Vector3 position;
    /// 用于显示的值，伤害的时候为数字，正负判读为掉血或加血；文本的时候为ID
    public int value;
    /// 是否暴击
    public bool crit = false;
    /// 是否为自己
    public bool self = true;
  }
}