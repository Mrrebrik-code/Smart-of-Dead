using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
	[SerializeField] private TaskHandler _task;
	[SerializeField] private Inventory _inventory;
	[SerializeField] private Camera _cameraFPC = default;
	[SerializeField] private Reticle _reticle;

	[SerializeField] private Transform _offset;
	[SerializeField] private float _grabPower = 10f;
	[SerializeField] private float _throwPower = 1f;

	private bool _isGrabbing = false;
	private Rigidbody _grabbeObj;
	private Quaternion rot;

	private Ray _ray;
	static public RaycastHit Hit;

	public static float MaxDistanceRay = 2.5f;

	[SerializeField] GameObject[] _interactiveButtons;

	private void Update()
	{
		Ray();
		CheckingHit();
		IsGrabbing();
	}

	private void IsGrabbing()
	{
		try
		{
			if (_isGrabbing)
			{
				_grabbeObj.velocity = (_offset.position - (_grabbeObj.transform.position)) * _grabPower;
				_grabbeObj.rotation = rot;
				ShowButton("Grabbing");
			}
		}
		catch
		{
			_isGrabbing = false;
		}
	}
	private void Ray()
	{
		_ray = _cameraFPC.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

		if (Physics.Raycast(_ray, out Hit, MaxDistanceRay))
		{
			Debug.DrawRay(_ray.origin, _ray.direction * MaxDistanceRay, Color.blue);
		}
	}
	private void CheckingHit()
	{
		switch (Hit.transform)
		{
			case null:
				Debug.DrawRay(_ray.origin, _ray.direction * MaxDistanceRay, Color.red);
				ShowButton("Null");
				_reticle.SwopeReticle("point");
				break;
			default:
				if (Hit.transform.GetComponent<Thing>())
				{
					switch (Hit.transform.GetComponent<Thing>().Type)
					{
						case TypeThing.TAKED:
							ShowButton("Take");
							_reticle.SwopeReticle("take");
							break;
						case TypeThing.GRABBING:
							ShowButton("Grabbing");
							_reticle.SwopeReticle("take");
							break;
						case TypeThing.INTERACT:
							ShowButton("Interact");
							_reticle.SwopeReticle("take");
							break;
					}
				}
				else if (Hit.transform.GetComponent<Drawer>())
				{
					ShowButton("Open");
					_reticle.SwopeReticle("take");
				}
				else
				{
					//Ебал я в рот эту систему взаимодейстаия
					//TODO: Нужно сделать все едино, а не разбивкой!
					//MaxDistanceRay = 2.5f;
					ShowButton("None");
					_reticle.SwopeReticle("point");
				}
				break;
		}
	}
	private void ShowButton(string name)
	{
		for (int i = 0; i < _interactiveButtons.Length; i++)
		{
			if(_interactiveButtons[i].GetComponent<InteractiveButton>().NameButton == name)
			{
				_interactiveButtons[i].SetActive(true);
			}
			else
			{
				_interactiveButtons[i].SetActive(false);
			}
		}
	}
	public void Take()
	{
		if (Hit.transform.GetComponent<Task>() == TaskHandler.CurrentTask)
		{
			TaskHandler.CurrentTask.IsPerformed = true;
		}
		_inventory.AddItem(Hit.transform.GetComponent<Thing>().Item);
		Destroy(Hit.transform.gameObject);
	}
	public void Open()
	{
		Hit.transform.GetComponent<Drawer>().OpenDrawer();
	}
	public void Grabbing()
	{
		if (!_isGrabbing)
		{
			_grabbeObj = Hit.transform.GetComponent<Rigidbody>();
			rot = _grabbeObj.transform.rotation;
			_isGrabbing = !_isGrabbing;
		}
		else
		{
			_isGrabbing = false;
			_grabbeObj.velocity = _ray.direction * _throwPower;
			_grabbeObj = null;
		}
	}

	public void Interact()
	{
		if (Hit.transform.GetComponent<Phone>())
		{
			Hit.transform.GetComponent<Phone>()._phoneUI.SetActive(true);
		}
	}
}
