using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] GameObject football;

    public void Fire()
    {
        Debug.Log("Football launched!");

        GameObject ball = Instantiate(football, transform.position, gameObject.transform.rotation);

        ball.GetComponent<Rigidbody>().AddForce(transform.forward * 50, ForceMode.Impulse);
    }
}
