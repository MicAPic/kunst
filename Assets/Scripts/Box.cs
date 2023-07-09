using System;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float overlapSensitivity;
    public LayerMask collisionMask;
    private Rigidbody2D _rigidbody;
    private List<string> _collisions;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collisions = new List<string>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // Physics2D.OverlapBox(transform.position, transform.localScale * overlapSensitivity, 0, collisionMask)
        // if (Physics2D.BoxCast(transform.position, Vector2.one * overlapSensitivity, 0, Vector2.zero, collisionMask))
        // {
        //     GameManager.Instance.draggableObject.currentVelocity = Vector2.zero;
        // }
        
        for (int i = 0; i < col.contactCount; i++)
        {
            var contact = col.GetContact(i);
            // _collisions.Add(contact.collider.gameObject.tag);
            //
            // if (contact.collider.gameObject.CompareTag("Player"))
            // {
            //     Debug.Log(Vector3.Cross(contact.normal, _rigidbody.velocity));
            // }

            if (!contact.collider.gameObject.CompareTag("Obstacle")) continue;
            
            var distance = contact.normalImpulse - Physics2D.defaultContactOffset * 2.0f;
            _rigidbody.AddForce(contact.normal * distance);
        }

        // if (_collisions.Contains("Player") && _collisions.Contains("Obstacle"))
        // {
        //     Debug.Log("Stop!!!");
        //     GameManager.Instance.draggableObject.currentVelocity = Vector2.zero;
        // }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollisionEnter2D(collision);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _collisions.Clear();
    }
}
