using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private float m_moveSpeed;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += move * m_moveSpeed * Time.deltaTime;
        
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
}
