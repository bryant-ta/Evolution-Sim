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
        rb.velocity = transform.right;
    }

    private void Update()
    {
        rb.velocity = speed * (rb.velocity.normalized);
    }

    public override void UpdateTrability()
    {
        speed = ho.Speed;
    }
}