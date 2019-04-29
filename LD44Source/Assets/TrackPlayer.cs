using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    
    public float trackSpeed = 1;
    public float aimingAccuracy = 0.0001f;
    public bool aimed;

    private Transform target;

    private Quaternion prev = new Quaternion();

    private void Start()
    {
        
    }
    private void Update()
    {
        if (target == null)
        {
            target = transform.parent.GetComponent<SimpleAI>().target.transform;
        }
        Vector3 vectorToTarget = target.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * trackSpeed);
        if (Mathf.Abs(prev.z - Quaternion.Slerp(transform.rotation, q, Time.deltaTime * trackSpeed).z) <= aimingAccuracy)
        {
            aimed = true;
        }
        else
        {
            aimed = false;
        }
        prev = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * trackSpeed);
        
    }
}
