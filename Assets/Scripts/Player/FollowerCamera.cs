using UnityEngine;

public class FollowerCamera : MonoBehaviour
{
    [SerializeField] private Transform target; // TARGET TRANSFORM TO FOLLOW
    private Vector3 offset; // START OFFSET

    void Awake()
    {
        offset = target.position - transform.position; // FIND OFFSET
    }
    void LateUpdate()
    {
        transform.position = target.position - offset; // ALWAYS KEEP THE SAME DISTANCE WITH TARGET OBJECT
    }
}
