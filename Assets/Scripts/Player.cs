using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Thorn"))
        {
            Die();
        }
    }

    public void Die()
    {
        throw new NotImplementedException();
    }
}
