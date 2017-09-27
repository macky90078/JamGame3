using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float m_moveSpeed;
    [HideInInspector] public bool m_facingRight = true;

    [SerializeField] Animator m_playerAnim;
    private bool m_canMove = true;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (m_canMove)
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
        if(!m_canMove)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Test1", LoadSceneMode.Single);
            }
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Death")
        {
            m_playerAnim.SetTrigger("isDeath");
            m_canMove = false;
        }
        else if (other.gameObject.tag == "Win")
        {
            m_playerAnim.SetTrigger("isWin");
            m_canMove = false;
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
