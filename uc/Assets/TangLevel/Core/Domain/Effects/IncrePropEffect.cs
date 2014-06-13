using System;
using System.Collections;

namespace TangLevel
{
  /*
   * {属性类型，类型，值}，
属性类型包括全部基础属性，当前能量，当前血量。
类型表示是按百分比，还是绝对值。
值为具体值，可能由技能表para传入。

    strength = 1,
    intellence = 2,
    max_hp = 3,
    physical_attack = 4,
    magic_power = 5,
    physical_defence = 6,
    magic_defence = 7,
    physical_crit = 8,
    magic_crit = 9,
    hp_recovery = 10,
    mp_recovery = 11,
    routPhysicalDefence = 12,
    routMagicDefence = 13,
    bloodSuckingGrade = 14,
    eva = 15,
    healEffect = 16
*/
  [EffectAttribute (CODE)]
  public class IncrePropEffect : Effect
  {

    public const int CODE = 3;

    public IncrePropEffect () : base (CODE)
    {

    }

    public static void Arise (Effect effect, EffectorWrapper w)
    {

      if (effect.paramList != null && effect.paramList.Count == 3) {

        PropType propType = (PropType)effect.paramList [0];
        bool isPercent = (bool)effect.paramList [1];
        int value = (int)effect.paramList [2];

        switch (propType) {
        case PropType.strength:
          break;
        case PropType.intellence:
          break;
        case PropType.max_hp:
          break;
        case PropType.physical_attack:
          break;
        case PropType.magic_power:
          break;
        case PropType.physical_defence:
          break;
        case PropType.magic_defence:
          break;
        case PropType.physical_crit:
          break;
        case PropType.magic_crit:
          break;
        case PropType.hp_recovery:
          break;
        case PropType.mp_recovery:
          break;
        case PropType.routPhysicalDefence:
          break;
        case PropType.routMagicDefence:
          break;
        case PropType.bloodSuckingGrade:
          break;
        case PropType.eva:
          break;
        case PropType.healEffect:
          break;
        }
      }
    }
  }
}

