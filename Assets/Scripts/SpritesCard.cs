using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpritesCard : MonoBehaviour
{
	[SerializeField]
	Sprite chrestSprite;
	[SerializeField]
	Sprite bubnaSprite;
	[SerializeField]
	Sprite chirvaSprite;
	[SerializeField]
	Sprite pikaSprite;

	public Sprite ChrestSprite { get => chrestSprite; }
	public Sprite BubnaSprite { get => bubnaSprite; }
	public Sprite ChirvaSprite { get => chirvaSprite; }
	public Sprite PikaSprite { get => pikaSprite; }

	private void OnValidate()
	{
		if (chrestSprite == null)
			Debug.LogError("sprite bad value");
		if (bubnaSprite == null)
			Debug.LogError("sprite bad value");
		if (chirvaSprite == null)
			Debug.LogError("sprite bad value");
		if (pikaSprite == null)
			Debug.LogError("sprite bad value");
	}
	public Sprite ReturnTypeSprite(CardSuit type)
	{
		if (type == CardSuit.Bubna)
			return bubnaSprite;
		else if(type == CardSuit.Chirva)
			return chirvaSprite;
		else if(type == CardSuit.Chrest)
			return chrestSprite;
		else
			return pikaSprite;
	}
}
