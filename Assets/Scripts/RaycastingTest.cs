using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastingTest : MonoBehaviour
{
    Camera m_mainCamera;
    GameObject m_selectObj;

    Ray ray;
    RaycastHit hit;


    private void Awake()
    {
        m_mainCamera = Camera.main;
    }
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))    //mouse left button
        {
            m_selectObj = GetTouchedObject();
            if (m_selectObj != null)
            {
                m_selectObj.transform.position += Vector3.back * 7f;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (m_selectObj != null)
            {
                m_selectObj.transform.position += Vector3.forward * 7f;
                m_selectObj = null;
            }
        }
        if (m_selectObj == null)
        {
            Debug.DrawRay(ray.origin, ray.direction.normalized * 1000f, Color.green);
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction.normalized * hit.distance, Color.red);
        }

    }

    GameObject GetTouchedObject()
    {
        ray = m_mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000f))
        {
            return hit.collider.gameObject;
        }
        return null;
    }
}
