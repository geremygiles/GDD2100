using System.Collections;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] GameObject football;
    [SerializeField] GameObject cannon;
    [SerializeField] GameObject fireDirection;
    [SerializeField] float fireForce = 100.0f;
    [SerializeField] float cooldown = 1.0f;

    float activeCooldown = 0.0f;

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

        //Vector3 cannonRotationVector = cannon.transform.rotation.x;

        Quaternion cannonRotationQuat = cannon.transform.rotation;
        //Vector3 cannonRotation = new Vector3(cannonRotationQuat.eulerAngles.y, cannonRotationQuat.eulerAngles.z, cannonRotationQuat.eulerAngles.x);

        GameObject ball = Instantiate(football, transform.position, cannonRotationQuat);

        // Vector of position difference from spawn point to fireDirection
        Vector3 direction = fireDirection.transform.position - transform.position;

        ball.GetComponent<Rigidbody>().AddForce(direction * fireForce, ForceMode.Impulse);

        //Debug.Log(cannonRotationQuat.eulerAngles);
       // Debug.Log(cannonRotationVector);

        activeCooldown = cooldown;
    }

    IEnumerator Cooldown( float seconds )
    {
        yield return new WaitForSeconds(seconds);
    }
}
