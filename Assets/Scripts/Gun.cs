using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    Joystick joystick;
    [SerializeField]
    Transform MainGun;
    Transform position => transform;
    [SerializeField]
    Transform direction;
    [SerializeField]
    float power;
    private float mouseX => joystick.Horizontal;
    private float mouseY => joystick.Vertical;
    [SerializeField]
    private float GunSpeed;

    private Vector3 mouseStart;
    private Vector3 mouseDelta;
    private void Update()
    {

        RotLogic();

    }
    private void RotLogic()
    {
        Vector3 RotVector = new Vector3(0f, mouseX, mouseY) * Time.deltaTime * GunSpeed;

        if (joystick.Vertical != 0 && joystick.Horizontal != 0 || joystick.Vertical != 0 || joystick.Horizontal != 0)
        {
            MainGun.rotation = Quaternion.Euler(MainGun.rotation.eulerAngles + Quaternion.Euler(RotVector).eulerAngles);

            MainGun.rotation = Quaternion.Euler(0f, Mathf.Clamp(MainGun.rotation.eulerAngles.y, 30, 150), Mathf.Clamp(MainGun.rotation.eulerAngles.z, 240, 320));
        }

    }
    public void Shoot()
    {
        Pool.instance.ThrowBomb(position.position, direction.position, power);
    }
}
