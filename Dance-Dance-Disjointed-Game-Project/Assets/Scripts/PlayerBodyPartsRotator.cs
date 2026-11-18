using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyPartsRotator : MonoBehaviour
{
    [SerializeField] KeyCode rotateClockwiseKey;
    [SerializeField] KeyCode rotateCounterClockwiseKey;

    [SerializeField] private float rotationAmount = 15f;

    [SerializeField] private Vector3 rotationAxis = Vector3.forward;

    public DancePoseSequence dancePoseSequence;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Clockwise Rotation
        if (Input.GetKeyDown(rotateClockwiseKey))
        {
            RotateBodyPart(-rotationAmount);
        }
        // Counter-Clockwise Rotation
        else if (Input.GetKeyDown(rotateCounterClockwiseKey))
        {
            RotateBodyPart(rotationAmount);
        }
    }

    void RotateBodyPart(float angle)
    {
        transform.Rotate(rotationAxis, angle, Space.Self); // rotate around the body part's pivot point

        dancePoseSequence.CheckGroupAlignment();
    }
}
