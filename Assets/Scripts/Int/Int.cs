using System;
using UnityEngine;

[CreateAssetMenu(menuName ="New Int")]
public class Int : ScriptableObject
{
    [SerializeField] private int value;
    public Action<int> OnValueChanged;
    public int Value{get{return value;} set{this.value = value; OnValueChanged?.Invoke(value);}}
    void OnValidate(){Value = value;}
}