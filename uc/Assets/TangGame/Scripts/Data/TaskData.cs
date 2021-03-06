﻿using TangUtils;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace TangGame.Xml
{

  public class TaskData {

    public int id;
    public string name;
    /// 条件
    public string condition;
    /// 前置任务ID
    public int preId;
    /// 计数器
    public int count;
    /// 战队经验奖励
    public int exp;
    /// 金钱奖励
    public int moeny;
    /// 元宝奖励
    public int ingot;
    /// 体力奖励
    public int vitality;
    /// 物品奖励
    public string award;
    /// 前往打开面板的类型
    public int openPanelType;
    /// 前往打开面板的传递值
    public string openPanelValue;

  }

  [XmlRoot ("root")]
  [XmlLate("task")]
  public class TaskRoot
  {
    [XmlElement ("value")]
    public List<TaskData> items = new List<TaskData> ();
    
    public static void LateProcess (object obj)
    {
      TaskRoot root = obj as TaskRoot;
      foreach (TaskData item in root.items) {
        Config.taskTable[item.id] = item;
      }
    }
  }
}