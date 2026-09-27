using UnityEngine;

[CreateAssetMenu(fileName = "MaterialObject", menuName = "ScriptableObjects/MaterialObjects")]
public class MaterialObjectBlueprint : ScriptableObject
{
    public Sprite icon;
    public GameObject prefab;
    public MaterialSpecs specs;
}
