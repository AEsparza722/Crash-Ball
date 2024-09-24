using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duplicator : MonoBehaviour
{
    [SerializeField] GameObject ball;
    public bool isActive = false;
    bool canDuplicate = true;
    public GameObject duplicatorVFX;

    private void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null && isActive && canDuplicate)
        {
            if (collision.collider.CompareTag("Ball"))
            {
                DuplicateBall(collision.gameObject.GetComponent<Rigidbody>().velocity, collision.transform.position);
                StartCoroutine(CooldownCollision());
            }
        }
    }

    IEnumerator CooldownCollision()
    {
        canDuplicate = false;
        yield return new WaitForSeconds(.3f);
        canDuplicate = true;
    }
    void DuplicateBall(Vector3 velocity, Vector3 position)
    {
        GameObject ballTemp = Instantiate(ball, position, Quaternion.identity);
        ballTemp.GetComponent<Rigidbody>().velocity = velocity;
        //isActive = false;
    }
}
