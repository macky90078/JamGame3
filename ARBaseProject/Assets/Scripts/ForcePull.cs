using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePull : MonoBehaviour {

    [SerializeField] private bool m_active = false;
    [SerializeField] GameObject m_mainCamera;
    [SerializeField] float m_unitsPerSecond = 10f;
    [SerializeField] float m_maxDistanceFromPlayer = 5;

    public float test;

    ObjectSelect objectSelectScript;


    private float m_pullForce;

    [SerializeField] private Rigidbody2D m_rbSelectObj;

    private void Awake()
    {
        m_rbSelectObj = null;
        objectSelectScript = m_mainCamera.GetComponent<ObjectSelect>();
    }

    private void FixedUpdate()
    {
        if (m_active)
        {
            m_rbSelectObj = objectSelectScript.m_foundObj.GetComponent<Rigidbody2D>();
            Vector3 objectPos = m_rbSelectObj.transform.position;
            float maxHight = transform.position.x + m_maxDistanceFromPlayer;

            //objectPos.x = Mathf.Clamp(objectPos.x, objectPos.x, maxHight);
            //m_rbSelectObj.transform.position = objectPos;

 

            float playerObjDist = objectPos.x - transform.position.x;
            Vector2 dirOfPlayer = new Vector2(playerObjDist, 0f);
            dirOfPlayer = dirOfPlayer.normalized;
            test = Mathf.Abs(playerObjDist);

            if (dirOfPlayer.x < 0 && Mathf.Abs(playerObjDist) < 20f)
            {
                float finalVelocityUp = CalculateFinalVelocity(m_unitsPerSecond, 1f, m_rbSelectObj.velocity.x);
                float accelerationUp = CalculateAcceleration(finalVelocityUp, m_rbSelectObj.velocity.x, 1f);
                float spurtForceUp = CalculateLaunchForce(m_rbSelectObj.mass, accelerationUp);

                m_rbSelectObj.AddForce(dirOfPlayer * -spurtForceUp, ForceMode2D.Impulse);
            }
            else if (dirOfPlayer.x > 0 && Mathf.Abs(playerObjDist) < 20f)
            {
                float finalVelocityUp = CalculateFinalVelocity(m_unitsPerSecond, 1f, -m_rbSelectObj.velocity.x);
                float accelerationUp = CalculateAcceleration(finalVelocityUp, -m_rbSelectObj.velocity.x, 1f);
                float spurtForceUp = CalculateLaunchForce(m_rbSelectObj.mass, accelerationUp);

                m_rbSelectObj.AddForce(dirOfPlayer * -spurtForceUp, ForceMode2D.Impulse);
            }

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

    float CalculateFinalVelocity(float dist, float time, float initVelocity)
    {
        return (dist / time) - initVelocity / 2;
    }
    float CalculateAcceleration(float finalVelocity, float initVelocity, float time)
    {
        return (finalVelocity - initVelocity) / time;
    }
    float CalculateLaunchForce(float mass, float acceleration)
    {
        return mass * acceleration;
    }
}
