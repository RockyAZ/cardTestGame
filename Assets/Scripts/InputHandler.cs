using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
	void Update()
	{
		Ray ray = new Ray();

		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())//2nd part - check if UI hitted
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		else
			return;
		RaycastHit hit;
		int layer = 1 << LayerMask.NameToLayer("Card");
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
		{
			if (hit.transform.GetComponent<CardObj>().isActive)
				GameController.Instance.CardHitted(hit.transform.GetComponent<CardObj>());
		}
	}
}
