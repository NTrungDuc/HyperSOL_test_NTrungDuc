using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletShip : MonoBehaviour
{
    public bool isPlayer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
    }
}
