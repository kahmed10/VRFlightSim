using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_Collide : MonoBehaviour {

    private float rocketSpeed = 600.0f;
    private ParticleSystem trail;
    private ParticleSystem.ShapeModule explosion;
    private MeshRenderer rocketMesh;
    private Rigidbody rocketPhysics;

    private AudioSource explosionSound;

    // Use this for initialization
    void Start () {
        //Debug.Log("Started this script");
        gameObject.AddComponent<Rigidbody>();
        gameObject.AddComponent<CapsuleCollider>();

        rocketMesh = GetComponent<MeshRenderer>();
        rocketPhysics = GetComponent<Rigidbody>();

        trail = GetComponent<ParticleSystem>(); // when rocket shoots, make a trail
        explosion = trail.shape;
        explosionSound = GetComponent<AudioSource>();

        trail.Play();
        

        rocketPhysics.velocity = transform.forward * rocketSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        // explode if it hits something
        //Debug.Log("hit something: " + other.gameObject.name);
        if (other.gameObject.tag == "Sphere")
        {
            other.gameObject.GetComponent<Sphere_Disappear>().isActive = false;
        }
        
        if (other.gameObject.name != "plane")  // sometimes it might hit plane, ignore
        {
            explosion.shapeType = ParticleSystemShapeType.Sphere;
            explosion.position = Vector3.zero;
            explosion.scale = new Vector3(10f, 10f, 10f);
            explosionSound.Play();
            rocketMesh.enabled = false;
            rocketPhysics.isKinematic = true;
            Destroy(gameObject, 2.0f);
        }

        //if (other.gameObject.name == "Terrain")
        //{
        //    Debug.Log("Destroyed");
        //    explosion.shapeType = ParticleSystemShapeType.Sphere;
        //    explosion.position = Vector3.zero;
        //    explosion.scale = new Vector3(10f, 10f, 10f);
        //    rocketMesh.enabled = false;
        //    rocketPhysics.isKinematic = true;
        //    Destroy(gameObject, 2.0f);
        //}
        //else if (other.gameObject.tag == "Sphere")
        //{
        //    other.gameObject.GetComponent<Sphere_Disappear>().isActive = false;
        //    explosion.shapeType = ParticleSystemShapeType.Sphere;
        //    explosion.position = Vector3.zero;
        //    explosion.scale = new Vector3(10f, 10f, 10f);
        //    rocketMesh.enabled = false;
        //    rocketPhysics.isKinematic = true;
        //    Destroy(gameObject, 2.0f);
        //}
    }
}
