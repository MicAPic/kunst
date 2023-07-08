using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDragLevel : MonoBehaviour
{
    [Header("Physics")]
    public float dragPower = 1.0f;
    public float dragDamping = 1.75f;
    public float boxPushForce = 1.0f;
    private Vector2 _currentVelocity;
    private Vector2 _velocity;

    private bool _isDragged;
    private Vector3 _offset;

    [SerializeField] private Rigidbody2D _levelRigidbody;
    [SerializeField] private Transform _levelTransform;

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    // void Start()
    // {
    //     
    // }

    // Update is called once per frame
    void Update()
    {
        _currentVelocity = Vector2.Lerp(_currentVelocity, _velocity, Time.deltaTime * dragDamping);
        _levelRigidbody.velocity = _currentVelocity;

        foreach (var box in GameManager.Instance.boxes)
        {
            box.velocity = -_currentVelocity * boxPushForce;
        }

        if (!_isDragged) return;

        var newPosition = GameManager.Instance.mainCamera.ScreenToWorldPoint(Input.mousePosition) + _offset;
        _velocity = (Vector2)(newPosition - _levelTransform.position) * dragPower;
    }

    void OnMouseDown()
    {
        // Debug.Log("Mouse Down!");
        _offset = _levelTransform.position - GameManager.Instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _isDragged = true;
    }

    void OnMouseUp()
    {
        // Debug.Log("Mouse Up!");
        _isDragged = false;
        _velocity = Vector2.zero;
    }
}
