using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
            Debug.Log("남은 체력 : " + HP);
            if (HP <= 0)
            {
                Debug.Log("플레이어 사망!");
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
