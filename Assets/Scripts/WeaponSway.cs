using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{

    [SerializeField] float intensity;
    [SerializeField] float smooth;
    Quaternion original_rotation;
    
    // Start is called before the first frame update
    void Start()
    {
        original_rotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        Sway();
    }

    private void Sway()
    {
        float mouse_x = Input.GetAxis("Mouse X");
        float mouse_y = Input.GetAxis("Mouse Y");
        Quaternion x_adjust = Quaternion.AngleAxis(intensity * mouse_x, Vector3.up);
        Quaternion y_adjust = Quaternion.AngleAxis(intensity * mouse_y, Vector3.right);
        Quaternion target_rotation = original_rotation * x_adjust * y_adjust;

        transform.localRotation = Quaternion.Lerp(transform.localRotation,
                                     target_rotation, Time.deltaTime * smooth);
    }
}
