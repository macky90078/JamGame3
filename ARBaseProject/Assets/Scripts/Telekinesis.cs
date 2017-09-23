using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telekinesis : MonoBehaviour {

    [SerializeField] private bool m_active = false;
    [SerializeField] GameObject m_mainCamera;

    [SerializeField] float m_finalLiftVelocity = 23.0f;
    [SerializeField] float m_timeToSetLiftVelocity = 1.0f;

    ObjectSelect objectSelectScript;

    private float m_liftForce;

    private Rigidbody2D m_rb;

    private void Awake()
    {
        objectSelectScript = m_mainCamera.GetComponent<ObjectSelect>();
    }

    private void FixedUpdate()
    {
        if(m_active)
        {
            //m_rb = objectSelectScript.m_foundObj.GetComponent<Rigidbody2D>();
            //Vector2 m_liftDist = objectSelectScript.m_foundObj.transform.position - new Vector3(0f,5f,0f);
            //m_liftForce = CalculatForce(m_finalLiftVelocity, m_timeToSetLiftVelocity, 5f);
            //m_rb.AddForce((m_liftDist).normalized * m_liftForce, ForceMode2D.Force);
            //objectSelectScript.m_foundObj;

            objectSelectScript.m_foundObj.transform.position += new Vector3(0f, 50f * Time.deltaTime, 0f);
        }
    }

    public void FoundSprite()
    {
        m_active = true;
    }

    public void LostSprite()
    {
        m_active = false;
    }

    float CalculatForce(float Fvelocity, float time, float dist)
    {
        float acceleration = Fvelocity - 0 / time;
        float force = m_rb.mass * acceleration;
        return force = force / dist;
    }
}
