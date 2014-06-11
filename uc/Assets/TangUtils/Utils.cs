﻿using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class Utils
{
	// 24 9  10 11 12
	// 23 8  1  2  13
	// 22 7  0  3  14
	// 21 6  5  4  15
	// 20 19 18 17 16
	public static void GetGridCoordinate (int index)
	{
		index++;//主要是下面按中心为1计算的
		int x = 0;
		int y = 0;
		int d = (int)Mathf.Sqrt (index);
		d = (d % 2) == 0 ? d - 1 : d;//取出奇数
		int r = (int)(d / 2);//半径
		int surplus = index - d * d;//剩余的格子数

		Debug.Log ("d = " + d + " ,r = " + r + " ,surplus = " + surplus);

		if (surplus < 1) {
			x = -r;
			y = r;
		} else {
			int mod = d + 1;//新的模
			int newSurplus = surplus - 1;
			int direction = (int)(newSurplus / mod);
			int g = newSurplus % mod;
			int newR = r + 1;
			if (direction == 0) {
				x = -r + g;
				y = newR;
			} else if (direction == 1) {
				x = newR;
				y = r - g;
			} else if (direction == 2) {
				x = r - g;
				y = -newR;
			} else if (direction == 3) {
				x = -newR;
				y = -r + g;
			}
		}

		Debug.Log (">> x = " + x + ", y = " + y);
	}

	/// 字符串转int，不存在返回0
	public static int StrToInt (string str)
	{
		int result = 0;
		if (str != null) {
			int.TryParse (str, out result);
		}
		return result;
	}

	/// 字符串占用的字节长度
	public static int StrAsciiLen (string str)
	{
		//return str.Length + str.ToCharArray;
		return System.Text.Encoding.Default.GetBytes (str).Length;
	}

	/// 按字节长度接取字符串
	public static string StrAsciiSubstring (string str, int len)
	{
		int aLen = StrAsciiLen (str);
		if (aLen <= len) {
			return str;
		}
		int idx = 0;
		int i = 0;
		while (i < len) {
			i += str [idx] > 255 ? 2 : 1;
			idx += 1;
		}
		if (i > len) {
			idx -= 1;
		}
		return str.Substring (0, idx);
	}

	/// 按字节长度获取索引位置
	public static int StrAsciiIndex (string str, int len)
	{
		int aLen = StrAsciiLen (str);
		if (aLen <= len) {
			return str.Length;
		}
		int idx = 0;
		int i = 0;
		while (i < len) {
			i += str [idx] > 255 ? 2 : 1;
			idx += 1;
		}
		if (i > len) {
			idx -= 1;
		}
		return idx;
	}

	/// <summary>
	/// Convert an hexadecimal value into an integer value
	/// </summary>
	/// <param name="value">The hexadecimal value</param>
	/// <returns>The integer value</returns>
	public static int HexToInt (string value)
	{
		int intValue;
		try {
			intValue = int.Parse (value, System.Globalization.NumberStyles.HexNumber);
		} catch (System.Exception) {
			intValue = 0;
		}
		return intValue;
	}

	/// <summary>
	/// Convert an integer value into an hexadecimal value
	/// </summary>
	/// <param name="value">The integer value</param>
	/// <returns>The hexadecimal value</returns>
	public static string IntToHex (int value)
	{
		string hexValue = value.ToString ("X");
		if (hexValue.Length == 1) {
			hexValue = "0" + hexValue;
		}
		return hexValue;
	}

	/// <summary>
	/// Convert a float value into an hexadecimal value
	/// </summary>
	/// <param name="value">The float value</param>
	/// <returns>The hexadecimal value</returns>
	public static string FloatToHex (float value)
	{
		return IntToHex (int.Parse (value.ToString ()));
	}

	/// <summary>
	/// Convert an hexadecimal color representation RRGGBBAA into a Color value
	/// </summary>
	public static Color HexToColor (string value)
	{
		if (value.Length != 8) {
			return Color.white;
		}

		string rText = value.Substring (0, 2);
		string gText = value.Substring (2, 2);
		string bText = value.Substring (4, 2);
		string aText = value.Substring (6, 2);

		float r = HexToInt (rText) / 255.0f;
		float g = HexToInt (gText) / 255.0f;
		float b = HexToInt (bText) / 255.0f;
		float a = HexToInt (aText) / 255.0f;
		if (r < 0 || g < 0 || b < 0 || a < 0) {
			return Color.white;
		}

		return new Color (r, g, b, a);
	}

	/// <summary>
	/// Convert a Color value into an hexadecimal representation RRGGBBAA
	/// </summary>
	/// <param name="color">The color to convert</param>
	/// <returns>The hexadecimal representation of the color</returns>
	public static string ColorToHex (Color color)
	{
		return FloatToHex (color.r * 255.0f)
		+ FloatToHex (color.g * 255.0f)
		+ FloatToHex (color.b * 255.0f)
		+ FloatToHex (color.a * 255.0f);
	}

	/// <summary>
	/// Splits the string by braces.提取大括号（｛｝） 里面的字符串，形成数组
	/// </summary>
	/// <returns>The string by braces.</returns>
	/// <param name="str">String.</param>
	public static ArrayList SplitStrByBraces (string str)
	{
    str = str.Replace ("[", "");
    string[] strs = str.Split (']');
		ArrayList strList = new ArrayList (strs);
		for (int i = 0; i < strList.Count; i++) {
			if (strList [i] == null || strList [i].ToString () == "" || strList [i].ToString () == " ") {
				strList.RemoveAt (i);
				i--;
			}
		}
		return strList;
	}

	/// <summary>
	/// Splits the string by comma.根据英文 （，）分割字符串
	/// </summary>
	/// <returns>The string by comma.</returns>
	/// <param name="str">String.</param>
	public static string[] SplitStrByComma (string val)
	{
		return val.Split (',');
	}

	public static int[] SplitStrByCommaToInt (string val)
	{
		string[] strs = SplitStrByComma (val);
		int[] ints = new int[strs.Length];
		for (int i = 0; i < strs.Length; i++) {
			ints [i] = int.Parse (strs [i]);
		}
		return ints;
	}

	/// <summary>
	/// Propertieses the size of the info adaptive.
	/// 字体背景自适应
	/// </summary>
	public static void TextBgAdaptiveSize (UILabel text, UISprite background)
	{
		if (background != null && !background.isAnchored) {
			Vector3 mSize = Vector3.zero;
			Transform textTrans = text.transform;
			Vector3 offset = textTrans.localPosition;
			Vector3 textScale = textTrans.localScale;

			// Calculate the dimensions of the printed text
			mSize = text.printedSize;

			// Scale by the transform and adjust by the padding offset
			mSize.x *= textScale.x;
			mSize.y *= textScale.y;

			Vector4 border = background.border;
			mSize.x += border.x + border.z + (offset.x - border.x) * 2f;
			mSize.y += border.y + border.w + (-offset.y - border.y) * 2f;

//			background.width = Mathf.RoundToInt(mSize.x);
			background.height = Mathf.RoundToInt (mSize.y);
		}


	}

	public static bool isXMLStr_Null (string str)
	{
		if (str == "//") {
			return true;
		}
		return false;
	}

	public static float EnchantingVariable (int rank_color, int enchantLevel)
	{
		//TODO only to test;
		return 0.25f * enchantLevel;
	}
}
