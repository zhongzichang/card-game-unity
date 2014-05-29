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

    /// 主角色名字
    public string name;
    // 等级
    public short level;
    // 金钱
    public int money;
    // 元宝 
    public int ingot;
    // 体力
    public int vitality;
    // 战力
    public int battlePoint;
    // VIP等级
    public int vipLevel;
    // 剩余购买体力次数
    public int buyVitalityNum;
    // 剩余购买金钱次数
    public int buyMoneyNum;
    
    
    private Account(){
      
    }

  }
}