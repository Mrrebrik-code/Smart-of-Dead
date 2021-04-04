using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
	public TypeDrawer Type;
	List<Thing> Things = null;
	[SerializeField] private PullPush _pullPush;
	[SerializeField] private Animator _animator;
	public bool _isOpened = true;
	public bool IsLocked;
	[SerializeField] private string _text;

	private void Start()
	{
		if(Type == TypeDrawer.LOCKED)
		{
			IsLocked = true;
		}
		if(Type == TypeDrawer.JAMMED)
		{
			IsLocked = true;
		}
		if (Type == TypeDrawer.OPENED)
		{
			IsLocked = false;
		}
		if (_isOpened)
		{
			_animator.SetBool("isOpened", !_isOpened);
		}
		else
		{
			_animator.SetBool("isOpened", !_isOpened);
		}
	}
	public void OpenDrawer()
	{
		if(IsLocked == false)
		{
		_animator.SetBool("isOpened", _isOpened);
		_isOpened = !_isOpened;
		}
		else
		{
			_pullPush.Show(_text);
		}
	}
}
