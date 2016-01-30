using UnityEngine;
using System.Collections;

public class CharacterControllerScript : MonoBehaviour {
    public float X_THRESHOLD = 0.15f;
    public float MAX_RUN_SPEED = 3;
    public float MAX_SPEED = 3;
    public float m_fMoveSpeed = 8;
    public float m_fJumpSpeed = 80;
    public bool m_bJumping = false;
    public bool m_bRunning = false;


    private Animator animator;

    private Rigidbody2D rigidBody;
    // Use this for initialization
    void Start ()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetAxis("Horizontal") < -X_THRESHOLD || Input.GetAxis("5thAxis") < -X_THRESHOLD)
        {
            rigidBody.AddForce(new Vector2(-m_fMoveSpeed, 0.0f));

            if (rigidBody.velocity.x <  -getSpeed())
            {
                rigidBody.velocity = new Vector2(-getSpeed(), rigidBody.velocity.y);
            }
        }
        if (Input.GetKey(KeyCode.D) || Input.GetAxis("Horizontal") > X_THRESHOLD || Input.GetAxis("5thAxis") > X_THRESHOLD)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(m_fMoveSpeed, 0.0f));

            if (rigidBody.velocity.x > getSpeed())
            {
                rigidBody.velocity = new Vector2(getSpeed(), rigidBody.velocity.y);
            }
        }
        if (Input.GetKey(KeyCode.CapsLock) || Input.GetAxis("8thAxis") > X_THRESHOLD)
        {
            m_bRunning = true;
        }
        else { m_bRunning = false; }


        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Button0")) && m_bJumping == false)
        {
            m_bJumping = true;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, m_fJumpSpeed));
        }

        animator.SetFloat("MomentumX", rigidBody.velocity.x);
        animator.SetFloat("MomentumY", rigidBody.velocity.y);

        if (Input.GetButton("Button0")) { Debug.Log("STP->button0"); }
        if (Input.GetButton("Button1")) { Debug.Log("STP->button1"); }
        if (Input.GetButton("Button2")) { Debug.Log("STP->button2"); }
        if (Input.GetButton("Button3")) { Debug.Log("STP->button3"); }
        if (Input.GetButton("Button4")) { Debug.Log("STP->button4"); }
        if (Input.GetButton("Button5")) { Debug.Log("STP->button5"); }
        if (Input.GetButton("Button6")) { Debug.Log("STP->button6"); }
        if (Input.GetButton("Button7")) { Debug.Log("STP->button7"); }
        if (Input.GetButton("Button8")) { Debug.Log("STP->button8"); }
        if (Input.GetButton("Button9")) { Debug.Log("STP->button9"); }
        if (Input.GetButton("Button10")) { Debug.Log("STP->button10"); }
        if (Input.GetButton("Button11")) { Debug.Log("STP->button11"); }
        if (Input.GetButton("Button12")) { Debug.Log("STP->button12"); }

        //Debug.Log("STP->3rdAxis" + Input.GetAxis("3rdAxis")); //horizontal right stick
        //Debug.Log("STP->4thAxis" + Input.GetAxis("4thAxis")); //vertical right stick
        //Debug.Log("STP->5thAxis" + Input.GetAxis("5thAxis")); // horizontal dpad
        //Debug.Log("STP->6thAxis" + Input.GetAxis("6thAxis")); //vertical dpad

        //Debug.Log("STP->7thAxis" + Input.GetAxis("7thAxis")); //l trigger
        //Debug.Log("STP->8thAxis" + Input.GetAxis("8thAxis")); //r trigger

    }

    public void GroundCollision()
    {
            m_bJumping = false;
    }

    public void FinishCollision()
    {
        //tell the game manager to go up a level 
        //respawn

        Debug.Log("FinishCollision()");
    }

    public float getSpeed()
    {
        if (m_bRunning == true)
        {
            return MAX_RUN_SPEED;
        }
        else
        {
            return MAX_SPEED;
        }

    }
}
