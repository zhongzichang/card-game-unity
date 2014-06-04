using System;

namespace TangLevel
{
  /*
   * 将前摇时间作为参数，进入伤害运算，时间越长，伤害越高。
{伤害作用器编号，系数}与另一个纯粹用作伤害的作用器配合使用。表示在该作用器产生伤害时额外乘以一个系数。
加成系数＝(前摇时间(毫秒)／1000)＊系数
魔法伤害＝(魔法强度＊技能系数＋升级提升参数＊技能等级)*加成系数*/
  public class ChargeHurtEffect : Effect
  {
    public const int TYPE = 27;

    public override void Arise ()
    {
    }
  }
}

