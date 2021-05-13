using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TableType
{
	public Sprite sprite;
	public int id;

	public TableType(Sprite sprite, int id)
	{
		this.sprite = sprite;
		this.id = id;
	}
}

public class TablesSprites : MonoBehaviour
{
	public static TablesSprites Instance { get; private set; }

	[SerializeField]
	Sprite[] tableSprites;

	public List<TableType> tables { get; private set; }

	private void Awake()
	{
		if (Instance == null)
			Instance = this;

		tables = new List<TableType>();
		//set sprites and id
		for (int i = 0; i < tableSprites.Length; i++)
			tables.Add(new TableType(tableSprites[i], i));
	}
}
