using UnityEngine;
using System.Collections;
using JsonFx.Json;

namespace TangGame.Net
{
	public class HeroResult
	{
		public HeroNet[] data;
	}

	public class HeroService
	{
		private JsonReader reader = new JsonReader ();
		private JsonWriter writer = new JsonWriter();

		public void getHeroes(System.Action<HeroResult> responseHandler){
			string endpoint = "get_heroes";
			System.Action<string> handler = delegate(string jsonData){
				HeroResult result = reader.Read<HeroResult> (jsonData);
				responseHandler (result);
			};
			Test.RestApi.Instance.HttpGet (endpoint, handler);
		}



	}
}