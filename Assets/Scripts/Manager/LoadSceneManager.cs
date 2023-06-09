using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    Scene m_scene;  //현재 scene
    Scene m_loadingScene = Scene.None;

    //void Awake()
    //{
    //    if (m_instance == null)     //처음 instance
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
            else    //로딩중
            {
                m_loadingBar.value = m_loadingInfo.progress;
                m_labelProgress.text = (int)(m_loadingInfo.progress * 100) + "%";
            }
        }
    }
    public void LoadSceneAsync(Scene scene)
    {
        if (m_loadingScene != Scene.None) return;   //로딩중
        m_loadingObj.SetActive(true);
        m_loadingScene = scene;
        m_loadingInfo = SceneManager.LoadSceneAsync((int)m_loadingScene);
    }
    void HideUI()
    {
        m_loadingObj.SetActive(false);
    }
}
