using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableObj : MonoBehaviour
{
	SpriteRenderer tableSprite;

	[SerializeField]
	Transform topPoint;
	[SerializeField]
	Transform rightPoint;
	[SerializeField]
	Transform botPoint;
	[SerializeField]
	Transform leftPoint;
	[SerializeField]
	Transform centerPoint;

	public Vector3 TopPoint { get => topPoint.position; }
	public Vector3 RightPoint { get => rightPoint.position; }
	public Vector3 BotPoint { get => botPoint.position; }
	public Vector3 LeftPoint { get => leftPoint.position; }
	public Vector3 CenterPoint { get => centerPoint.position; }


	private void OnValidate()
	{
		if (topPoint == null)
			Debug.LogError("topPoint bad value");
		if (rightPoint == null)
			Debug.LogError("rightPoint bad value");
		if (botPoint == null)
			Debug.LogError("botPoint bad value");
		if (leftPoint == null)
			Debug.LogError("leftPoint bad value");
		if (centerPoint == null)
			Debug.LogError("centerPoint bad value");
	}

	private void Awake()
	{
		tableSprite = this.GetComponent<SpriteRenderer>();
		if (tableSprite == null)
			Debug.LogError("tableSprite bad value");
	}
	private void Start()
	{
		SetStartTable();
	}

	public void SetSprite(Sprite sprite)
	{
		tableSprite.sprite = sprite;
	}
	public void SetStartTable()
	{
		GameController.Instance.GameTable.SetSprite(TablesSprites.Instance.tables[PlayerData.Instance.CurrentTable].sprite);
	}
}
