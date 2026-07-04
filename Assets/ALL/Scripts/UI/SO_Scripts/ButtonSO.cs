using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Button_Active&Inactive", menuName = "SO/UI/Button/Active&Inactive")]
public class ButtonSO : BaseUI
{
    private Button m_CreateAccountBTN;
    private GameObject m_obj;
    public override void InitData(MonoBehaviour OWner, params object[] datas)
    {
        foreach (object data in datas)
        {
            if(data is GameObject  OBJ)
            {
                if (OBJ.TryGetComponent<Button>(out Button button))
                {
                   
                    m_CreateAccountBTN = button;
                    m_CreateAccountBTN.onClick.AddListener(() =>
                    {
                        Execute();
                    });
                }
                else
                {
                    m_obj = OBJ;
                }
            }
        }
    }

    public override void Execute()
    {
        m_obj.SetActive(!m_obj.activeSelf);
    }


}
