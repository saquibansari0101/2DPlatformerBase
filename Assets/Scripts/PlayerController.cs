using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D player;

    private float moveSpeed;
    private float jumpForce;
    private float moveHorizontal;
    private float moveVertical;

    private bool isJumping;

    public Quaternion originalRotationValue; // declare this as a Quaternion
    public float rotationResetSpeed = 0.1f;


    // Start is called before the first frame update
    void Start()
    {

        player = gameObject.GetComponent<Rigidbody2D>();

        moveSpeed = 3f;
        jumpForce = 10f;
        isJumping = false;

        originalRotationValue = player.transform.rotation;


    }

    // Update is called once per frame
    void Update()
    {
        if (!isJumping)
        {
          //  player.transform.Rotate(-player.transform.rotation.x, -player.transform.rotation.y, -player.transform.rotation.z, Space.Self);
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, Time.time * rotationResetSpeed);
        }
    }

    void FixedUpdate()
    {
        if (UltimateJoystick.GetHorizontalAxis("movement") < -0.1f || UltimateJoystick.GetHorizontalAxis("movement") > 0.1f || UltimateJoystick.GetVerticalAxis("movement") > 0.1f || UltimateJoystick.GetVerticalAxis("movement") < -0.1f)
        {
            player.AddForce(new Vector2(UltimateJoystick.GetHorizontalAxis("movement") * moveSpeed, UltimateJoystick.GetVerticalAxis("movement") * moveSpeed), ForceMode2D.Impulse);
        }

        if (UltimateJoystick.GetVerticalAxis("movement") > 0.1f && !isJumping)
        {
            player.AddForce(new Vector2(0f, moveVertical * moveSpeed), ForceMode2D.Impulse);
        }

       

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = false;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = true;

        }
    }
}
