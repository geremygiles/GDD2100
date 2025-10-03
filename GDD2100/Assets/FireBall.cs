using System.Collections;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] GameObject football;
    [SerializeField] GameObject cannon;
    [SerializeField] GameObject cannonBase;
    [SerializeField] GameObject fireDirection;
    [SerializeField] float fireForce = 100.0f;
    [SerializeField] float cooldown = 1.0f;

    [SerializeField] GameObject resetPos;

    Rigidbody ballRb;

    float activeCooldown = 0.0f;

    private void Start()
    {
        Invoke(nameof(ResetRotation), .1f);
    }

    private void FixedUpdate()
    {
        if (activeCooldown > 0)
        {
            activeCooldown -= Time.fixedDeltaTime;
        }

        if (activeCooldown < 0)
        {
            activeCooldown = 0;
        }
    }

    public void Fire()
    {
        if (activeCooldown > 0)
        {
            return;
        }

        Debug.Log("Football launched!");

        Quaternion cannonRotationQuat = cannon.transform.rotation;

        GameObject ball = Instantiate(football, transform.position, cannonRotationQuat);

        // Vector of position difference from spawn point to fireDirection
        Vector3 direction = fireDirection.transform.position - transform.position;

        ballRb = ball.GetComponent<Rigidbody>();
        if (ballRb == null)
        {
            Debug.LogError("Rigidbody component missing from this game object");
        }

        ballRb.AddForce(5 * ballRb.mass * fireForce * direction, ForceMode.Impulse);

        activeCooldown = cooldown;
    }

    public void Turn(float x, float y, float z)
    {
        cannon.transform.Rotate(x,y,z);
    }

    public void ResetRotation()
    {
        cannon.transform.LookAt(resetPos.transform);
    }

    IEnumerator Cooldown( float seconds )
    {
        yield return new WaitForSeconds(seconds);
    }
}
