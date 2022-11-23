using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    Transform MainGun;
    Transform position => transform;
    [SerializeField]
    Transform direction;
    [SerializeField]
    float power;
    private float mouseX => Input.mousePosition.x;
    private float mouseY => Input.mousePosition.y;
    [SerializeField]
    private float GunSpeed;
    private Vector3 mouseCurrent => new Vector3(0f, mouseX, mouseY);
    private Vector3 mouseStart;
    private Vector3 mouseDelta;
    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
            {
            
            mouseStart = mouseCurrent;
            }
        if(Input.GetMouseButton(0))
        {
            mouseDelta = (mouseCurrent - mouseStart) * Time.deltaTime * GunSpeed;
            mouseStart = mouseCurrent;
            
            MainGun.rotation = Quaternion.Euler( MainGun.rotation.eulerAngles + Quaternion.Euler(mouseDelta).eulerAngles) ;
            //print(MainGun.rotation.eulerAngles.z);
            MainGun.rotation = Quaternion.Euler(0f, Mathf.Clamp(MainGun.rotation.eulerAngles.y, 30, 150), Mathf.Clamp(MainGun.rotation.eulerAngles.z, 240, 320));
        }
        
    }
    public void Shoot()
    {
        Pool.instance.ThrowBomb(position.position, direction.position, power);
    }
}
