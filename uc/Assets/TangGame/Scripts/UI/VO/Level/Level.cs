using System.Collections;
using TangGame.Xml;
using TangGame.Net;

namespace TangGame.UI{

	/// 关卡
	public class Level {

		public LevelData data;

    private LevelNet mNet;

    public LevelNet net{
      get{
        if(mNet == null){
          mNet = new LevelNet();//防止获取空对象
        }
        return mNet;
      }
      set{this.mNet = value;}
    }
	}
}