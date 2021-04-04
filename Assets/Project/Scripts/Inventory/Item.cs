using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 1)]
public class Item : ScriptableObject
{
	public string Name = "Item";
	public int Count;
	public Sprite Icon;
	public TypeItem Type;
	public bool isEnd;
	public bool isUse;
}
