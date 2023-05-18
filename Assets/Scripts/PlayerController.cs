using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Animator m_animator;
    [SerializeField]
    SpriteRenderer m_spriteRenderer;
    [SerializeField]
    float m_speed = 10;

    Rigidbody2D m_rigidbody2D;
    BoxCollider2D m_boxCollider2D;
    Vector3 m_dir;

    [SerializeField]
    GameObject m_bulletPrefab;
    [SerializeField]
    Transform m_firePos;

#if UNITY_EDITOR
    public int HP { get; set; }
#else
    public int HP;
#endif
    private void Awake()
    {
        Debug.Log("플레이어 인스턴스 완료!");
    }
    private void OnEnable()
    {
        HP = 100;
        Debug.Log("플레이어 현재 체력 : " + HP);

    }

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_boxCollider2D = GetComponent<BoxCollider2D>();
    }
    void FixedUpdate()
    {
        //m_rigidbody2D.velocity += (Vector2) m_dir * m_speed * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        ActionControl();

    }
    void ActionControl()
    {
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    for (int i = 0; i < 2000; i++)
        //    {
        //        var obj = Instantiate(gameObject);
        //        obj.transform.position = new Vector3(Random.Range(-9f, 9f), Random.Range(-5f, 5f));
        //    }
        //}
        if (Input.GetKeyDown(KeyCode.Space))    //공격
        {
            m_animator.SetBool("IsFire", true);
            //CreateBullet();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            m_animator.SetBool("IsFire", false);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))    //Idle
        {
            m_animator.SetBool("IsMove", false);
            m_dir = Vector3.zero;
        }


        if (Input.GetKey(KeyCode.LeftArrow))    //왼쪽 이동
        {
            //gameObject.transform.position += Vector3.left * 0.01f;
            m_dir = Vector3.left;
            m_animator.SetBool("IsMove", true);
            //m_spriteRenderer.flipX = false;
            //gameObject.transform.rotation = Quaternion.identity; //Quaternion.Euler(0f, 0f, 0f);
            transform.eulerAngles = Vector3.zero;
        }
        if (Input.GetKey(KeyCode.RightArrow))      //오른쪽 이동
        {
            //gameObject.transform.position += Vector3.right * 0.01f;
            m_dir = Vector3.right;
            m_animator.SetBool("IsMove", true);
            //m_spriteRenderer.flipX = true;
            //gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            transform.eulerAngles = new Vector3(0f, 180f, 0);
        }
        var stateInfo = m_animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName("Fire"))  //공격중이 아닐 경우 이동
        {
            transform.position += m_dir * m_speed * Time.deltaTime;
        }
    }

    void CreateBullet()
    {
        var obj = Instantiate(m_bulletPrefab);
        var bullet = obj.GetComponent<BulletController>();
        bullet.SetBullet(m_firePos.position, transform.eulerAngles.y == 180 ? Vector3.right : Vector3.left);
    }
}
