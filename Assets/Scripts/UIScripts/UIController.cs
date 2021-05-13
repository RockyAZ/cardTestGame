using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [SerializeField]
    Text score;
    [SerializeField]
    Button tableSelectBtn;
    [SerializeField]
    GameObject currentCardTypeObj;

    UITableController uiTable;
	private void OnValidate()
	{
        if (score == null)
            Debug.LogError("score bad value");
        if (tableSelectBtn == null)
            Debug.LogError("tableSelect bad value");
        if (this.GetComponent<UITableController>() == null)
            Debug.LogError("need to attach UITableController in this object");
	}

	void Awake()
    {
        if (Instance == null)
            Instance = this;
        tableSelectBtn.onClick.AddListener(TableSelectionBtn);
        uiTable = this.GetComponent<UITableController>();
        currentCardTypeObj.SetActive(false);
    }

    void TableSelectionBtn()
    {
        uiTable.ActivateUI();
    }
    public void SetScore(int amount)
    {
        score.text = amount.ToString();
    }
    public void SetCurrentCardType(Sprite img)
    {
        currentCardTypeObj.SetActive(true);
        currentCardTypeObj.GetComponent<Image>().sprite = img;
    }
    public void HideCurrentCardType()
    {
        currentCardTypeObj.SetActive(false);
    }
}
