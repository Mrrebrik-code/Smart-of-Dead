using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PullPush : MonoBehaviour
{
	[SerializeField] private GameObject _panel;
	[SerializeField] private TMP_Text _pushText;

	public void Show(string text)
	{
		_panel.gameObject.SetActive(true);
		_pushText.text = text;
		StartCoroutine(HideText());
	}

	IEnumerator HideText()
	{
		yield return new WaitForSeconds(2f);
		_pushText.text = null;
		_panel.gameObject.SetActive(false);
	}
}
