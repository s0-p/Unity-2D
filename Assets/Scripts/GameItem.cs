using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem : MonoBehaviour
{
    [SerializeField]
    UI2DSprite m_icon;
    [SerializeField]
    UILabel m_cntLable;
    ItemInfo m_ItemInfo;
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    public void InitItem(ItemInfo itemInfo, Sprite icon)
    {
        m_ItemInfo = itemInfo;
        m_icon.sprite2D = icon;
        m_cntLable.text = m_ItemInfo.count.ToString();
    }
    public int Used()
    {
        m_ItemInfo.count--;
        if (m_ItemInfo.count == 0)
        {
            Destroy(gameObject);
            return 0;
        }
        else
        {
            m_cntLable.text = m_ItemInfo.count.ToString();
        }
        return m_ItemInfo.count;
    }
}
