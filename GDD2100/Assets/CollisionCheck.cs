using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    [SerializeField] float cooldown = 1.0f;
    float activeCooldown = 0.0f;

    // Inside the ring
    private void OnTriggerEnter(Collider other)
    {
        PointManager.Instance.AddPoints(1);
        Debug.Log("Score!");
        Destroy(gameObject.transform.parent.gameObject);
    }

    // Hit the ring
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponentInChildren<SphereCollider>().enabled = false;

        activeCooldown = cooldown;
    }

    private void FixedUpdate()
    {
        if (gameObject.transform.childCount == 0)
        {
            return;
        }

        if (activeCooldown > 0)
            activeCooldown -= Time.fixedDeltaTime;
        if (activeCooldown < 0)
        {
            gameObject.GetComponentInChildren<SphereCollider>().enabled = true;
            activeCooldown = 0;
        }
            
    }
}
