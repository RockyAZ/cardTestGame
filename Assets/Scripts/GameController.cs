using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public static GameController Instance { get; private set; }

	CardHandler cardHandler;

	SpritesCard spritesCard;
	[SerializeField]
	Transform cardParent;
	[SerializeField]
	GameObject cardPrefab;
	[SerializeField]
	TableObj table;
	[Space]
	[SerializeField]
	int cardAmount;
	[SerializeField]
	float cardSpeed;

	int scoreCounter = 0;
	CardSuit currentCardType;

	public SpritesCard SpritesCard { get => spritesCard; }
	public float CardSpeed { get => cardSpeed; }
	public int CardAmount{ get => cardAmount; }
	public TableObj GameTable { get => table; }

	private void OnValidate()
	{
		if (cardParent == null)
			Debug.LogError("cardParent bad value");
		if (cardPrefab == null)
			Debug.LogError("cardPrefab bad value");
		if (table == null)
			Debug.LogError("table bad value");
		if (cardSpeed <= 0)
			cardSpeed = 1;
		if (cardAmount <= 0)
			cardAmount = 13;
	}

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
		cardHandler = this.GetComponent<CardHandler>();
		if (cardHandler == null)
			Debug.LogError("cardHandler bad value");
		spritesCard = this.GetComponent<SpritesCard>();
		if (SpritesCard == null)
			Debug.LogError("spritesCard bad value");
	}

	public void Start()
	{
		UIController.Instance.SetScore(scoreCounter);
		StartCoroutine(StartGameEnum());
	}

	IEnumerator StartGameEnum()
	{
		cardHandler.CreateCards(13, cardParent, cardPrefab);
		cardHandler.RotateCardsBack(cardParent);
		cardHandler.ResizeNormalCardHand(cardParent, table);
		while (CardHandler.cardsMoving)
			yield return null;
		cardHandler.SortCardsHierarchy(cardParent, table);
		cardHandler.RotateCardsSuit(cardParent);
		DoNextMove();
	}

	void DoNextMove()
	{
		if (cardParent.childCount <= 0)
		{
			StartCoroutine(EndGameEnum());
			return;
		}
		//get random card type
		currentCardType = cardParent.GetChild(Random.Range(0, cardParent.childCount))
			.GetComponent<CardObj>().CardType;
		UIController.Instance.SetCurrentCardType(spritesCard.ReturnTypeSprite(currentCardType));
		cardHandler.ResizeSpecialCardHand(cardParent, table, currentCardType);
	}
	IEnumerator EndGameEnum()
	{
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene("LoadScene");
	}

	public void CardHitted(CardObj card)
	{
		if (card.transform.parent == null)
			return;
		scoreCounter++;
		UIController.Instance.SetScore(scoreCounter);
		card.transform.parent = null;
		cardHandler.MoveCardToCenter(card, table);
		DoNextMove();
	}
}
