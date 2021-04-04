using UnityEngine;
using TMPro;

[RequireComponent(typeof(FramePerSecond))]
class FPSDisplay : MonoBehaviour
{
	[SerializeField] private TMP_Text _fpsCounterText;
	private FramePerSecond _fpsCounter;

	private string[] _stringsFrom00To9999 = new string[9999];

	private void Awake()
	{
		_fpsCounter = GetComponent<FramePerSecond>();
		for (int i = 0; i < 9999; i++)
		{
			_stringsFrom00To9999[i] = i.ToString();
		}
		Debug.Log(_stringsFrom00To9999[9998]);
	}

	private void Update()
	{
		_fpsCounterText.text = "FPS: " + _stringsFrom00To9999[(_fpsCounter.AverageFPS)];
	}
}
