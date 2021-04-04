using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commands : MonoBehaviour
{
	[SerializeField] private FPC _fpc;
	private DeveloperConsole _developerConsole;
	private Vector3 _playerPosition;
	private void Start()
	{
		_developerConsole = GetComponent<DeveloperConsole>();
	}
	public void Message(string message)
	{
		_developerConsole._debugLogText.text += "\n" + message;
		_developerConsole._inputCommand.text = null;

	}

	public void SavePosition()
	{
		_playerPosition = _fpc.transform.position;
		Message("Save current position");
	}

	public void LoadPosition()
	{
		_fpc.transform.position = _playerPosition;
		Message("Load position");
	}

	public void Fly()
	{
		_fpc.isFly = !_fpc.isFly;
		Message("Fly: " + _fpc.isFly);
		
	}
}
