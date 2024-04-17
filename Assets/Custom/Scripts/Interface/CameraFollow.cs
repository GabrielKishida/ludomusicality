using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;  // The target GameObject to follow

    public float smoothSpeed = 5f;  // Smoothing factor for camera movement
    public Vector3 offset = new Vector3(0f, 2f, -10f);  // Offset from the target position

    void LateUpdate() {
        if (target == null) {
            Debug.LogWarning("Camera target is not set. Assign a target in the inspector.");
            return;
        }

        Vector3 desiredPosition = target.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(transform.position - offset);
    }
}