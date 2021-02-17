using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class UIRotateToTarget : MonoBehaviour
{
    [Header("Thing To Look At")]
    public Transform target;
    
    [Header("Speed Of Rotation")]
    public float rotateSpeed;

    [Header("Should You Rotate?")] 
    public bool shouldRotate;

    [Header("Choose Axis To Rotate Around")]
    public RotationAxis rotationAxis;

    [Header("Face Opposite Direction?")] 
    public bool flipFacingAngle;
    

    public enum RotationAxis
    {
        X,
        Y,
        Z,
        XY,
        XZ,
        YZ,
        XYZ
    }
    
     
     void Update()
     {
         if (shouldRotate)
         {
             
             // The step size is equal to speed times frame time.
             float step = rotateSpeed * Time.deltaTime;
         
             Vector3 relativePosition = target.position - transform.position;
             Quaternion targetRotation = Quaternion.LookRotation(relativePosition);
             
             
             switch (rotationAxis)
             {
             case RotationAxis.X:
                 targetRotation.y = 0f;
                 targetRotation.z = 0f;
                 break;
             case RotationAxis.Y:
                 targetRotation.x = 0f;
                 targetRotation.z = 0f;
                 break;
             case RotationAxis.Z:
                 targetRotation.x = 0f;
                 targetRotation.y = 0f;
                 break;
             case RotationAxis.XY:
                 targetRotation.z = 0f;
                 break;
             case RotationAxis.XZ:
                 targetRotation.y = 0f;
                 break;
             case RotationAxis.YZ:
                 targetRotation.x = 0f;
                 break;
             case RotationAxis.XYZ:
                 targetRotation = target.rotation;
                 break;
             }
             
             if (flipFacingAngle)
             {
                 targetRotation *= Quaternion.Euler(0f,180f,0f);
             }
         
             // Rotate our transform a step closer to the target's.
             // transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
             transform.rotation = Quaternion.Lerp(transform.rotation,targetRotation, step);
         }
     }
}
