using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed = 15;

    private void Update()
    {
        transform.Rotate(transform.forward, speed * Time.deltaTime);
    }
}