using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionHandler : MonoBehaviour
{
    void Start()
    {
        var currentResolution = Screen.currentResolution;
        Screen.SetResolution(currentResolution.width / 2, currentResolution.height / 2, true);
    }
}
