using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveForward : Trability
{
    public float speed;

    Rigidbody2D rb;

    void Awake()
    {
        base.Setup();
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(transform.right * speed * 100);
    }

    public override void UpdateTrability()
    {
        speed = ho.Speed;
        rb.velocity = Vector2.zero;
        rb.AddForce(transform.right * speed);
    }
}