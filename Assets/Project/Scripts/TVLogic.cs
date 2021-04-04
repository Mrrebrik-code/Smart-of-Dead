using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TVLogic : MonoBehaviour
{
	public bool IsOn;
	private Task _task;
	[SerializeField] private VideoPlayer _videoPlayer;
	[SerializeField] private List <VideoClip> _videoClips;
	[SerializeField] private AudioSource _audioSource;
	private void Start()
	{
		_task = GetComponent<Task>();
		_videoPlayer.isLooping = true;
	}

	public void FixedUpdate()
	{

		if (IsOn == false && TaskHandler.CurrentTask == _task)
		{
			TaskHandler.CurrentTask.IsPerformed = true;
			_videoPlayer.isLooping = false;
			_videoPlayer.clip = _videoClips[1];
			_audioSource.Stop();
			StartCoroutine(OffVideoPlayer());
		}
	}

	IEnumerator OffVideoPlayer()
	{
		yield return new WaitForSeconds(2f);
		_videoPlayer.enabled = false;
	}
}
