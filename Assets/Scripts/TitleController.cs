using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    //int m_height = 20;
    //bool[] m_isOn = { false, false };
    //string m_id = "���̵� �Է��ϼ���";
    //string m_password = string.Empty;

    //bool m_isOpen;
    //int m_select;
    //string[] m_weapon = { "��� ��", "���������", "�Ҳɳ� ��", "��Į", "������ ���", "���ڰ�", "��Ż ��", "���ν� ���� ��" };
    //private void OnGUI()
    //{
    //    if (GUI.Button(new Rect((Screen.width - 150) / 2, (Screen.height - 50) / 2, 150, 50), "Start"))    //Rect(x, y, width, height), text
    //    {
    //        print("���ӽ���");
    //    }

    //    GUILayout.BeginArea(new Rect(10, Screen.height - 310, 200, 300), GUI.skin.box);
    //    GUILayout.Button("START");
    //    m_isOn[0] = GUILayout.Toggle(m_isOn[0], "�������");
    //    if (m_isOn[0])
    //    {
    //        GUILayout.TextArea("������� Ȱ��ȭ");
    //    }
    //    m_isOn[1] = GUILayout.Toggle(m_isOn[1], "���ǵ���");
    //    GUILayout.Label ("���̵�");
    //    m_id = GUILayout.TextField(m_id);
    //    GUILayout.Label("��й�ȣ");
    //    m_password = GUILayout.TextField(m_password);
    //    GUILayout.EndArea();

    //    GUILayout.BeginArea(new Rect(Screen.width - 210, Screen.height - m_height, 200, 300), GUI.skin.box);
    //    m_isOpen = GUILayout.Toggle(m_isOpen, "���⼱��", GUI.skin.button);
    //    if (m_isOpen)
    //    {
    //        m_height = 300;
    //        m_select = GUILayout.SelectionGrid(m_select, m_weapon, 1);
    //    }
    //    else
    //    {
    //        m_height = 20;
    //    }
    //    GUILayout.EndArea();
    //}


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoNextScene()
    {
        SceneManager.LoadScene("Game");
    }
}
