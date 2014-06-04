using System;

namespace TangLevel
{
  /* {对象，轨迹类型，距离，时间}。改变作用对象的位置。
对象表示是技能的施法者，还是目标。
轨迹类型描述了如何从起始点到终点。参见实体轨迹。
距离描述了移动多少个格子。
特别的，正数表示向前方移动，负数表示向后方（注意施法者在左侧队伍还是右侧队伍，这个描述是相对的。）填0表示移动到施法者的目标所在位置。
时间规定了从起始点到终点消耗的毫秒数。*/
  public class ChangePosEffect : Effect
  {
    public const int TYPE = 20;
    public override void Arise ()
    {
    }
  }
}

