using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITableTypeBtn : MonoBehaviour
{
	Button btn;
	Image img;
	int id;
	public void ActivateBtn(TableType table)
	{
		btn = this.GetComponent<Button>();
		if (btn == null)
			Debug.LogError("btn bad value in UITableTypeBtn");
		img = this.GetComponent<Image>();
		if (img == null)
			Debug.LogError("img bad value in UITableTypeBtn");

		btn.onClick.AddListener(Clicked);

		img.sprite = table.sprite;
		id = table.id;
	}

	void Clicked()
	{
		PlayerData.Instance.CurrentTable = id;
		GameController.Instance.GameTable.SetSprite(img.sprite);
	}
}
