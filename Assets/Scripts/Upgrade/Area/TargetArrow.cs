using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArrow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Vector3 offset;

    [SerializeField] private float turnSpeed = 5f;

    private bool isActive;

    void Awake()
    {
        UpgradeArea.OnSafeAreaActive += ActiveArrow;
        UpgradeArea.OnSafeAreaEntered += DisableArrow;
    }

    private void OnDestroy() {
        UpgradeArea.OnSafeAreaActive -= ActiveArrow;
        UpgradeArea.OnSafeAreaEntered -= DisableArrow;
    }

    void LateUpdate()
    {
        if(!isActive)
            return;
        transform.position = playerTransform.position + offset;
        Quaternion targetRot = Quaternion.LookRotation((targetTransform.position - transform.position).normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * turnSpeed);
    }

    private void DisableArrow()
    {
        isActive = false;
        arrow.SetActive(false);
    }
    private void ActiveArrow()
    {
        isActive = true;
        arrow.SetActive(true);
    }
}
