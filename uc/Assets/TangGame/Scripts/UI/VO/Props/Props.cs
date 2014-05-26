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

		public PropsNet net {
			get {
				if (mNet == null) {
					return new PropsNet ();
				}
				return mNet;
			}
			set {
				mNet = value;
			}
		}
	}
}