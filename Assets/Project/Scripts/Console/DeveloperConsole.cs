using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeveloperConsole : MonoBehaviour
{
	private Commands _commands;
	public TMP_InputField _inputCommand;
	public TMP_Text _debugLogText;
	public TMP_Text _version;

	private void Start()
	{
		_commands = GetComponent<Commands>();
		_debugLogText.text += "Smart of Dead " + _version.text;
		_debugLogText.text += "\nDeveloper Console -version 0.0.1";
	}

	public void Send()
	{
		switch (_inputCommand.text.ToLower())
		{
			case "savepos":
				_commands.SavePosition();
				break;
			case "loadpos":
				_commands.LoadPosition();
				break;
			case "info":
				_debugLogText.text += "\n" + "Company: " + Application.companyName;
				_debugLogText.text += "\n" + "Mobile: " + Application.isMobilePlatform;
				_debugLogText.text += "\n" + "Platform: " + Application.platform;
				break;
			case "fly":
				_commands.Fly();
				break;
			default:
				_commands.Message(_inputCommand.text);
				break;
		}
		_inputCommand.text = "";

	}
		
}
