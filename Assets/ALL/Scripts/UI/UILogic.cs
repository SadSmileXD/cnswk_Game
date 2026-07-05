using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UILogic : MonoBehaviour
{
    [SerializeField] private BaseUI m_UI;
    [SerializeField]private List<Object> m_initObjects = new List<Object>();    
    private void Awake()
    {
        m_UI.InitData(this, m_initObjects.ToArray());
    }
       
}
