using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Transform gun;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float maxSpeed = 17.5f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float bulletDmg = 5.0f;

    private Vector2 lookingPosition;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gun = FindObjectOfType<PlayerController>().GetComponentInChildren<Transform>();

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookingPosition = (mousePosition - (Vector2)gun.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity += bulletSpeed * lookingPosition;
    }
}
