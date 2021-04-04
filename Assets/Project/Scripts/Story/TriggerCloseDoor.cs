using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCloseDoor : MonoBehaviour
{
	public bool isOpen = true;
	[SerializeField] private Animator _doorAnimator;
	[SerializeField] private GameObject obj;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && isOpen == true)
		{
			_doorAnimator.SetBool("isTrigger", isOpen);
			_doorAnimator.SetBool("isOpened", false);
			isOpen = false;
			StartCoroutine(Trigger(other.gameObject.GetComponent<FPC>()));
			StartCoroutine(OffCamera(other.gameObject.GetComponent<FPC>()));


		}
	}

	IEnumerator Trigger(FPC other)
	{
		yield return new WaitForSeconds(0.5f);
		_doorAnimator.SetBool("isTrigger", false);
		Destroy(gameObject);
		other.GetComponentInChildren<Camera>().transform.LookAt(obj.transform);
		other.enabled = false;
		
	}
	IEnumerator OffCamera(FPC other)
	{
		yield return new WaitForSeconds(1f);
		other.enabled = true;
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			isOpen = true;
		}
	}


}
