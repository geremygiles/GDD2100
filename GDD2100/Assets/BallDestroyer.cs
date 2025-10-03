using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BallDestroyer : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
    }

    private void DestroyBall()
    {
        Destroy(gameObject);
    }
}
