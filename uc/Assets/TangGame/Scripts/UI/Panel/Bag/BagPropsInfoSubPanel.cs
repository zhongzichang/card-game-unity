using UnityEngine;
using System.Collections;
using TangGame.UI.Base;

namespace TangGame.UI
{
	public class BagPropsInfoSubPanel : MonoBehaviour
	{
		/// <summary>
		/// The properties info background.
		/// 物品信息文字的背景
		/// </summary>
		public GameObject PropsInfoBg;
		/// <summary>
		/// The properties info label.
		/// 物品的信息
		/// </summary>
		public GameObject PropsInfoLabel;
		/// <summary>
		/// The properties description.
		/// 物品的描述
		/// </summary>
		public GameObject PropsDescription;
		/// <summary>
		/// The property info table.
		/// 描述内容的table容器
		/// </summary>
		public GameObject PropInfoTable;
		/// <summary>
		/// The frames.
		/// 物品边框对象
		/// </summary>
		public GameObject Frames;
		/// <summary>
		/// The properties icon.
		/// 物品图标
		/// </summary>
		public GameObject PropsIcon;
		/// <summary>
		/// The properties count.
		/// 物品的数量
		/// </summary>
		public	GameObject PropsCount;
		/// <summary>
		/// The price label.
		/// 物品的价格
		/// </summary>
		public GameObject PriceLabel;
		/// <summary>
		/// The name of the properties.
		/// 物品的名字
		/// </summary>
		public GameObject PropsName;
		// Use this for initialization
		void Start ()
		{
			this.UpPropsInfo ("物品描述，这是一个非常非常非常废话的描述！你可以忽略它！当然，忽略也是有代价的！！！！");
		}
		// Update is called once per frame
		void Update ()
		{
	
		}

		/// <summary>
		/// Flush the specified data.
		/// 刷新面板数据
		/// </summary>
		/// <param name="data">Data.</param>
		public void Flush (PropsBase data)
		{
			if (this.gameObject.activeSelf == false) {
				this.gameObject.SetActive (true);
				this.GetComponent<TweenPosition> ().Play (true);
			}
		}
		//TODO 更新物品名字
		public void UpPropsName (string name)
		{
			this.PropsName.GetComponent<UILabel> ().text = name;
		}
		//TODO 更新图标
		public void UpPropsIcon (string name)
		{
			this.PropsIcon.GetComponent<UILabel> ().text = name;
		}

		/// <summary>
		/// Ups the properties frames.
		/// 更新阶级
		/// </summary>
		public void UpPropsFrames ()
		{
			//			this.Frames.GetComponent<UISprite>().spriteName = "" TODO  需要根据图片名字修改
		}
		//TODO 更新数量
		public void UpPropsCount (int num)
		{
			UILabel label = this.PropsCount.GetComponent<UILabel> ();
			label.text = string.Format (label.text, num);
		}
		//TODO 更新信息
		public void UpPropsInfoLabel (PropsBase data)
		{
			//		<!-- 属性加成 -->
			//		<!-- 力量 -->
			//		<strength>21</strength>
			if (data.Xml.strength > 0) {

			}
			//		<!-- 智力 -->
			//		<intellect>42</intellect>
			if (data.Xml.intellect > 0) {
			}
			//		<!-- 敏捷 -->
			//		<agile>2</agile>
			if (data.Xml.agile > 0) {
			}
			//		<!-- 生命最大 -->
			//		<hpMax>132</hpMax>
			if (data.Xml.hpMax > 0) {
			}
			//		<!-- 攻击强度 -->
			//		<attack_damage>23</attack_damage>
			if (data.Xml.attack_damage > 0) {
			}
			//		<!-- 法术强度 -->
			//		<spell_power>123</spell_power>
			if (data.Xml.spell_power > 0) {
			}
			//		<!-- 物理防御 -->
			//		<physical_defense>321</physical_defense>
			if (data.Xml.physical_defense > 0) {
			}
			//		<!-- 法术防御 -->
			//		<spell_defense>123</spell_defense>
			if (data.Xml.spell_defense > 0) {
			}
			//		<!-- 物理爆击 -->
			//		<physical_crit>12</physical_crit>
			if (data.Xml.physical_crit > 0) {
			}
			//		<!-- 法术爆击 -->
			//		<spell_crit>21</spell_crit>
			if (data.Xml.spell_cirt > 0) {
			}
			//		<!-- 生命回复 -->
			//		<hp_re>12</hp_re>
			if (data.Xml.hp_re > 0) {
			}
			//		<!-- 能量回复 -->
			//		<energy_re>21</energy_re>
			if (data.Xml.energy_re > 0) {
			}
			//		<!-- 物理穿透 -->
			//		<physical_penetrate>12</physical_penetrate>
			if (data.Xml.physical_penetrate > 0) {
			}
			//		<!-- 法术穿透 -->
			//		<spell_penetrate>21</spell_penetrate>
			if (data.Xml.spell_penetrate > 0) {
			}
			//		<!-- 吸血等级 -->
			//		<bloodsucking_lv>12</bloodsucking_lv>
			if (data.Xml.bloodsucking_lv > 0) {
			}
			//		<!-- 闪避 -->
			//		<dodge>21</dodge>
			if (data.Xml.dodge > 0) {
			}
			//		<!-- 治疗效果 -->
			//		<addition_treatment>21</addition_treatment>
			if (data.Xml.addtition_treatment > 0) {
			}

		}
		//TODO 更新描述
		//TODO 更新出售单价
		/// <summary>
		/// Ups the properties info.
		/// 更新文字内容
		/// </summary>
		/// <param name="text">Text.</param>
		public void UpPropsInfo (string text)
		{
			UILabel label = PropsInfoLabel.GetComponent<UILabel> ();
			label.text = text;
			Utils.TextBgAdaptiveSize (label, PropsInfoBg.GetComponent<UISprite> ());
			PropInfoTable.GetComponent<UITable> ().repositionNow = true;
		}
	}
}