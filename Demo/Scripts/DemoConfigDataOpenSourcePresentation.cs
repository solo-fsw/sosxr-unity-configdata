using UnityEngine;


[CreateAssetMenu(fileName = "Demo Config Data Open Source Presentation", menuName = "SOSXR/Config Data/Demo Config Data Open Source Presentation")]
public class DemoConfigDataOpenSourcePresentation : ConfigDataBase
{
    public string MonsterDemoUrl = "https://youtu.be/xvFZjo5PgG0?si=F3cJFXtwofUAeAq2";
    public string MonsterFBXFolder = "/Users/Mine/3DModels/Monsters";
    public string MainMonsterFBXName = "TRex";
    public string[] Extensions = {".fbx", ".obj", ".3ds"};
    public int NumberOfMonsters = 5;
    public float MonsterScale = 1.0f;
    public Vector3 MonsterSpawnPosition = new(1, 2, 4);
    public bool ShowDebug = false;
}