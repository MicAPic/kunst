using System;
using DG.Tweening;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    public float dragPower = 1.0f;
    public float dragDamping = 1.75f;
    private bool _isDragged;
    private Tween _tween;
    private Vector3 _offset;
    private Camera _mainCamera;
    private Rigidbody2D _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    // void Start()
    // {
    //     
    // }

    // Update is called once per frame
    void Update()
    {
        if (!_isDragged) return;
        
        var newPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition) + _offset;
        var newVelocity = (Vector2) (newPosition - transform.position) * dragPower;
        DOTween.To(
            () => _rigidbody.velocity, 
            x => _rigidbody.velocity = x,
            newVelocity, 
            dragDamping);
    }

    void OnMouseDown()
    {
        // Debug.Log("Mouse Down!");
        // if (GameManager.Instance.levelIsColliding) return;
        // _offset = GameManager.Instance.level.position - _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _offset = transform.position - _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _isDragged = true;
    }

    void OnMouseUp()
    {
        // Debug.Log("Mouse Up!");
        _isDragged = false;
        DOTween.To(
            () => _rigidbody.velocity, 
            x => _rigidbody.velocity = x, 
            Vector2.zero, 
            dragDamping);
    }

    // void OnCollisionEnter2D(Collision2D col)
    // {
    //     if (col.gameObject.CompareTag("Player"))
    //     {
    //         Debug.Log("Collided!");
    //         _tween.Kill();
    //     }
    // }
    
    // void OnCollisionExit2D(Collision2D col)
    // {
    //     // if (col.gameObject.CompareTag("Player"))
    //     // {
    //     //     GameManager.Instance.levelIsColliding = false;
    //     // }
    // }
}
