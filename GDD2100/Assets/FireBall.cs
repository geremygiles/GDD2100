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

    Rigidbody ballRb;

    float activeCooldown = 0.0f;

    private void Start()
    {
        
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

        ballRb.AddForce(direction * fireForce * ballRb.mass * 5, ForceMode.Impulse);

        activeCooldown = cooldown;
    }

    public void Turn(float x, float y, float z)
    {
        //cannonBase.transform.Rotate(0, 0, z);

        /*Debug.Log("x: " + cannon.transform.rotation.eulerAngles.x.ToString());
        Debug.Log("y: " + cannon.transform.rotation.eulerAngles.y.ToString());
        Debug.Log("z: " + cannon.transform.rotation.eulerAngles.z.ToString());
        Debug.Log("x: " + cannon.transform.rotation.x.ToString());
        Debug.Log("y: " + cannon.transform.rotation.y.ToString());
        Debug.Log("z: " + cannon.transform.rotation.z.ToString());*/

        Debug.Log("FireDirection position: " + (fireDirection.transform.position - transform.position).ToString());

        if (cannon.transform.rotation.x > 0)
        {
            
        }
        cannon.transform.Rotate(x,y,z);
    }

    IEnumerator Cooldown( float seconds )
    {
        yield return new WaitForSeconds(seconds);
    }
}
