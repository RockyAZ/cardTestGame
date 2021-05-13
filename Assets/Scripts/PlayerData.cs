using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
	private PlayerData() { }

	private static PlayerData instance = null;
	public static PlayerData Instance
	{
		get
		{
			if (instance == null)
				instance = new PlayerData();

			return instance;
		}
	}
	public int CurrentTable
	{
		get => PlayerPrefs.HasKey("CurrentTable") ? PlayerPrefs.GetInt("CurrentTable") : 0;
		set => PlayerPrefs.SetInt("CurrentTable", value);
	}
}
