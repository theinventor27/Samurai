using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target; // The target to follow (e.g., the player)
    public float smoothSpeed = 5f; // The speed at which the object follows the target
    public float distance = 5f; // The distance between the camera and the target
    public float yOffset = 2f; // The Y distance from the target

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired position with offsets for distance and Y position
            Vector3 desiredPosition = target.position - (target.forward * distance) + new Vector3(0f, yOffset, 0f);

            // Smoothly move towards the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;

            // Fix the X rotation to keep the camera level
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
        }
    }
}
