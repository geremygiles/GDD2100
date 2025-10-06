using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [Tooltip("The initial speed of the projectile.")]
    [SerializeField] private float initialVelocity = 15f;

    [Tooltip("The launch angle in degrees.")]
    [SerializeField] private float launchAngle = 45f;

    [Tooltip("The number of points to use for drawing the line. Higher numbers create a smoother curve.")]
    [SerializeField] private int linePoints = 100;

    [Tooltip("The time interval between each point on the line. Affects the length of the rendered path.")]
    [SerializeField] private float timeStep = 0.1f;

    // Private reference to the LineRenderer component
    private LineRenderer lineRenderer;

    // The gravity affecting the projectile
    private Vector3 gravity;

    void Awake()
    {
        // Get the LineRenderer component attached to this GameObject
        lineRenderer = GetComponent<LineRenderer>();

        // Use the project's physics settings for gravity
        gravity = Physics.gravity;
    }

    void Update()
    {
        // Every frame, recalculate and draw the trajectory to allow for real-time adjustments
        DrawTrajectory();
    }

    /// <summary>
    /// Calculates the projectile's trajectory and sets the points for the LineRenderer.
    /// </summary>
    private void DrawTrajectory()
    {
        // 1. Calculate the initial velocity vector based on angle and initial velocity
        // The launch direction is a combination of the object's forward vector and an upward rotation based on the launch angle.
        Vector3 launchDirection = (transform.forward + transform.up * Mathf.Tan(launchAngle * Mathf.Deg2Rad)).normalized;
        Vector3 velocityVector = launchDirection * initialVelocity;

        // 2. Configure the LineRenderer
        // Set the number of points the line will have.
        lineRenderer.positionCount = linePoints;

        // Create an array to hold the points of the trajectory.
        Vector3[] points = new Vector3[linePoints];

        // 3. Calculate each point along the trajectory
        for (int i = 0; i < linePoints; i++)
        {
            // Calculate the time for the current point
            float time = i * timeStep;

            // Apply the physics formula for projectile motion:
            // Position = InitialPosition + (InitialVelocity * time) + (0.5 * gravity * time^2)
            Vector3 point = transform.position + velocityVector * time + 0.5f * gravity * time * time;

            points[i] = point;
        }

        // 4. Set the points to the LineRenderer
        lineRenderer.SetPositions(points);
    }
}
