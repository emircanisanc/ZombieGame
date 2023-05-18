using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New ItemDropData")]
public class ItemDropData : ScriptableObject {
    public DropItem[] dropItems;
}

[System.Serializable]
public class DropItem {
    public ObjType type;
    [Range(0, 1)] public float chance;
}