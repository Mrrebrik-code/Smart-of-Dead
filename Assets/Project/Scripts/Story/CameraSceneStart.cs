using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSceneStart : MonoBehaviour
{
	public GameObject _fpc;
	public bool isStop = false;
	private void Update()
	{
		if (isStop)
		{
			_fpc.SetActive(true);
			gameObject.SetActive(false);
		}
	}
}
