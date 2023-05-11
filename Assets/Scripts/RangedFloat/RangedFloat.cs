using UnityEngine;

[System.Serializable]
public class RangedFloat
{
    public float min;
    public float max;
    public float RandomValue => Random.Range(min, max);
}
