using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDestroyer : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(DestroyBall(collision.gameObject));
        InterfaceUpdate.Instance.RefreshUI();
    }

    private IEnumerator DestroyBall(GameObject ball)
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(ball);
    }
}
