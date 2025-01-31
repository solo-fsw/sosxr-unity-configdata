using UnityEngine;


[CreateAssetMenu(fileName = "Demo Config Data", menuName = "SOSXR/Config Data/Demo Config Data")]
public class DemoConfigData : ConfigDataBase
{
    public string TaskName = "TaskToDo";
    public int PPN = -1;
    public Order Order = Order.Counterbalanced;
    public string Url = "https://youtu.be/xvFZjo5PgG0?si=F3cJFXtwofUAeAq2";
    public string VideoName = "VideoName";
    public string ClipDirectory = "/Users/Shared/Movies/Clips";
    public string[] Extensions = {".mp4"};
    public bool ShowDebug = false;
}


public enum Order
{
    InOrder,
    Random,
    Permutation,
    Counterbalanced
}