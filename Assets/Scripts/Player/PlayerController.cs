using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private Transform crosshair;

    [SerializeField, Range(0f, 15f)] private float acceleration = 5f;
    [SerializeField, Range(0f, 15f)] private float deceleration = 5f;
    [SerializeField, Range(0f, 25f)] private float maxSpeed = 10f;
    [SerializeField, Range(0f, 0.5f)] private float stopThreshold = 0.1f;

    private Vector2 velocity;
    private Vector2 desiredVelocity;
    private Vector3 lookingDirection;

    private float radius = 0.7f;
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
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        lookingDirection = (mousePosition - transform.position).normalized;

        desiredVelocity = new Vector2(inputX, inputY).normalized * maxSpeed;
        if (Input.GetMouseButtonUp(0)) Shoot(0.7f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Mouse position
        #region

        #endregion

        //Player movement
        #region
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
        #endregion

        //Crosshair
        #region
        float desiredAngle = Mathf.Atan2(lookingDirection.y, lookingDirection.x);

        Vector3 dessiredPosition = new Vector3(
                transform.position.x + radius * Mathf.Cos(desiredAngle),
                transform.position.y + radius * Mathf.Sin(desiredAngle), 
                0
            );
        crosshair.position = Vector3.Lerp(crosshair.position, dessiredPosition, 15f * Time.fixedDeltaTime);

        crosshair.rotation = Quaternion.Euler(0, 0, (desiredAngle * Mathf.Rad2Deg));
        #endregion

    }

    private void Shoot(float fireDelay)
    {
        float rotationAngle = Mathf.Atan2(lookingDirection.y, lookingDirection.x);

        GameObject go = Instantiate(bulletPref, crosshair.position, Quaternion.Euler(0f, 0f, rotationAngle * Mathf.Rad2Deg + 90f));

        Destroy(go, 2.5f);
    }

    
}
