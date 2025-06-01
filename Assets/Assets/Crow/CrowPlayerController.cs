using UnityEngine;

public class CrowPlayerController : MonoBehaviour
{
    public float speed;
    private float Move;

    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move = Input.GetAxis("Horizontal");

        transform.position += new Vector3(Move * speed * Time.deltaTime, 0f, 0f);


    }
}
