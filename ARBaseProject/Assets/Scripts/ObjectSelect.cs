using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelect : MonoBehaviour
{
     public SpriteRenderer m_foundObj;
    [SerializeField] Camera m_mainCam;
    [SerializeField] SpriteRenderer tempObj;

    private void Awake()
    {   
        m_foundObj = tempObj;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_foundObj = ClickSelectTarget();
        }

    }

    SpriteRenderer ClickSelectTarget()
    {
        Ray ray = m_mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit/*Physics.Raycast(ray, out hit, Mathf.Infinity)*/)
        {
            Debug.DrawRay(ray.origin, hit.point);
            if (hit.collider.tag == "Intractable")
            {
                return hit.collider.GetComponent<SpriteRenderer>();
            }
            else { return tempObj; }
        }
        else { return tempObj; }
    }
}
