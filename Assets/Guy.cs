using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guy : MonoBehaviour
{
    
    Vector3 ReturnBound;
    Rigidbody rb;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float SpeedToSet;

    private float Speed;
    private bool ToKill = true;

    private void Awake()
    {
        Speed = SpeedToSet;
        
    }
    private void Start()
    {
        ReturnBound = Pool.instance.DieZ();
        rb = GetComponent<Rigidbody>();
        animator.SetTrigger("Run");
    }
    // Update is called once per frame
    void Update()
    {
        //rb.position += new Vector3(0f, 0f, Time.deltaTime * Speed);
        
       transform.position += new Vector3(0f,0f,Time.deltaTime * Speed);
        if(transform.position.z > ReturnBound.z)
        {
            animator.SetTrigger("Fall");
            Speed = 0;
            rb.isKinematic = true;
            Pool.instance.ReturnGuy(this);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Death();
        }
    }
    public void Death()
    {
        if (ToKill)
        {
            ToKill = false;
            animator.SetTrigger("Fall");
            Speed = 0;
            rb.isKinematic = true;
            Pool.instance.ReturnGuyWithDelay(this, 1f);
        }

        
    }
    public void RunAnim()
    {
        ToKill = true;
        animator.ResetTrigger("Fall");
        animator.ResetTrigger("Default");
        animator.SetTrigger("Run");
        Speed = SpeedToSet;
    }
    public void ResetAnim()
    {
        animator.ResetTrigger("Fall");
        animator.ResetTrigger("Run");
        animator.SetTrigger("Default");
        Speed = 0;
        rb.isKinematic = true;
    }
}
