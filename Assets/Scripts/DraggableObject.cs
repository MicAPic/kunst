using UI;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    [Header("Physics")]
    public float dragPower = 1.0f;
    public float dragDamping = 1.75f;
    public float boxPushForce = 1.0f;
    private Vector2 _currentVelocity;
    private Vector2 _velocity;

    private bool _isDragged;
    private Vector3 _offset;
    
    private Rigidbody2D _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
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
        _rigidbody.velocity = _currentVelocity;

        foreach (var box in GameManager.Instance.boxes)
        {
            box.velocity = -_currentVelocity * boxPushForce;
        }

        if (!_isDragged) return;

        var newPosition = GameManager.Instance.mainCamera.ScreenToWorldPoint(Input.mousePosition) + _offset;
        _velocity = (Vector2) (newPosition - transform.position) * dragPower;
    }

    void OnMouseDown()
    {
        // Debug.Log("Mouse Down!");
        _offset = transform.position - GameManager.Instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _isDragged = true;
        InGameUI.Instance.SetCursor(true);
    }

    void OnMouseUp()
    {
        // Debug.Log("Mouse Up!");
        _isDragged = false;
        _velocity = Vector2.zero;
        InGameUI.Instance.SetCursor(false);
    }
}
