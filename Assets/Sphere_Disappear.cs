using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere_Disappear : MonoBehaviour {

    private ParticleSystem explosion;
    public bool isActive = true;
    private SphereCollider sphereCollider;
    private MeshRenderer sphereRender;

    public GameObject scoreboard;
    public Keep_Score score;

    public float respawnTimer = 5.0f;

	// Use this for initialization
	void Start () {
        explosion = GetComponent<ParticleSystem>();
        sphereCollider = GetComponent<SphereCollider>();
        sphereRender = GetComponent<MeshRenderer>();

        scoreboard = GameObject.Find("Scoreboard");
        score = scoreboard.GetComponent<Keep_Score>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!isActive)
        {
            score.score += 1;
            explosion.Play();
            sphereCollider.enabled = false;
            sphereRender.enabled = false;
            isActive = true;
            respawnTimer = 5.0f;
        }
        else if (respawnTimer <= 0.0f)
        {
            respawnTimer = 5.0f;
            sphereCollider.enabled = true;
            sphereRender.enabled = true;
        }
        else
            respawnTimer -= Time.deltaTime;
	}

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("got hit by something: " + other.gameObject.name);
    }
}
