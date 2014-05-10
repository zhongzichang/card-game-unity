/// <summary>
/// Json parser.
/// xbhuang
/// 2014-5-10
/// </summary>
using System;
using UnityEngine;
using System.Collections.Generic;
using JsonFx.Json;

namespace TangUtils
{
	public class JsonParser
	{
		public static T[] Parse<T> (string fileName)
		{
			JsonReader reader = new JsonReader ();
			TextAsset textAsset = Resources.Load ("json/" + fileName, typeof(TextAsset)) as TextAsset;
			if (textAsset == null) {
				return null;
			}
			T[] table = reader.Read<T[]> (textAsset.text);
			return table;
		}
	}
}
