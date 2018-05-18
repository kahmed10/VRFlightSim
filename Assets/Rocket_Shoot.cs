using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rocket_Shoot : MonoBehaviour {

    private GameObject mainCanvas, cdText;
    private Text t;

    private AudioSource rocketAudioLeft, rocketAudioRight;

    private bool rocketSide = false; // false = left side, true = right side
    private bool  onCoolDown = false, internalCoolDownStart = false;

    public float coolDown = 2.5f, spawnLength = 6.0f;
    private float activeCoolDown, internalCoolDown;

    private GameObject rocket, rocketLeft, rocketRight;

    // Use this for initialization
    void Start () {
        mainCanvas = GameObject.Find("MainCanvas");
        cdText = mainCanvas.transform.Find("CDText").gameObject;
        t = cdText.GetComponent<Text>();

        rocketLeft = transform.Find("RocketLeft").gameObject;
        rocketRight = transform.Find("RocketRight").gameObject;

        rocketAudioLeft = transform.Find("RocketAudioLeft").gameObject.GetComponent<AudioSource>();
        rocketAudioRight = transform.Find("RocketAudioRight").gameObject.GetComponent<AudioSource>();

        activeCoolDown = 0;
        internalCoolDown = 0;
    }

    // Update is called once per frame
    void Update () {
        if (activeCoolDown >= 0)    // user spammed shoot, give visual cue
            t.enabled = true;
        else
        {
            t.enabled = false;
        }

        if (Input.GetButtonDown("Fire3"))   // "X button" on the xbox controller
        {
            if (activeCoolDown <= 0 ) // so you can't spam the rockets
            {
                if (!rocketSide)
                {
                    rocket = GameObject.Instantiate(rocketLeft, rocketLeft.transform.position, rocketLeft.transform.rotation);
                    rocketSide = true;
                    rocketAudioLeft.Play();

                    internalCoolDown = coolDown;
                    internalCoolDownStart = true;
                }
                else
                {
                    rocket = GameObject.Instantiate(rocketRight, rocketRight.transform.position, rocketRight.transform.rotation);
                    rocketSide = false;
                    rocketAudioRight.Play();

                    activeCoolDown = coolDown;
                }

                rocket.AddComponent<Rocket_Collide>();

                Destroy(rocket, spawnLength);
            }
                
        }
        activeCoolDown -= Time.deltaTime;
        if (internalCoolDownStart)
        {
            internalCoolDown -= Time.deltaTime;
            if (internalCoolDown <= 0)
            {
                // if user does not spam, restart rocket from left side
                rocketSide = false;
                internalCoolDownStart = false;
            }
        }
    }
}
