using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour
{

    private MeshRenderer meshRenderer;
    private bool ToKill = false;
    private Rigidbody rb;
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Guy")
        {
            if (ToKill)
            {
                other.GetComponent<Guy>().Death();

            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        Kill();
        Pool.instance.SpawnExplosion(transform.position);
        

    }
    public void Explode()
    {
        

    }
    private void Kill()
    {
        StartCoroutine(StartKill());
    }
    private IEnumerator StartKill()
    {
        ToKill = true;
        meshRenderer.enabled = false;
        rb.isKinematic = true;
        yield return new WaitForSeconds(0.1f);

        ToKill = false;
        Pool.instance.ReturnBomb(gameObject);
        meshRenderer.enabled = true;
        rb.isKinematic = false;
    }
}
