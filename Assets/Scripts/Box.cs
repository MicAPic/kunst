using UnityEngine;

public class Box : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        for (int i = 0; i < col.contactCount; i++)
        {
            var contact = col.GetContact(i);
            if (!contact.collider.gameObject.CompareTag("Obstacle")) continue;
            
            var distance = contact.normalImpulse - Physics2D.defaultContactOffset * 2.0f;
            _rigidbody.AddForce(contact.normal * distance);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollisionEnter2D(collision);
    }
}
