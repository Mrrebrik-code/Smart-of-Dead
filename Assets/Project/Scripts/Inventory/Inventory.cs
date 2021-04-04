using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	private bool isOpen;
	public List<Item> _items = new List<Item>();
	public List<Cell> _cells = new List<Cell>();
	[SerializeField] private int _countcells;

	[SerializeField] private GameObject _cell;
	[SerializeField] private GameObject _panel;
	[SerializeField] private RectTransform _overlay;
	[SerializeField] private RectTransform _arrowUpDown;

	private void Start()
	{
		CreateCells();
	}

	private void CreateCells()
	{
		for (int i = 0; i < _countcells; i++)
		{
			_cells.Add(Instantiate(_cell, _panel.transform).GetComponent<Cell>());
			_cells[i].Index = i;

		}
		
	}
	public void AddItem(Item item)
	{
		for (int i = 0; i < _countcells; i++)
		{
			if(_cells[i].isFull == false)
			{
				_items.Add(item);
				_cells[i].Item = item;
				_cells[i].isFull = true;
				break;
			}
		}
		RefreshCells();
	}

	public void DeleteItem(Item item)
	{
		Debug.Log(item.Name);
		for (int i = 0; i < _items.Count; i++)
		{

			if(item == _items[i] && item.isEnd)
			{
				Debug.Log("Эллемнт найден");
				Debug.Log(_items[i].Name);
				_items.RemoveAt(i);
				_cells[i].isFull = false;
			}
			else
			{
				item.isUse = false;
			}
		}
		RefreshCells();
	}

	public void RefreshCells()
	{
		for (int i = 0; i < _countcells; i++)
		{
			if (_cells[i].isFull == true)
			{
				_cells[i].Icon.sprite = _cells[i].Item.Icon;
				_cells[i].Count.text = _cells[i].Item.Count.ToString();
			}
			else
			{
				_cells[i].Icon.sprite = null;
				_cells[i].Count.text = null;
			}
		}
	}

	public void UpDown()
	{
		_arrowUpDown.rotation = _arrowUpDown.rotation * Quaternion.Euler(new Vector3(0, 0, 180));
		if(isOpen)
		{
			_overlay.transform.Translate(new Vector3(0, -214, 0));
			isOpen = false;
		}
		else
		{
			_overlay.transform.Translate(new Vector3(0, 214, 0));
			isOpen = true;
		}
	}
}
