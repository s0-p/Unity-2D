using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator m_animator;
    [SerializeField] SpriteRenderer m_spriteRenderer;
    [SerializeField] float m_speed = 10;
    Rigidbody2D m_rigidbody2D;
    Vector3 m_dir;

    public int HP { get; set; }
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
        m_animator = gameObject.GetComponent<Animator>();
        m_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        m_rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_animator.SetBool("IsFire", true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            m_animator.SetBool("IsFire", false);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            m_animator.SetBool("IsMove", false);
            m_dir = Vector3.zero;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //gameObject.transform.position += Vector3.left * 0.01f;
            m_dir = Vector3.left;
            m_animator.SetBool("IsMove", true);
            m_spriteRenderer.flipX = false;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //gameObject.transform.position += Vector3.right * 0.01f;
            m_dir = Vector3.right;
            m_animator.SetBool("IsMove", true);
            m_spriteRenderer.flipX = true;
        }
        var stateInfo = m_animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName("Fire"))
        {
            gameObject.transform.position += m_dir * m_speed * Time.deltaTime;
        }
    }
}
