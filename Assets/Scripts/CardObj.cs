using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObj : MonoBehaviour
{
	SpriteRenderer cardSprite;
	SpriteRenderer backSprite;
	CardSuit cardType;
	public bool isActive { get; private set; }

	public CardSuit CardType { get => cardType; }

	public void Init(CardSuit cardType)
	{
		this.cardType = cardType;
		cardSprite = this.GetComponent<SpriteRenderer>();
		if (cardSprite == null)
			Debug.LogError("cardSprite bad value");
		backSprite = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
		if (backSprite == null)
			Debug.LogError("backSprite bad value");

		if (this.cardType == CardSuit.Chrest)
			this.cardSprite.sprite = GameController.Instance.SpritesCard.ChrestSprite;
		if (this.cardType == CardSuit.Bubna)
			this.cardSprite.sprite = GameController.Instance.SpritesCard.BubnaSprite;
		if (this.cardType == CardSuit.Chirva)
			this.cardSprite.sprite = GameController.Instance.SpritesCard.ChirvaSprite;
		if (this.cardType == CardSuit.Pika)
			this.cardSprite.sprite = GameController.Instance.SpritesCard.PikaSprite;
	}

	public void SetSortLayer(int layer)
	{
		cardSprite.sortingOrder = layer;
		backSprite.sortingOrder = layer;
	}

	public void RotateToSuit()
	{
		this.transform.eulerAngles = Vector3.zero;
	}

	public void RotateToBack()
	{
		this.transform.eulerAngles = new Vector3(0, 180, 0);
	}

	/// <summary>
	/// set card color and state
	/// </summary>
	public void SetActive(bool isActive)
	{
		this.isActive = isActive;
		if (isActive)
			cardSprite.color = Color.white;
		else
			cardSprite.color = Color.grey;
	}
}
