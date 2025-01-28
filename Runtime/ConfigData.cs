using UnityEngine;


[CreateAssetMenu(fileName = "ConfigData", menuName = "SOSXR/ConfigData")]
public class ConfigData : ScriptableObject
{
    public string TaskName = "TaskToDo";
    public int PPN = -1;
    public Order Order = Order.Counterbalanced;
    public string Url = "https://youtu.be/xvFZjo5PgG0?si=F3cJFXtwofUAeAq2";
    public string VideoName = "VideoName";
    public string ClipDirectory;
    public string[] Extensions = {".mp4"};
    public bool ShowDebug = false;


    private void Awake()
    {
        ClipDirectory = FileHelpers.GetArborXRPath(); // As an example. Can easily try to get another path instead.
    }
}


public enum Order
{
    InOrder,
    Random,
    Permutation,
    Counterbalanced
}