using System.Collections;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] GameObject football;
    [SerializeField] GameObject cannon;
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

        Quaternion cannonRotationQuat = cannon.transform.rotation;
        Vector3 cannonRotation = new Vector3(cannonRotationQuat.eulerAngles.y, cannonRotationQuat.eulerAngles.z, cannonRotationQuat.eulerAngles.x);

        GameObject ball = Instantiate(football, transform.position, cannonRotationQuat);

        ball.GetComponent<Rigidbody>().AddForce(Vector3.forward * 50, ForceMode.Impulse);

        Debug.Log(cannonRotationQuat.eulerAngles);
        Debug.Log(cannonRotation);

        Debug.Log(Vector3.forward);

        activeCooldown = cooldown;
    }

    IEnumerator Cooldown( float seconds )
    {
        yield return new WaitForSeconds(seconds);
    }
}
