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
    //float m_duratuion = 1.5f;

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
    //void Start()
    //{
    //}

    // Update is called once per frame
    void Update()
    {
        //if (m_time > m_duratuion)     //�ð� �������� bullet ����
        //{
        //    Destroy(gameObject);
        //}
        //m_time += Time.deltaTime;

        var viewPos = m_mainCamera.WorldToViewportPoint(transform.position);    //��ǥ�� ī�޶� ������ bullet ����
        if (viewPos.x < 0f || viewPos.x > 1f)
        {
            RemoveBullet();
        }
        transform.position += m_dir * m_speed * Time.deltaTime;
    }
    public void SetBullet(Vector3 pos, Vector3 dir)
    {
        transform.position = pos;
        m_dir = dir;
        m_rigidbody.AddForce(m_dir * m_speed, ForceMode2D.Impulse);
        //Invoke("RemoveBullet", m_duratuion);    //�ð� �������� bullet ����
    }
    void RemoveBullet()
    {
        Destroy(gameObject);
    }
    //void OnBecameInvisible()  //ī�޶󿡼� �Ⱥ��̸� ����
    //{
    //    Destroy(gameObject);
    //}
    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag("Background"))
    //    {
    //        RemoveBullet();
    //    }
    //}
    //private void OnCollisionExit2D(Collision2D collision) {}
    //private void OnCollisionStay(Collision collision) {}
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Background"))
        {
            RemoveBullet();
        }
        else if (collision.CompareTag("Enemy"))
        {
            var enemy = collision.gameObject.GetComponent<EnemyController>();
            enemy.SetDamage(1);
            RemoveBullet();
        }
    }
}
