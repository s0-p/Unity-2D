using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    Inventory m_inventory;
    [SerializeField]
    GameItem m_item;
    [SerializeField]
    bool m_isEmpty;
    [SerializeField]
    bool m_isSelected;
    public bool IsEmpty { get { return m_isEmpty = m_item == null;} set { m_isEmpty = value; } }
    public bool IsSelected { get { return m_isSelected; } set { m_isSelected = value; } }

    // Start is called before the first frame update
    //void Start()
    //{
    //}
    public void SetSlot(Inventory inven)
    {
        m_inventory = inven;
    }
    public void SetItem(GameItem item)
    {
        m_item = item;
        m_isEmpty = false;
        item.transform.SetParent(transform);
        item.transform.localPosition = Vector3.zero;
        item.transform.localScale = Vector3.one;
    }
    public void UseItem()
    {
        if (m_item == null) return;
        var left = m_item.Used();
        if (left == 0)
        {
            m_item = null;
            m_isEmpty = false;
        }
    }
    public void OnSelect()
    {
        m_inventory.OnSelectSlot(this);
    }
}
