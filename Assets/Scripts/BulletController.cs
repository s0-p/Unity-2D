using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D m_rigidbody;
    Camera m_mainCamera;
    
    [SerializeField]
    float m_speed = 10f;
    Vector3 m_dir = Vector3.left;
    //float m_time;
    float m_duratuion = 1.5f;

    void Awake()
    {
        //var obj = GameObject.FindGameObjectWithTag("Main Camera");
        //if (obj != null)
        //{
        //    m_mainCamera = obj.GetComponent<Camera>();
        //}
        m_mainCamera = Camera.main;
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if (m_time > m_duratuion)     //시간 기준으로 bullet 삭제
        //{
        //    Destroy(gameObject);
        //}
        //m_time += Time.deltaTime;

        var viewPos = m_mainCamera.WorldToViewportPoint(transform.position);    //좌표로 카메라 끝에서 bullet 삭제
        if (viewPos.x < 0f || viewPos.x > 1f)
        {
            RemoveBullet();
        }
        //transform.position += m_dir * m_speed * Time.deltaTime;
    }

    //void OnBecameInvisible()  //카메라에서 안보이면 삭제
    //{
    //    Destroy(gameObject);
    //}
    void RemoveBullet()
    {
        Destroy(gameObject);
    }
    public void SetBullet(Vector3 pos, Vector3 dir)
    {
        transform.position = pos;
        m_dir = dir;
        m_rigidbody.AddForce(m_dir * m_speed, ForceMode2D.Impulse);
        //Invoke("RemoveBullet", m_duratuion);    //시간 기준으로 bullet 삭제
    }
}
