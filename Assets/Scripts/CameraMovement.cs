using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float smoothTime;
    
    private Transform target;
    private Vector3 offset;
    private Vector3 currentVelocity = Vector3.zero;

    private void Start()
    {
        target = Player.Instance.transform;
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }
}
