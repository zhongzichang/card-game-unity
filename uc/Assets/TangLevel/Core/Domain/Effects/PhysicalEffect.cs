using System;

namespace TangLevel
{
  /*
    * 无参数。物理伤害公式：
    * 物理伤害＝物理攻击＊（1-0.01*护甲／（1+0.01*护甲））
  */

  public class PhysicalEffect : Effect
  {
    public const int TYPE = 1;

    public PhysicalEffect(){
      type = TYPE;
    }

    public override void Arise(){



    }

  }
}

