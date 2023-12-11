using UnityEngine;

[CreateAssetMenu(fileName = "New Map", menuName = "Scriptable Objects/Map")]
public class Map : ScriptableObject
{
    [SerializeField] public int levelIndex;

    [SerializeField] public int Clearstar;
    [SerializeField] public string DungeonName;
    [SerializeField] public int LevelText;
    [SerializeField, Range(1, 20)] public int difficulty;
    [SerializeField] public string DungeonEx;

    public Color nameColor;
    
    public Object sceneToLoad;
}