using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Thorn"))
        {
            Debug.Log("Thorn detection");
        }
    }
}
