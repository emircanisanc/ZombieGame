using System;
using UnityEngine;

[CreateAssetMenu(menuName ="New Float")]
public class Float : ScriptableObject
{
    [SerializeField] private float value;
    public Action<float> OnValueChanged;
    public float Value{get{return value;} set{this.value = value; OnValueChanged?.Invoke(value);}}
    void OnValidate(){Value = value;}
}