using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static AudioSource Instance;

    void Awake() {
        Instance = GetComponent<AudioSource>();
    }
}
