using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float turnSpeed = 360f;

    private Vector3 input;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GatherInput();
        Look();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GatherInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        input = input.normalized;
    }

    private void Look()
    {

        if (input != Vector3.zero)
        {
            var relativePosition = (transform.position + input.ToIso()) - transform.position;
            var playerRotation = Quaternion.LookRotation(relativePosition, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, playerRotation, turnSpeed * Time.deltaTime);
        }
    }

    private void Move()
    {
        rb.MovePosition(transform.position + (transform.forward * input.magnitude) * speed * Time.deltaTime);
    }
}
