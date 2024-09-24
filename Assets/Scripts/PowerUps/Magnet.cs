using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] float force;
    bool isMagnet = true;
    public GameObject magnetVfx;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void FixedUpdate()
    {
        if (isMagnet)
        {
            PointEffect();
        }
    }

    void PointEffect()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position,radius);
        
        if(colliders.Length != 0)
        {
            foreach(Collider collider in colliders)
            {
                if (collider.CompareTag("Ball"))
                {
                    Vector3 forceDir = collider.transform.position - transform.position;
                    collider.GetComponent<Rigidbody>().AddForce(forceDir.normalized * force, ForceMode.Force);
                }
            }
        }       
    }
}
