using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class Cell : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
{
	public int Index;
	public bool isFull;
	public Item Item;

	public Image Icon;
	public Text Count;
	private Inventory _inventory;
	[SerializeField] private GameObject _fantomCell;
	private GameObject _currentFantomCell;

	private void Awake()
	{
		_inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
	}
	public void OnBeginDrag(PointerEventData eventData)
	{
		if(Item != null)
		{
		_currentFantomCell = Instantiate(_fantomCell, GameObject.Find("HUD").transform);
		_currentFantomCell.GetComponent<FantomCell>().Icon.sprite = Item.Icon;
		_currentFantomCell.GetComponent<FantomCell>().Count.text = Item.Count.ToString();
		_currentFantomCell.GetComponent<CanvasGroup>().blocksRaycasts = false;
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (Item != null)
		{
			_currentFantomCell.transform.position = Input.mousePosition;
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (Item != null)
		{
			Destroy(_currentFantomCell);
			_currentFantomCell.GetComponent<CanvasGroup>().blocksRaycasts = true;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		UseItem();
	}


	public void UseItem()
	{
		if(Item != null)
		{
			if (Item.isEnd)
			{
				_inventory.DeleteItem(Item);
			}
			if (Item.Type == TypeItem.REMOTE_CONTROLLER)
			{
				Interactive.MaxDistanceRay = 10f;
				StartCoroutine(OnOff());
			}
			//Jammed
			//if (Interactive.Hit.transform.GetComponent<Drawer>().Type == TypeDrawer.JAMMED &&
			//	Item.Type == TypeItem.UNJAMMED)
			//{
			//	Debug.Log("Разклинило");
			//	Interactive.Hit.transform.GetComponent<Drawer>().Type = TypeDrawer.OPENED;
			//	Interactive.Hit.transform.GetComponent<Drawer>().IsLocked = false;
			//}
			//if(Interactive.Hit.transform.GetComponent<Thing>().Type == TypeThing.UNSCREWDRIVER &&
			//	Item.Type == TypeItem.TOOL_SCREWDRIVER)
			//{
			//	Interactive.Hit.transform.GetComponent<Rigidbody>().isKinematic = false;
			//}
		}
	}

		IEnumerator OnOff()
	{
		yield return new WaitForSeconds(0.2f);
		if (Interactive.Hit.transform.GetComponent<TVLogic>())
		{
			Interactive.Hit.transform.GetComponent<TVLogic>().IsOn = false;
			Interactive.MaxDistanceRay = 2.5f;
		}
	}
}
