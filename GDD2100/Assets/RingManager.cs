using UnityEngine;

public class RingManager : MonoBehaviour
{
    [SerializeField] int numberOfRings = 5;
    [SerializeField] GameObject ringPrefab;
    [SerializeField] float minX = -100.0f;
    [SerializeField] float maxX = 100.0f;
    [SerializeField] float minZ = -50.0f;
    [SerializeField] float maxZ = 50.0f;
    [SerializeField] float minY = 10.0f;
    [SerializeField] float maxY = 100.0f;
    [SerializeField] float minScale = 5.0f;
    [SerializeField] float maxScale = 25.0f;


    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0)
        {
            while (transform.childCount < numberOfRings)
            {
                SpawnRing();
            }
        }
    }

    private void SpawnRing()
    {
        Debug.Log("Spawning new ring");
        // Instantiate a new ring at a random position above the ground
        Vector3 position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));

        GameObject ring = Instantiate(ringPrefab, position, Quaternion.identity, transform);
        float scale = Random.Range(minScale, maxScale);
        ring.transform.localScale = new Vector3(scale, scale, scale);

        ring.transform.LookAt(FindFirstObjectByType<FireBall>().transform);
    }
}
