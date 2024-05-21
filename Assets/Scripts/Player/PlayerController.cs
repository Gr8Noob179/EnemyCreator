using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float acceleration = 5f;
    [SerializeField, Range(0f, 100f)] private float deceleration = 5f;
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 10f;
    [SerializeField, Range(0f, 100f)] private float stopThreshold = 0.1f;
    [SerializeField] private Vector2 velocity;
    [SerializeField] private Vector2 desiredVelocity;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        desiredVelocity = new Vector2(inputX, inputY).normalized * maxSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 differenceVelocity = desiredVelocity - velocity;

        if (desiredVelocity.magnitude > 0.1f)
        {
            velocity += differenceVelocity.normalized * acceleration * Time.fixedDeltaTime;

            velocity = Vector2.ClampMagnitude(velocity, maxSpeed);
        }
        else
        {
            if (velocity.magnitude > stopThreshold)
            {
                velocity -= velocity.normalized * deceleration * Time.fixedDeltaTime;
            }
            else
            {
                velocity = Vector2.zero;
            }
        }

        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    private void PlayerRotation()
    {

    }
}
