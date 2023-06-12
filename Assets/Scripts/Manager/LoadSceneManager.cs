using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
public enum Scene
{
    None = -1,
    Title,
    Lobby,
    Game
}
public class LoadSceneManager : SingletonDontDestroy<LoadSceneManager>
{
    AsyncOperation m_loadingInfo;
    [SerializeField]
    GameObject m_loadingObj;
    [SerializeField]
    UIProgressBar m_loadingBar;
    [SerializeField]
    UILabel m_labelProgress;

    //static LoadSceneManager m_instance;
    //public static LoadSceneManager Instance { get { return m_instance; } }

    Scene m_scene;  //���� scene
    Scene m_loadingScene = Scene.None;

    //void Awake()
    //{
    //    if (m_instance == null)     //ó�� instance
    //    {
    //        m_instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    // Start is called before the first frame update
    protected override void OnStart()
    {
        m_loadingObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_loadingInfo != null && m_loadingScene != Scene.None)
        {
            if (m_loadingInfo.isDone)
            {
                m_loadingInfo = null;
                m_scene = m_loadingScene;
                m_loadingScene = Scene.None;
                m_labelProgress.text = "100%";
                m_loadingBar.value = 100;
                Invoke("HideUI", 1f);
            }
            else    //�ε���
            {
                m_loadingBar.value = m_loadingInfo.progress;
                m_labelProgress.text = (int)(m_loadingInfo.progress * 100) + "%";
            }
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (PopupManager.Instance.IsOpen)
            {
                PopupManager.Instance.ClosePopup();
            }
            else
            {
                switch (m_scene)
                {
                    case Scene.Title:
                        PopupManager.Instance.Open_PopupOkCancel("�ȳ�", "������ �����Ͻðڽ��ϱ�?", () =>
                            {
#if UNITY_EDITOR
                                EditorApplication.isPlaying = false;
#else
                                Application.Quit();
#endif
                            }, null, "��", "�ƴϿ�");

                        break;
                    case Scene.Lobby:
                        PopupManager.Instance.Open_PopupOkCancel("�ȳ�", "Ÿ��Ʋ ȭ������ ���ư��ðڽ��ϱ�?", () =>
                        {
                            LoadSceneAsync(Scene.Title);
                            PopupManager.Instance.ClosePopup();
                        }, null, "��", "�ƴϿ�");
                        break;
                    case Scene.Game:
                        PopupManager.Instance.Open_PopupOkCancel("�ȳ�", "������ �����ϰ� �κ�� ���ư��ðڽ��ϱ�?\r\n(�������� ���� ������ ��� �Ұ� �˴ϴ�.)", () => 
                        {
                            LoadSceneAsync(Scene.Lobby);
                            PopupManager.Instance.ClosePopup();
                        }, null, "��", "�ƴϿ�");
                        break;
                }
            }
        }
    }
    public void LoadSceneAsync(Scene scene)
    {
        if (m_loadingScene != Scene.None) return;   //�ε���
        m_loadingObj.SetActive(true);
        m_loadingScene = scene;
        m_loadingInfo = SceneManager.LoadSceneAsync((int)m_loadingScene);
    }
    void HideUI()
    {
        m_loadingObj.SetActive(false);
    }
}
