using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class UpgradeArea : MonoBehaviour
{

    public static Action OnSafeAreaEntered;
    public static Action OnSafeAreaActive;
    public static Action OnSafeAreaDisabled;

    private Vector3 startPos;
    [SerializeField] private float spawnRadius;
    [SerializeField] private Transform sphereTransform;

    private bool isEntered;

    void Awake()
    {
        startPos = sphereTransform.localPosition;
        UpgradeManager.OnUpgradeMenuClosed += SetDisable;
    }
    void OnDestroy()
    {
        UpgradeManager.OnUpgradeMenuClosed -= SetDisable;
    }
    void OnEnable()
    {
        OnSafeAreaActive?.Invoke();
    }
    void OnDisable()
    {
        OnSafeAreaDisabled?.Invoke();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !isEntered)
        {
            isEntered = true;
            OnSafeAreaEntered?.Invoke();
            Time.timeScale = 0;
        }    
    }

    private void SetDisable()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        isEntered = false;
    }

    public bool TryEnableAtRandomPosition()
    {
        if(gameObject.activeSelf)
            return false;

        Vector3 randomPoint = new Vector3(UnityEngine.Random.Range(-spawnRadius, spawnRadius), 0f, UnityEngine.Random.Range(-spawnRadius, spawnRadius));
        NavMeshHit hit;
        int i = 5;
        while (!NavMesh.SamplePosition(randomPoint, out hit, 10f, 3))
        {
            randomPoint = new Vector3(UnityEngine.Random.Range(-50f, 50f), 0f, UnityEngine.Random.Range(-50f, 50f));
            i--;
            if(i == 0)
                break;
        }
        transform.position = randomPoint;
        gameObject.SetActive(true);
        return true;
    }
}
