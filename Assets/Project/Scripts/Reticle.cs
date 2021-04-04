using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour
{
	[SerializeField] private GameObject _pointReticle;
	[SerializeField] private GameObject _takeReticle;

	public void SwopeReticle(string name)
	{
		//TODO: Переписать на более хорошее решение смены вида прицела
		if (name == "take")
		{
			_pointReticle.SetActive(false);
			_takeReticle.SetActive(true);
		}
		if (name == "point")
		{
			_pointReticle.SetActive(true);
			_takeReticle.SetActive(false);
		}
	}
}
