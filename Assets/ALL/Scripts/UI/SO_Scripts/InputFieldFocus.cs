
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
[CreateAssetMenu(fileName = "InputFieldFocus", menuName = "SO/UI/InputField/InputFieldFocus")]
public class InputFieldFocus : BaseUI
{
    /*
     현재 스크립트 기능은 Tab키를 누르면  연결된 인풋필드 포커스가 왔다갔다 처리하는 기능
     */
    protected int index = 0;
    private  List<TMP_InputField> m_inputFields= new List<TMP_InputField>();
    public override void InitData(MonoBehaviour owner, params object[] datas)
    {
        int i = 0;
         foreach (var data in datas)
         {
            if(data is GameObject obj)
            {
                if (obj.TryGetComponent<TMP_InputField>(out TMP_InputField inputField))
                {
                    int currentIndex = i;
                    inputField.onSelect.AddListener(_ =>
                    {
                        index = currentIndex;
                        Debug.Log($"{index}");
                    });
                    m_inputFields.Add(inputField);
                    i++;
                }
            }
            
        }
    }
    public void TabFun(InputAction.CallbackContext context)
    {

    }
    public override void Execute()
    {
        index = (index + 1) % m_inputFields.Count;
        m_inputFields[index].Select();
    }
}
