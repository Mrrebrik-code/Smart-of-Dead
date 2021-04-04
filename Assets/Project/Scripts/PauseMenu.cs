using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	[SerializeField] private FPC _fpc;
	[SerializeField] private GameObject _FPS;
	[SerializeField] private GameObject _effectImage;
	[SerializeField] private Slider _sensetiveSlider;
	[SerializeField] private Toggle _smoothDumpToggle;
	[SerializeField] private Toggle _fpsToggle;
	[SerializeField] private Toggle _EffectToggle;

	private void Start()
	{
		_sensetiveSlider.value = _fpc._sensetive;
	}

	private void Update()
	{
		_fpc.isSmooth = _smoothDumpToggle.isOn;
		_fpc._sensetive = _sensetiveSlider.value;
		_FPS.SetActive(_fpsToggle.isOn);
		_effectImage.SetActive(_EffectToggle.isOn);
	}

	public void Exit()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
