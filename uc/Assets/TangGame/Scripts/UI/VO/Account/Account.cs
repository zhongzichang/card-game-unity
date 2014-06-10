using System.Collections;

namespace TangGame.UI{
  /// <summary>
  /// 账号信息
  /// </summary>
  public class Account {
    
    private static Account mInstance;
    
    public static Account instance {
      get{
        if(null == mInstance){
          mInstance = new  Account();
        }
        return mInstance; 
      }
    }
    /// 账号ID
    public int id;
    /// 主角色名字
    public string name;
    /// 等级
    public short level;
    /// 金钱
    public int money;
    /// 元宝 
    public int ingot;
    /// 体力
    public int vitality;
    /// 体力上限
    public int vitalityMax;
    /// 战力
    public int battlePoint;
    /// VIP等级
    public int vipLevel;
    /// 剩余购买体力次数
    public int buyVitalityNum;
    /// 剩余购买金钱次数
    public int buyMoneyNum;
    /// 队伍经验
    public int exp;
    /// 队伍经验上限
    public int expMax;
    
    private Account(){
      id = 10001;
      name = "调皮的小黄人";
      level = 23;
      money = 50000;
      ingot = 10000;
      vitality = 200;
      vitalityMax = 210;
      exp = 100;
      expMax = 1500;
    }

  }
}