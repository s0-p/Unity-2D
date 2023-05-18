using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTween : MonoBehaviour
{
    [SerializeField]
    AnimationCurve m_curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    [SerializeField]
    Vector3 m_from;     //출발 위치
    [SerializeField]
    Vector3 m_to;       //도착 위치
    [SerializeField]
    float m_duration = 1f;
    float m_time;
    bool m_isStart;

    // Start is called before the first frame update
    void Start()
    {
        Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isStart)
        {
            if (m_time > 1f)
            {
                m_time = 0f;
                m_isStart = false;
                return;
            }
            m_time += Time.deltaTime / m_duration;
            var value = m_curve.Evaluate(m_time);
            var result = m_from * (1f - value) + m_to * value;
            transform.position = result;
        }
    }

    public void Play()
    {
        m_isStart = true;
        m_time = 0f;
    }
    public void Play(Vector3 from, Vector3 to, float duration)
    {
        m_from = from;
        m_to = to;
        m_duration = duration;
        Play();
    }
}
