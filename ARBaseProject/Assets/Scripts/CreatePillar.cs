using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePillar : MonoBehaviour
{

    [SerializeField] private bool m_active = false;
    [SerializeField] GameObject m_pillarObj;
    private GameObject m_pillarUp;
    private bool m_pillarSpawned = false;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (m_active && m_pillarSpawned == false)
        {
            Vector2 spawnPos = new Vector2(transform.position.x + 3.5f, transform.position.y);
            m_pillarUp = Instantiate(m_pillarObj, spawnPos, m_pillarObj.transform.rotation);
            m_pillarSpawned = true;
            m_active = false;
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

    public void DestroyPillar()
    {
        Destroy(m_pillarUp);
        m_pillarSpawned = false;
    }

}
