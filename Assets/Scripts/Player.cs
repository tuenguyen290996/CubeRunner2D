using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce;

    Rigidbody2D m_rb;

    bool m_isGround;

    GameController m_gc;

    public AudioSource aus;

    public AudioClip jumpSound;
    public AudioClip loseSound;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_gc = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isJumpKeyPressed = Input.GetKeyDown(KeyCode.Space);

        if (isJumpKeyPressed && m_isGround)
        {
            //Vector2.up = (0,1)
            // (0,1) *5 = (0,5)
            m_rb.AddForce(Vector2.up * jumpForce);
            m_isGround = false;

            if (aus && jumpSound)
            {
                aus.PlayOneShot(jumpSound);
            }
        }
    }

    public void jump()
    {
        if (!m_isGround)
        {
            return;
        }
        
          
            m_rb.AddForce(Vector2.up * jumpForce);
            m_isGround = false;

            if (aus && jumpSound)
            {
                aus.PlayOneShot(jumpSound);
            }
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            m_isGround = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Obstacle"))
        {
            if (aus && loseSound && !m_gc.IsGameover())
            {
                aus.PlayOneShot(loseSound);
            }

            m_gc.SetGameoverState(true);

            

            Debug.Log("Player da va cham vs chuong ngai vat, Tro choi ket thuc");
        }
    }
}
