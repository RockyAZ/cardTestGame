using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum CardSuit
{
	Pika,
	Chirva,
	Chrest,
	Bubna
}

//все стандартные способы перебора enum в unity не работают как положено, пришлось использовать это
public static class Globals
{
	public const CardSuit firstSuit = CardSuit.Pika;
	public const CardSuit lastSuit = CardSuit.Bubna;
}

public class CardHandler : MonoBehaviour
{
	public static bool cardsMoving { get; private set; }

	//логика так же заточена под создание карт в хаотичном порядке
	public void CreateCards(int amount, Transform cardParent, GameObject cardPrefab)
	{
		CardSuit currentType = Globals.firstSuit;
		for (int i = 0; i < amount; i++)
		{
			Instantiate(cardPrefab, cardParent).GetComponent<CardObj>().Init(currentType);
			if (currentType == Globals.lastSuit)
				currentType = Globals.firstSuit;
			else
				currentType++;
		}
	}

	public void RotateCardsSuit(Transform parentCard)
	{
		foreach (Transform trans in parentCard)
		{
			trans.GetComponent<CardObj>().RotateToSuit();
		}
	}
	public void RotateCardsBack(Transform parentCard)
	{
		foreach (Transform trans in parentCard)
		{
			trans.GetComponent<CardObj>().RotateToBack();
		}
	}

	public void MoveCardToCenter(CardObj card, TableObj table)
	{
		StartCoroutine(MoveDestroyCard(card.transform, table.CenterPoint));
	}

	public void SortCardsHierarchy(Transform cardParent, TableObj table)
	{
		List<Transform> tmpList = new List<Transform>();
		//sorting for every card type
		CardSuit currentType = Globals.firstSuit;
		//create sorted list
		while (currentType <= Globals.lastSuit)
		{
			foreach (Transform trans in cardParent)
			{
				CardObj tmpCard = trans.GetComponent<CardObj>();
				if (tmpCard.gameObject.activeSelf && tmpCard.CardType == currentType)
					tmpList.Add(trans);
			}
			currentType++;
		}
		//create sorted parent hierarchy 
		foreach (Transform trans in tmpList)
		{
			trans.parent = null;
			trans.parent = cardParent;
		}
	}


	/// <summary>
	/// Resize every card in hand with same space
	/// </summary>
	public void ResizeNormalCardHand(Transform cardParent, TableObj table)
	{
		float minX = table.LeftPoint.x;
		float maxX = table.RightPoint.x;
		float moveLength = Mathf.Abs(maxX - minX) / cardParent.childCount;
		float currentX = minX;
		List<Vector3> newPoses = new List<Vector3>();

		//sorting for every card type
		for (int i = 0; i < cardParent.childCount; i++)
		{
			CardObj tmpCard = cardParent.GetChild(i).GetComponent<CardObj>();

			//set position, change Z axis for normal colliders
			Vector3 newPos = new Vector3(currentX, table.BotPoint.y, table.transform.position.z - i * 0.01f);
			newPoses.Add(newPos);
			currentX += moveLength;

			//set layer order
			tmpCard.SetSortLayer(i + 1);

			//normalize colors and activity
			tmpCard.SetActive(false);
		}
		StartCoroutine(MoveCardsEnum(cardParent, newPoses));
	}

	/// <summary>
	/// Resize cards in hand with more space for special cards
	/// </summary>
	public void ResizeSpecialCardHand(Transform cardParent, TableObj table, CardSuit specialType)
	{
		float minX = table.LeftPoint.x;
		float maxX = table.RightPoint.x;
		float moveLength;
		float moveSpecialLength;
		float currentX = minX;
		List<Vector3> newPoses = new List<Vector3>();

		//count special type for normalizing width
		int specialCounter = 0;
		for (int i = 0; i < cardParent.childCount; i++)
			if (cardParent.GetChild(i).GetComponent<CardObj>().CardType == specialType)
				specialCounter++;
		//formula calculate special and normal width
		moveSpecialLength = (Mathf.Abs(maxX - minX) / cardParent.childCount) * 1.5f;
		moveLength = Mathf.Abs((maxX - minX) - moveSpecialLength * specialCounter) / (cardParent.childCount - specialCounter);

		for (int i = 0; i < cardParent.childCount; i++)
		{
			CardObj tmpCard = cardParent.GetChild(i).GetComponent<CardObj>();

			//set position, change Z axis for normal colliders
			Vector3 newPos = new Vector3(currentX, table.BotPoint.y, table.transform.position.z - i * 0.01f);
			newPoses.Add(newPos);

			//create dist for next card and set color
			float currLen;
			if (tmpCard.CardType == specialType)
			{
				currLen = moveSpecialLength;
				tmpCard.SetActive(true);
			}
			else
			{
				currLen = moveLength;
				tmpCard.SetActive(false);
			}
			currentX += currLen;

			//set layer order
			tmpCard.SetSortLayer(i + 1);
		}
		StartCoroutine(MoveCardsEnum(cardParent, newPoses));
	}

	IEnumerator MoveCardsEnum(Transform cardParent, List<Vector3> poses)
	{
		//wait for other coroutine moving
		while (cardsMoving == true)
			yield return null;
		cardsMoving = true;
		if (cardParent.childCount != poses.Count)
			Debug.LogError("logic error in MoveCardsEnum");

		while (cardParent.childCount > 0 && cardParent.GetChild(0).position != poses[0])
		{
			for (int i = 0; i < cardParent.childCount; i++)
			{
				cardParent.GetChild(i).position =
					Vector3.MoveTowards(cardParent.GetChild(i).position, poses[i],
					GameController.Instance.CardSpeed * Time.deltaTime);
			}
			yield return null;
		}
		for (int i = 0; i < cardParent.childCount; i++)
			cardParent.GetChild(i).position = poses[i];
		cardsMoving = false;
	}

	IEnumerator MoveDestroyCard(Transform card, Vector3 endPos)
	{
		while (Vector3.Distance(card.position, endPos) > 0.01f)
		{
			card.position = Vector3.MoveTowards(card.position, endPos,
					GameController.Instance.CardSpeed * Time.deltaTime * 2);
			yield return null;
		}
		card.position = endPos;
		yield return new WaitForSeconds(2);
		Destroy(card.gameObject);
	}
}
