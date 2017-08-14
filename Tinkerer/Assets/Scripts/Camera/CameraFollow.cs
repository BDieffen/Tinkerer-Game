using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float smoothSpeed = 0.125f;
    public float smootheCamSpeed = 0.1f;
    public Vector3 offset;

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        //transform.LookAt(target);

        //Quaternion toRotation = Quaternion.FromToRotation(desiredPosition, smoothedPosition);
        //transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, smootheCamSpeed * Time.time);
    }

}
