using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjecttileController : MonoBehaviour
{
    Camera m_mainCamera;
    Rigidbody2D m_rigidbody;
    SpringJoint2D m_springJoint;
    LineRenderer[] m_line;

    bool m_isDragging;
    float m_maxDist = 4f;
    float m_sqrMaxDist;

    Vector2 m_preVelocity;
    Vector2 m_originPos;
    private void Awake()
    {
        m_originPos = transform.position;
        m_mainCamera = Camera.main;
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_springJoint = GetComponent<SpringJoint2D>();
        m_sqrMaxDist = Mathf.Pow(m_maxDist, 2f);

        var root = m_springJoint.connectedBody.transform;
        m_line = new LineRenderer[2];
        m_line[0] = root.Find("Ruber_Band").GetComponent<LineRenderer>();
        m_line[1] = root.Find("CatapultFront").transform.Find("Ruber_Band").GetComponent<LineRenderer>();

        //back
        m_line[0].SetPosition(0, m_line[0].transform.position);
        m_line[0].sortingLayerName = "Default";
        m_line[0].sortingOrder = 1;
        m_line[0].startWidth = 0.3f;
        m_line[0].endWidth = 0.3f;

        //front
        m_line[1].SetPosition(0, m_line[1].transform.position);
        m_line[1].sortingLayerName = "Default";
        m_line[1].sortingOrder = 4;
        m_line[1].startWidth = 0.35f;
        m_line[1].endWidth = 0.35f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isDragging)
        {
            var worldPos = m_mainCamera.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0f;
            var dir = worldPos - m_springJoint.connectedBody.transform.position;    //connecterBody¿Í mouse ÁÂÇ¥ Â÷ÀÌ
            Debug.Log(dir);
            if (dir.sqrMagnitude < m_sqrMaxDist)
            {
                //transform.position = m_springJoint.connectedBody.transform.position + dir;
                transform.position = worldPos;
            }
            else
            {
                transform.position = m_springJoint.connectedBody.transform.position + dir.normalized * m_maxDist;
            }
        }
        else if (!m_rigidbody.isKinematic && m_springJoint.enabled)
        {
            if (m_preVelocity.sqrMagnitude > m_rigidbody.velocity.sqrMagnitude)
            {
                m_springJoint.enabled = false;
                m_rigidbody.velocity = m_preVelocity;
                m_preVelocity = Vector2.zero;
                return;
            }
            m_preVelocity = m_rigidbody.velocity;
        }
        DrawRuberBand();
    }
    private void OnMouseDown()
    {
        m_isDragging = true;
    }
    private void OnMouseUp()
    {
        m_isDragging = false;
        m_rigidbody.isKinematic = false;
    }
    private void OnEnable()
    {
        m_rigidbody.isKinematic = true;
    }

   void DrawRuberBand()
    {
        if (m_springJoint.enabled)
        {
            var worldPos = m_mainCamera.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0f;
            var dir = worldPos - m_springJoint.connectedBody.transform.position;

            m_line[0].SetPosition(1, transform.position + dir.normalized * 0.46f);
            m_line[1].SetPosition(1, transform.position + dir.normalized * 0.46f);
        }
        else
        {
            m_line[0].SetPosition(1, m_originPos);
            m_line[1].SetPosition(1, m_originPos);
        }
    }
}
