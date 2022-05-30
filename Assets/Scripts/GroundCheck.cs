using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    bool isGrounded = false;

    void Start()
    {

    }

    void Update()
    {
        IsGrounded();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
    public bool IsGrounded()
    {
        return isGrounded;

    }

}
