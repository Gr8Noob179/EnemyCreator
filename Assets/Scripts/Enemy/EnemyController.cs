using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private UnityEvent recieveDamage = new UnityEvent();

    [SerializeField] private float acceleration = 2f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float maxSpeed = 10f;

    private Vector2 desiredVelocity;
    private Rigidbody2D rb;
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().gameObject;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        desiredVelocity = (target.transform.position - transform.position).normalized;

        rb.velocity = desiredVelocity * speed;
        rb.velocity *= acceleration * Time.deltaTime;

        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            recieveDamage?.Invoke();
        }
    }
}
