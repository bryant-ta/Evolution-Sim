using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveForward : MonoBehaviour
{
    public float speed;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb.velocity.magnitude < speed)
        {
            rb.AddForce(transform.right * speed * 100);
        }
    }
}