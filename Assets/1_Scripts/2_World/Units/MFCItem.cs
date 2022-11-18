using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MFCItem", order = 1)]
public class MFCItem : ScriptableObject
{
    public ResourceType input;
    public string inputName;
    public int inputCnt;
    public string outputName;
    public int getMoney;
}
