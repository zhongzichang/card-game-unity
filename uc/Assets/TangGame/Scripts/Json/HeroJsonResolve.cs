using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using JsonFx.Json;

public class HeroJsonResolve:MonoBehaviour
{
	void Awake ()
	{
		HeroJson[] table =TangUtils.JsonParser.Parse<HeroJson>("hero");
		Debug.LogError(table.Length);
		foreach (HeroJson item in table) {
			Debug.Log (item.name.ToString());
		}
	}


}
public class HeroJson
{
	public string skill_ids { get; set; }

	public string energy_recovery { get; set; }

	public string shot_order { get; set; }

	public string addition_treatment { get; set; }

	public string showView { get; set; }

	public string physical_penetration { get; set; }

	public string portrait { get; set; }

	public string physical_crit { get; set; }

	public string intellect { get; set; }

	public string physical_defense { get; set; }

	public string strength { get; set; }

	public string hpMax { get; set; }

	public string agile { get; set; }

	public string intellect_growth { get; set; }

	public string id { get; set; }

	public string location { get; set; }

	public string hero_info { get; set; }

	public string hp_recovery { get; set; }

	public string agile_growth { get; set; }

	public string avatar { get; set; }

	public string dodge { get; set; }

	public string evolve { get; set; }

	public string attribute_type { get; set; }

	public string hero_tip { get; set; }

	public string attack_damage { get; set; }

	public string equip_id_list { get; set; }

	public string ability_power { get; set; }

	public string magic_crit { get; set; }

	public string bloodsucking_lv { get; set; }

	public string name { get; set; }

	public string soul_rock_id { get; set; }

	public string gender { get; set; }

	public string camp { get; set; }

	public string strength_growth { get; set; }

	public string magic_defense { get; set; }

	public string spell_penetration { get; set; }
}

