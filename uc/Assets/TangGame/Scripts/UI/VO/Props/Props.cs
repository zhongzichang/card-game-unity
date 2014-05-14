using System.Collections;
using TangGame.Xml;
using TangGame.Net;

namespace TangGame.UI{

	/// 道具对象
	public class Props  {
		/// 配置数据对象
		public PropsData data;
    /// 网络数据对象
		private PropsNet mNet;
    /// The count. 道具数量
    public int count;

		public PropsNet net {
			get {
				return mNet;
			}
			set {
				mNet = value;
			}
		}
	}
}