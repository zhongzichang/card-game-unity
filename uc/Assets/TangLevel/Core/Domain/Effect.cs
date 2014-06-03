﻿using System;

namespace TangLevel
{
  [Serializable]
  public abstract class Effect
  {
    /*
     * 1  物理伤害
2 魔法伤害
3 提升某一项属性
4 治疗
5 弹射
6 施加另一个作用器
7 无法施放技能
8 减少物理伤害
9 减少魔法伤害
10  眩晕
11  魅惑
12  加／减速
13  加／减攻击速度
14  吸血
15  复活
16  放逐
17  对某一类敌人施加作用器，其他人不受影响
18  死神
19  护盾
20  改变位置
21  目标动画编号
22  改变角色贴图颜色
23  改变模型
24  改变命中
25  改变闪避
26  淘汰
27  前摇时间伤害
28  亡灵*/
    public int type = 0;

    public Effect(){

    }

    /// <summary>
    /// 发生
    /// </summary>
    public abstract void Arise();

  }
}

