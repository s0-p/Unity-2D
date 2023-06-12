using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
//#if UNITY_EDITOR
    public int HP { get; set; }
//#else
//    public int HP;
//#endif
    [SerializeField]
    Inventory m_inventory;
    [SerializeField]
    UISprite m_slotCursor;

    Animator m_animator;
    Rigidbody2D m_rigidbody;
    [SerializeField]
    float m_speed = 10f;
    Vector3 m_dir;

    [SerializeField]
    GameObject m_bulletPrefab;
    [SerializeField]
    Transform m_firePos;

    [SerializeField]
    float m_jumpPower = 10f;
    int m_jumpCount = 0;
    bool IsGrounded { get; set; }
    bool m_IsFalling;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        Debug.Log("플레이어 인스턴스 완료!");
    }
    private void OnEnable()
    {
        HP = 100;
        Debug.Log("플레이어 현재 체력 : " + HP);
    }
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}
    void FixedUpdate()
    {
        m_rigidbody.velocity += (Vector2)m_dir * m_speed * Time.deltaTime;
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

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))    //Idle
        {
            m_dir = Vector3.zero;
            m_animator.SetBool("IsMove", false);
        }
        if (Input.GetKey(KeyCode.LeftArrow))    //Left walk
        {
            //gameObject.transform.position += Vector3.left * 0.01f;
            m_dir = Vector3.left;
            //m_spriteRenderer.flipX = false;
            //gameObject.transform.rotation = Quaternion.identity; //Quaternion.Euler(0f, 0f, 0f);
            transform.eulerAngles = Vector3.zero;
            m_animator.SetBool("IsMove", true);
        }
        if (Input.GetKey(KeyCode.RightArrow))      //Right walk
        {
            //gameObject.transform.position += Vector3.right * 0.01f;
            m_dir = Vector3.right;
            //m_spriteRenderer.flipX = true;
            //gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            transform.eulerAngles = new Vector3(0f, 180f, 0);
            m_animator.SetBool("IsMove", true);
        }
        //var stateInfo = m_animator.GetCurrentAnimatorStateInfo(0);
        //if (!stateInfo.IsName("Fire"))  //공격중이 아닐 경우 이동
        //{
        //    transform.position += m_dir * m_speed * Time.deltaTime;
        //}

        if (Input.GetKeyDown(KeyCode.LeftControl))    //Fire
        {
            //CreateBullet();
            m_animator.SetBool("IsFire", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            m_animator.SetBool("IsFire", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && m_jumpCount < 2)    //JumpUp
        {
            m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, 0f);
            m_rigidbody.AddForce(Vector2.up * m_jumpPower, ForceMode2D.Impulse);
            m_jumpCount++;
            m_animator.SetInteger("JumpState", 1);
        }
        if (!IsGrounded)    //JumpDown
        {
            if (m_rigidbody.velocity.y < 0)
            {
                if (!m_IsFalling)
                {
                    m_IsFalling = true;
                    m_animator.SetInteger("JumpState", 2);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadSceneManager.Instance.LoadSceneAsync(Scene.Title);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (m_inventory.gameObject.activeSelf)
            {
                m_slotCursor.enabled = false;
                m_inventory.gameObject.SetActive(false);
            }
            else
            {
                m_inventory.gameObject.SetActive(true);
            }

        }
    }
    void OnTriggerExit2D(Collider2D collision)      //JumpUp
    {
        if (collision.CompareTag("Background"))
        {
            IsGrounded = false;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)     //JumpDown
    {
        if (collision.CompareTag("Background"))
        {
            m_IsFalling = false;
            IsGrounded = true;
            m_jumpCount = 0;
            m_animator.SetInteger("JumpState", 0);
        }
    }
    void CreateBullet()
    {
        var obj = Instantiate(m_bulletPrefab);
        var bullet = obj.GetComponent<BulletController>();
        bullet.SetBullet(m_firePos.position, transform.eulerAngles.y == 180 ? Vector3.right : Vector3.left);
    }
}
