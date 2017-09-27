using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

    [SerializeField] GameObject m_door;
    [SerializeField] GameObject m_doorTwo;

    [SerializeField] GameObject m_destroyObj;
    [SerializeField] float m_openTime;
    [SerializeField] bool m_triggerOne = true;
    [SerializeField] bool m_triggerTwo = false;
    [SerializeField] bool m_triggerThree = false;

    private bool m_moveDoor = false;
    private bool m_doorOneOpen = false;


    // Update is called once per frame
    void Update ()
    {
        if (m_moveDoor == true && m_triggerOne)
        {
            m_door.transform.position += Vector3.up * 4f * Time.deltaTime;
        }
        else if (m_moveDoor == true && m_triggerTwo && !m_doorOneOpen)
        {
            m_door.transform.position += Vector3.up * 4f * Time.deltaTime;
            m_doorTwo.transform.position += Vector3.down * 4f * Time.deltaTime;
        }
        else if (m_moveDoor == true && m_triggerTwo && m_doorOneOpen)
        {
            m_doorTwo.transform.position += Vector3.up * 4f * Time.deltaTime;
            m_door.transform.position += Vector3.down * 4f * Time.deltaTime;
        }
        else if (m_moveDoor == true && m_triggerThree)
        {
            m_doorTwo.transform.position += Vector3.up * 4f * Time.deltaTime;
            m_door.transform.position += Vector3.down * 4f * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Intractable" && m_triggerOne)
        {
            StartCoroutine("OpenDoor", m_openTime);

            if (m_destroyObj != null)
            {
                Destroy(m_destroyObj);
            }
           
        }
        else if (other.gameObject.tag == "Intractable" && m_triggerTwo)
        {
            if (m_doorOneOpen == false)
            {
                StartCoroutine("OpenDoor", m_openTime);
            }
            else if(m_doorOneOpen == true)
            {
                StartCoroutine("OpenDoorTwo", m_openTime);
            }
        }
    }

    IEnumerator OpenDoor(float openTime)
    {
        m_moveDoor = true;
        yield return new WaitForSeconds(openTime);
        m_doorOneOpen = true;
        m_moveDoor = false;
    }

    IEnumerator OpenDoorTwo(float openTime)
    {
        m_moveDoor = true;
        yield return new WaitForSeconds(openTime);
        m_doorOneOpen = false;
        m_moveDoor = false;
    }
}
