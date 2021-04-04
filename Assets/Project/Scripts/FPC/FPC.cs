using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPC : MonoBehaviour
{
    public GameObject _fpcInspection;
    public bool isSmooth;
    public bool isFly;
    public bool isApplayFly;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private TouchField _touchField;

    [SerializeField] private bool _mobileContoller;

    private float _xMove, _zMove;
    private float _xRot, _yRot;
    private float _xRotCurrent, _yRotCurrent;

    private Vector3 _moveDirection;

    [SerializeField] private float _speedMove;

    public float _sensetive;
    [SerializeField] private float _smoothDump;
    private float _currentVelocityY, _currentVelocityX;
    [SerializeField] private float _gravity;

    [SerializeField] private Camera _cameraFPC = default;
    private CharacterController _characterController = default;

    [SerializeField] private Quaternion _startRotationCamera;
    [SerializeField] private Quaternion _startRotationFpcGameobject;

	private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _startRotationCamera = _cameraFPC.transform.rotation;
    }
	private void FixedUpdate()
    {
		GetAxis();
		if (isFly)
		{
            if(isApplayFly)
			{
				_fpcInspection.SetActive(true);
				_fpcInspection.transform.rotation = _cameraFPC.transform.rotation;
				_fpcInspection.transform.position = _cameraFPC.transform.position;
                isApplayFly = !isApplayFly;
			}
		}
		else
		{
			if (_joystick.Horizontal() != 0 || _joystick.Vertical() != 0)
				Move();
			if (_touchField._touchDist.x != 0 || _touchField._touchDist.y != 0)
				Rotation();
		}
	}
    private void GetAxis()
    {
		if (_mobileContoller)
		{
			_xMove = _joystick.Horizontal();
			_zMove = _joystick.Vertical();
		}
		else
		{
			_xMove = Input.GetAxis("Horizontal");
			_zMove = Input.GetAxis("Vertical");

			_xRot += Input.GetAxis("Mouse X");
			_yRot += Input.GetAxis("Mouse Y");
		}
	}
    private void Move()
    {
        if (_characterController.isGrounded)
        {
            _moveDirection = transform.TransformDirection(new Vector3(_xMove, 0f, _zMove));
        }
        _moveDirection.y -= _gravity;
        _characterController.Move(_moveDirection * _speedMove * Time.deltaTime);
    }
    private void Rotation()
    {

        _xRot += _touchField._touchDist.x * _sensetive * Time.fixedDeltaTime;
        _yRot += _touchField._touchDist.y * _sensetive * Time.fixedDeltaTime;

        _yRot = Mathf.Clamp(_yRot, -90f, 90f);

        if (isSmooth)
        {
            _yRotCurrent = Mathf.SmoothDamp(_yRotCurrent, _yRot, ref _currentVelocityY, _smoothDump);
            _xRotCurrent = Mathf.SmoothDamp(_xRotCurrent, _xRot, ref _currentVelocityX, _smoothDump);

            _cameraFPC.transform.rotation =_startRotationCamera * Quaternion.Euler(-_yRotCurrent , _xRotCurrent, 0f);
            _characterController.transform.rotation = _startRotationCamera * Quaternion.Euler(0f, _xRotCurrent, 0f);
            //_startRotationCamera = _cameraFPC.transform.rotation;
        }
		else
		{
            _cameraFPC.transform.localRotation =  _startRotationCamera *  Quaternion.Euler(-_yRot, _xRot, 0f);
            _characterController.transform.localRotation = Quaternion.Euler(0f, _xRot, 0f);
        }
    }

}
