using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    UITweener m_uITweener;

    [SerializeField]
    GameObject m_slotPrefab;
    [SerializeField]
    GameObject m_itemPrefab;
    [SerializeField]
    UISprite m_slotCursor;
    [SerializeField]
    UIGrid m_slotGrid;
    [SerializeField]
    UILabel m_sizeLabel;
    int SlotMax = 24;
    List<ItemSlot> m_slotList = new List<ItemSlot>();

    [SerializeField]
    Sprite[] m_icons;
    [SerializeField]
    ItemData[] m_itemDatas;
    Dictionary<ItemType, ItemData> m_itemTable = new Dictionary<ItemType, ItemData>();

    void OnEnable()
    {
        m_uITweener.ResetToBeginning();
        m_uITweener.PlayForward();
    }
    // Start is called before the first frame update
    void Start()
    {
        m_slotCursor.enabled = false;
        CreateSlot(SlotMax);
        for (int i = 0; i < (int)ItemType.Max; i++)
        {
            m_itemTable.Add((ItemType)i, m_itemDatas[i]);
        }
        gameObject.SetActive(false);
    }
    
    public void CreateSlot(int cnt)
    {
        for (int i = 0; i < cnt; i++)
        {
            var obj = Instantiate(m_slotPrefab);
            obj.transform.SetParent(m_slotGrid.transform);
            obj.transform.localScale = Vector3.one;
            var slot = obj.GetComponent<ItemSlot>();
            slot.SetSlot(this);
            m_slotList.Add(slot);
        }
        m_slotGrid.repositionNow = true;
    }
    public void CreateItem()
    {
        for (int i = 0; i < m_slotList.Count; i++)
        {
            if (m_slotList[i].IsEmpty)
            {
                ItemData data = m_itemTable[(ItemType)Random.Range((int)ItemType.Ball, (int)ItemType.Max)];
                int count = Random.Range(1, 100);
                ItemInfo item = new ItemInfo() { data = data, count = count };

                var obj = Instantiate(m_itemPrefab);
                var gItem = obj.GetComponent<GameItem>();
                gItem.InitItem(item, m_icons[item.data.icon]);
                m_slotList[i].SetItem(gItem);
                break;
            }
        }
    }
    public void OnItemUse()
    {
        var selectedSlot = m_slotList.Find(slot => slot.IsSelected);
        if (selectedSlot != null)
        {
            selectedSlot.UseItem();
        }
    }
    public void OnSelectSlot(ItemSlot slot)
    {
        for (int i = 0; i < m_slotList.Count; i++)
        {
            if (m_slotList[i].IsSelected)
            {
                m_slotList[i].IsSelected = false;
                break;
            }
        }
        if (!m_slotCursor.enabled)
        {
            m_slotCursor.enabled = true;
        }
        slot.IsSelected = true;
        m_slotCursor.transform.position = slot.transform.position;
    }

    public void OnExpandSlot()
    {
        if (m_slotList.Count <= 93)

        {
            SlotMax += 6;
            CreateSlot(6);
            int usingSlot = 0;
            for (int i = 0; i < m_slotList.Count; i++)
            {
                if (!m_slotList[i].IsEmpty)
                {
                    usingSlot++;
                }
            }
            m_sizeLabel.text = $"{usingSlot}/{m_slotList.Count}";
        }
    }    
}