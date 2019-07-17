using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerThrustHandler : MonoBehaviour {

    //public ParticleSystem thrustLeft1, thrustLeft2, thrustLeft3, thrustLeft4;
    //public ParticleSystem thrustRight1, thrustRight2, thrustRight3, thrustRight4;

    [SerializeField]
    ParticleSystem[] leftThrustParticle;

    [SerializeField]
    ParticleSystem[] rightThrustParticle;

    public ParticleSystem thrustBott;

    public AudioSource mainThrustSound, botThrustSound;

    public Transform shipPos;
    //private ParticleSystem.MainModule particlesModule;

    float thrustSpeed;

    private void Start()
    {
        leftThrustParticle = gameObject.GetComponentsInChildren<ParticleSystem>();
        rightThrustParticle = gameObject.GetComponentsInChildren<ParticleSystem>();
        //mainThrustSound = GetComponent<AudioSource>();
        //botThrustSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        GameObject throttleGameObject = GameObject.Find("Text_Throttle_Speed");
        ThrottlePercentageSpeed throttleScript = throttleGameObject.GetComponent<ThrottlePercentageSpeed>();
        thrustSpeed = throttleScript.sliderValue;

        mainThrustSound.mute = false;
        mainThrustSound.pitch = thrustSpeed / 100f + 0.5f;

        bool thrustActive = true;

        var thrustBottEmission = thrustBott.emission;
        thrustBottEmission.enabled = true;

        if (thrustActive)
        {
            foreach (ParticleSystem leftThruster in leftThrustParticle) //Get the emission module of the current child particle ssystem [leftThruster]
            {
                ParticleSystem.EmissionModule leftThrusterEmissionModule = leftThruster.emission;
                leftThrusterEmissionModule.enabled = true;
                leftThrusterEmissionModule.rateOverTime = thrustSpeed;
            }

            foreach (ParticleSystem rightThruster in rightThrustParticle) //Get the emission module of the current child particle ssystem [leftThruster]
            {
                ParticleSystem.EmissionModule rightThrusterEmissionModule = rightThruster.emission;
                rightThrusterEmissionModule.enabled = true;
                rightThrusterEmissionModule.rateOverTime = thrustSpeed;
            }

            if (1 <= thrustSpeed && thrustSpeed < 30)
            {
                botThrustSound.mute = false;
                float thrustBottSpeed = Mathf.Lerp(0f, 100f, Mathf.InverseLerp(30f, 0f, thrustSpeed));
                botThrustSound.pitch = thrustBottSpeed / 100;
                thrustBottEmission.rateOverTime = thrustBottSpeed;
            }

            if (thrustSpeed > 30f)
            {
                thrustBottEmission.rateOverTime = 0;
                botThrustSound.mute = true;
                botThrustSound.pitch = 0f;
            }

            if (1 > thrustSpeed)
            {
                mainThrustSound.mute = true;
                botThrustSound.mute = false;
                thrustBottEmission.rateOverTime = 100;
                botThrustSound.pitch = 2f;
            }
        }

        else
        {
            foreach (ParticleSystem leftThruster in leftThrustParticle) //Get the emission module of the current child particle system [leftThruster]
            {
                ParticleSystem.EmissionModule leftThrusterEmissionModule = leftThruster.emission;
                leftThrusterEmissionModule.enabled = true;
                leftThrusterEmissionModule.rateOverTime = 5f;
            }

            foreach (ParticleSystem rightThruster in rightThrustParticle) //Get the emission module of the current child particle system [leftThruster]
            {
                ParticleSystem.EmissionModule rightThrusterEmissionModule = rightThruster.emission;
                rightThrusterEmissionModule.enabled = true;
                rightThrusterEmissionModule.rateOverTime = 5f;
            }

            thrustBottEmission.rateOverTime = 100f;
        }
    }
}
