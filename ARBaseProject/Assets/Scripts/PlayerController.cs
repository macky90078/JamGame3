using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float m_moveSpeed;
    [HideInInspector] public bool m_facingRight = true;

    [SerializeField] Animator m_playerAnim;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += move * m_moveSpeed * Time.deltaTime;
        if (Input.GetAxis("Horizontal") > 0)
        {
            m_playerAnim.SetBool("isWalking", true);

            if (!m_facingRight)
            {
                Flip();
            }
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            m_playerAnim.SetBool("isWalking", true);

            if (m_facingRight)
            {
                Flip();
            }
        }
        else
        {
            m_playerAnim.SetBool("isWalking", false);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Intractable")
        {
            gameObject.transform.SetParent(collision.transform);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Intractable")
        {
            transform.parent = null;
        }
    }

    public void Flip()
    {
        m_facingRight = !m_facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
