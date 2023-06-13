using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class CoroutineTest : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer m_overatSpr;
    [SerializeField]
    UITexture m_texture;
    float m_duration = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Coroutine_LoadTexture("https://i.pinimg.com/564x/85/65/0c/85650c9622b3d7fc8c63e9dec5dedd9e.jpg"));
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Coroutine_Overay());
        }
    }
    IEnumerator Coroutine_Overay()
    {
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;
            m_overatSpr.color = new Color(1f, 1f, 1f, time / m_duration);
            if (time > m_duration)
            {
                yield break;
            }
            yield return null;
        }
    }
    IEnumerator Coroutine_LoadTexture(string url)
    {
        UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url);
        yield return webRequest.SendWebRequest();
        if (webRequest.isDone)
        {
            m_texture.mainTexture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
            m_texture.MakePixelPerfect();
        }
        else if (webRequest.error != null)
        {
            print(webRequest.error);
        }
        webRequest.Dispose();
    }
}
