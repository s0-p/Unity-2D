using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int HP { get; set; }
    private void Awake()
    {
        Debug.Log("�÷��̾� �ν��Ͻ� �Ϸ�!");
    }
    private void OnEnable()
    {
        HP = 100;
        Debug.Log("�÷��̾� ���� ü�� : " + HP);

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ActionControl();
    }

    void ActionControl()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HP -= 10;
            Debug.Log("���� ü�� : " + HP);
            if (HP <= 0)
            {
                Debug.Log("�÷��̾� ���!");
                gameObject.SetActive(false);
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.position += Vector3.left * 0.01f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.position += Vector3.right * 0.01f;
        }
    }
}
