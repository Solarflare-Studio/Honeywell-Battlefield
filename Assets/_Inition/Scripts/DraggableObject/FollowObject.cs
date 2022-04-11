using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField]
    private Transform followTarget;

    private void LateUpdate()
    {
        transform.position = followTarget.position;
        transform.rotation = followTarget.rotation;
    }
}
