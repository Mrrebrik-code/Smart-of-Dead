using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Phone : MonoBehaviour
{
	[SerializeField] private string _timeAlarmClock;
	[SerializeField] private string _dataAlarmClock;
	[SerializeField] private string _descriptionAlarmClock;
	private Task _task;
	private AudioSource _audioSource;
	public GameObject _phoneUI;

	[SerializeField] private TMP_Text _time;
	[SerializeField] private TMP_Text _data;
	[SerializeField] private TMP_Text _description;

	[SerializeField] private AudioClip _soundAlarmClock;

	private void Awake()
	{
		_task = GetComponent<Task>();
		_audioSource = GetComponent<AudioSource>();
		_audioSource.clip = _soundAlarmClock;
		_audioSource.Play();
		_time.text = _timeAlarmClock;
		_data.text = _dataAlarmClock;
		_description.text = _descriptionAlarmClock;
	}
	public void AlarmClockOff()
	{
		TaskHandler.CurrentTask.IsPerformed = true;
		_audioSource.Stop();
		GetComponent<Thing>().Type = TypeThing.LOCKED;
	}
}
