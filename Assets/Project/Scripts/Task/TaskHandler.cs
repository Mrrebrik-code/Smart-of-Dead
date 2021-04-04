using System.Collections.Generic;
using UnityEngine;
using TMPro;

class TaskHandler : MonoBehaviour
{
	public static Task CurrentTask;
	[SerializeField] private List<Task> _tasks = default;
	[SerializeField] private TMP_Text _textTask;

	private void Start()
	{
		CurrentTask = _tasks[0];
		_textTask.text = CurrentTask.Description;
		LockedNoActiveTask();
	}
	private void Update()
	{
		CheckedTask();
	}
	public void CheckedTask()
	{
		for (int i = 0; i < _tasks.Count; i++)
		{
			if (CurrentTask.IsPerformed == false)
			{
				break;
			}
			else
			{
				if (_tasks[i].IsPerformed == false)
				{
					ClearTask();
					GiveTask(_tasks[i - 1]);
					LockedNoActiveTask();
				}
			}
		}
	}
	private void LockedNoActiveTask()
	{
		for (int i = 0; i < _tasks.Count; i++)
		{
			if(CurrentTask != _tasks[i])
			{
				if(_tasks[i].GetComponent<Thing>() != null)
				{
					_tasks[i].GetComponent<Thing>().Type = TypeThing.LOCKED;
				}
			}
		}
	}
	private void ClearTask()
	{
		for (int i = 0; i < _tasks.Count; i++)
		{
			if(_tasks[i] == null || _tasks[i].IsPerformed)
			{
				_tasks.RemoveAt(i);
			}
		}
	}
	private void GiveTask(Task task)
	{
		_textTask.text = task.Description;
		CurrentTask = task;
		if(CurrentTask.Type == TypeTask.TAKE)
		{
			CurrentTask.GetComponent<Thing>().Type = TypeThing.TAKED;
		}
	}
}
