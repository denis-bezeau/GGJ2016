using UnityEngine;
using System.Collections;

public class CharacterControllerScript : MonoBehaviour {

    public float MAX_RUN_SPEED = 3;
    public float MAX_SPEED = 3;
    public float m_fMoveSpeed = 8;
    public float m_fJumpSpeed = 80;
    public bool m_bJumping = false;
    public bool m_bRunning = false;

    private Rigidbody2D rigidBody;
    // Use this for initialization
    void Start ()
    {
        rigidBody = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rigidBody.AddForce(new Vector2(-m_fMoveSpeed, 0.0f));

            if (rigidBody.velocity.x <  -getSpeed())
            {
                rigidBody.velocity = new Vector2(-getSpeed(), rigidBody.velocity.y);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(m_fMoveSpeed, 0.0f));

            if (rigidBody.velocity.x > getSpeed())
            {
                rigidBody.velocity = new Vector2(getSpeed(), rigidBody.velocity.y);
            }
        }
        if (Input.GetKey(KeyCode.CapsLock))
        {
            m_bRunning = true;
        }
        else { m_bRunning = false; }


            if (Input.GetKeyDown(KeyCode.Space) && m_bJumping == false)
        {
            Debug.Log("pressing jump");
            m_bJumping = true;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, m_fJumpSpeed));
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter collision.collider.gameObject.tag=" + collision.collider.gameObject.tag);
        if (collision.collider.gameObject.tag == "Ground")
        {
            m_bJumping = false;
        }
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
