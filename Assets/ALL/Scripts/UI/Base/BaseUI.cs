using UnityEngine;


public abstract class BaseUI : ScriptableObject
{
    //초기화용 메소드
    public abstract void InitData(MonoBehaviour OWner, params object[] dats);

    public abstract void Execute();

}
