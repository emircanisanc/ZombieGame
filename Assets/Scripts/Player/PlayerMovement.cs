using Health;
using UnityEngine;
using Interfaces;
using System;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private IReloadable gun;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Transform cam;

    [SerializeField] private float speed = 6;

    [SerializeField] private float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    [SerializeField] private LayerMask enemyLayer;

    public float Speed{get; private set;}

    private bool canMove = true;

    private float horizontal;
    private float vertical;

    RaycastHit[] hits;
    Transform lastTarget;
    private int rayCounter;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        gun = GetComponentInChildren<IReloadable>();

        gun.OnReload += DisableMove;
        gun.OnReloadEnd += EnableMove;
        PlayerHealth.OnPlayerDie += DisableMove;
        UpgradeArea.OnSafeAreaEntered += DisableMove;
        UpgradeArea.OnSafeAreaDisabled += EnableMove;

        hits = new RaycastHit[15];
    }
    void OnDestroy()
    {
        gun.OnReload -= DisableMove;
        gun.OnReloadEnd -= EnableMove;
        PlayerHealth.OnPlayerDie -= DisableMove;
        UpgradeArea.OnSafeAreaEntered -= DisableMove;
        UpgradeArea.OnSafeAreaDisabled -= EnableMove;
    }

    void Update()
    {
        if(!canMove)
        {
            Speed = 0f;
            return;
        }
        
        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;
        
        Vector3 direction = new Vector3(horizontal, 0f, vertical);
        Speed = direction.magnitude;
        if(Speed >= 0.1f)
        {
            // Calculate the direction relative to the camera's forward vector
            Vector3 camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
            direction = camForward * vertical + cam.right * horizontal;

            RotatePlayer(direction);

            // Move player towards direction
            controller.Move(direction * speed * Time.deltaTime);
        }
        else
        {

            if(lastTarget == null)
            {
                rayCounter++;
                if(rayCounter % 20 != 0)
                {
                    return;
                }
                rayCounter = 1;
            } else {
                rayCounter++;
                if(rayCounter % 2 != 0)
                {
                    return;
                }
                rayCounter = 0;
            }

            

            Vector3 pos = transform.position;
            Vector3 forw = transform.forward;

            if(Physics.Raycast(pos, forw, out var hit, 2f, enemyLayer))
            {
                lastTarget = hit.transform;
                return;
            }
                
            if(Physics.SphereCastNonAlloc(pos + forw * 0.5f, 3.5f, forw, hits, 0f, enemyLayer) > 0) {
                if (Contains(hits) && lastTarget.gameObject.activeSelf) {
                    if (Vector3.Distance(pos, lastTarget.position) > 3f) {
                        lastTarget = hits[0].transform;
                    } 
                } else {
                    lastTarget = hits[0].transform;
                }
                direction = (lastTarget.position - pos).normalized;
                RotatePlayer(direction);
            } else {
                lastTarget = null;
            }
        }
    }

    private void RotatePlayer(Vector3 direction) {
        // Rotate player towards movement                
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    private bool Contains(RaycastHit[] hits) {
        if (lastTarget == null)
            return false;
        foreach (var hit in hits) {
            if (hit.transform == lastTarget)
                return true;
            continue;
        }
        return false;
    }

    private void EnableMove()
    {
        canMove = true;
    }
    private void DisableMove()
    {
        canMove = false;
    }
}
