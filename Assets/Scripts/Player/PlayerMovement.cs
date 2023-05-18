using Health;
using UnityEngine;
using Interfaces;

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

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        gun = GetComponentInChildren<IReloadable>();

        gun.OnReload += DisableMove;
        gun.OnReloadEnd += EnableMove;
        PlayerHealth.OnPlayerDie += DisableMove;
        UpgradeArea.OnSafeAreaEntered += DisableMove;
        UpgradeArea.OnSafeAreaDisabled += EnableMove;
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
        # region PLAYER INPUT
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        # endregion
        Vector3 direction = new Vector3(horizontal, 0f, vertical);
        Speed = direction.magnitude;
        if(direction.sqrMagnitude >= 0.1f)
        {
            // Calculate the direction relative to the camera's forward vector
            Vector3 camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
            direction = camForward * vertical + cam.right * horizontal;

            var hits = Physics.OverlapSphere(transform.position + direction * 3f, 2f, enemyLayer);
            if(hits.Length > 0) {
                direction = (hits[0].transform.position - transform.position).normalized;
            }
                
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(direction * speed * Time.deltaTime);
        }
        else
        {
            var hits = Physics.OverlapSphere(transform.position + direction * 2.5f, 2.5f, enemyLayer);
            if(hits.Length > 0) {
                direction = (hits[0].transform.position - transform.position).normalized;
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position + transform.forward * 2f, 4);
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
