using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class InstepctionFPC : MonoBehaviour
{
	private Camera _cameraFPC;
	public GameObject _obj;
	[SerializeField] private Joystick _joystick;
	[SerializeField] private TouchField _touchField;

	[SerializeField] private float _sensetive;
	private float _xMove, _zMove;
	private float _xRot, _yRot;
	private float _xRotCurrent, _yRotCurrent;

	[SerializeField] private float _smoothDump;
	private float _currentVelocityY, _currentVelocityX;

	private Vector3 _moveDirection;
	[SerializeField] private Quaternion _startRotationCamera;

	private void Awake()
	{
		_cameraFPC = GetComponent<Camera>();
		_startRotationCamera = _cameraFPC.transform.rotation;
	}

	private void FixedUpdate()
	{
		GetAxis();
		if(_joystick.isPressed || _touchField.isPressed)
		{
			Fly();
		}
	}

	private void GetAxis()
	{
		_xMove = _joystick.Horizontal();
		_zMove = _joystick.Vertical();

		_xRot += _touchField._touchDist.x * _sensetive * Time.fixedDeltaTime;
		_yRot += _touchField._touchDist.y * _sensetive * Time.fixedDeltaTime;
	}
	private void Fly()
	{

		_yRotCurrent = Mathf.SmoothDamp(_yRotCurrent, _yRot, ref _currentVelocityY, _smoothDump);
		_xRotCurrent = Mathf.SmoothDamp(_xRotCurrent, _xRot, ref _currentVelocityX, _smoothDump);


		_cameraFPC.transform.rotation = _startRotationCamera * Quaternion.Euler(-_yRotCurrent, _xRotCurrent, 0f);
		_obj.transform.rotation = _startRotationCamera * Quaternion.Euler(-_yRotCurrent, _xRotCurrent, 0f);

		_moveDirection = new Vector3(_xMove, 0f, _zMove);
		_obj.transform.Translate(_moveDirection);
	}
}
