using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITableController : MonoBehaviour
{
    [SerializeField]
    GameObject tableUIObj;
    [SerializeField]
    Transform tableUIParent;
    [SerializeField]
    Button tableSelectCloseBtn;
    [SerializeField]
    GameObject tablePrefab;
    [SerializeField]
    Animator tableAnimator;

    private void OnValidate()
	{
        if (tableUIObj == null)
            Debug.LogError("tableUI bad value");
        if (tableUIParent == null)
            Debug.LogError("tableUIParent bad value");
        if (tableSelectCloseBtn == null)
            Debug.LogError("tableSelectCloseBtn bad value");
        if (tablePrefab == null)
            Debug.LogError("tablePrefab bad value");
        if(tableAnimator == null)
            Debug.LogError("tableAnimator bad value");
    }

    void Start()
    {
        tableSelectCloseBtn.onClick.AddListener(TableSelectionCloseBtn);
        tableUIObj.SetActive(false);
        CreateTablesUI();
    }
    void TableSelectionCloseBtn()
    {
		//tableUIObj.SetActive(false);
		tableUIObj.SetActive(true);
		tableAnimator.Play("OffTableAnimUI");
    }

    public void ActivateUI()
    {
        //tableUIObj.SetActive(true);
        tableAnimator.Play("OnTableAnimUI");
    }

    void CreateTablesUI()
    {
        for (int i = 0; i < TablesSprites.Instance.tables.Count; i++)
        {
            Instantiate(tablePrefab, tableUIParent)
                .GetComponent<UITableTypeBtn>().ActivateBtn(TablesSprites.Instance.tables[i]);
        }
    }

}
