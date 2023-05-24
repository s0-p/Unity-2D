using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int Hp { get; set; }
    Animator m_animator;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Hp = 5;
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    public void SetDamage(int dmg)
    {
        m_animator.Play("Hit", 0, 0f);
        Hp -= dmg;
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
