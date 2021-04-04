using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector] public Vector2 _touchDist;
    public bool isPressed;
    private Vector2 _pointerOld;
    private int _pointerId;
    private bool _pressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        _pointerId = eventData.pointerId;
        _pointerOld = eventData.position;
    }

    private void FixedUpdate()
    {
        if (isPressed)
        {
            if (_pointerId >= 0 && _pointerId < Input.touches.Length)
            {
                _touchDist = Input.touches[_pointerId].position - _pointerOld;
                _pointerOld = Input.touches[_pointerId].position;
            }
            else
            {
                _touchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - _pointerOld;
                _pointerOld = Input.mousePosition;
            }
        }
        else
        {
            _touchDist = new Vector2();
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }
}
