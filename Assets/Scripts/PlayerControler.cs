using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [Header("Laser gun settings")]
    [SerializeField] GameObject[] lasers;
    
    [Header("General ship settings")]
    [SerializeField] float xTurnSpeed = 25;
    [SerializeField] float yTurnSpeed = 25;
    [SerializeField] float xRange = 5f;
    [SerializeField] float yRange = 3.5f;
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float controlRollFactor = -10f;
    float xThrow , yThrow;
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFire();
    }
        void ProcessTranslation()
    {
        float clapedXPos = xTranslation();
        float clapedYPos = yTranslation();

        transform.localPosition = new Vector3(clapedXPos, clapedYPos, transform.localPosition.z);
    }
    void ProcessRotation()
    {
        float pitch = PitchRotator();
        float yaw = YawRotator();
        float roll = RollRotator();
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
    void ProcessFire()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }
    float xTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        float xOffset = xThrow * Time.deltaTime * xTurnSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clapedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        return clapedXPos;
    }
    float yTranslation()
    {
        yThrow = Input.GetAxis("Vertical");
        float yOffset = yThrow * Time.deltaTime * yTurnSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clapedYPos = Mathf.Clamp(rawYPos, -yRange, yRange * 2);
        return clapedYPos;
    }
    float PitchRotator()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        return pitch;
    }
    float YawRotator()
    {
        float yaw = transform.localPosition.x * positionYawFactor;
        return yaw;
    }
    float RollRotator()
    {
        float roll = xThrow * controlRollFactor; 
        return roll;
    }  
    private void SetLasersActive(bool isActive)
    {
        foreach(GameObject laser in lasers)
        {
            var emissionModuel = laser.GetComponent<ParticleSystem>().emission;
            emissionModuel.enabled = isActive;
        }
    }
}
