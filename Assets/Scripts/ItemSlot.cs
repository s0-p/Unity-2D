using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    [SerializeField]
    GameItem m_item;
    [SerializeField]
    bool m_isEmpty;
    public bool IsEmpty { get { return m_isEmpty = m_item == null;} set { m_isEmpty = value; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
