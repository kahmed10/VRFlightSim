using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plane_Move : MonoBehaviour {

    public float speedZ;
    //public float rotation;

    //public Transform plane;
    public GameObject me;
    public float speedMin = 0.5f, speedMax = 5.0f;

    //private LineRenderer laser;
    private GameObject shootBase, shooting, engineBase;

    private Canvas screenCanvas;
    private AudioSource engineAudio;

    private GameObject mainCanvas, speedText;
    private Text t;
    public float speedCoolDown = 1.5f;
    private float activeCoolDown;
    private bool enableMin, enableMax;

    private Vector3 initialPos;
    private Rigidbody r;

    // Use this for initialization
    void Start () {
        speedZ = 1.0f;
        Vector3 temp = Vector3.zero; //new Vector3(0f,0f,0f);
        shootBase = transform.Find("ShootBase").gameObject;
        shooting = transform.Find("shootstart").gameObject;

        engineBase = transform.Find("Engine").gameObject;
        engineAudio = engineBase.GetComponent<AudioSource>();

        mainCanvas = transform.Find("MainCanvas").gameObject;
        speedText = mainCanvas.transform.Find("SpeedText").gameObject;
        t = speedText.GetComponent<Text>();
        activeCoolDown = 0;

        r = GetComponent<Rigidbody>();
        


        initialPos = transform.position;

        //laser = shooting.GetComponent<LineRenderer>(); // no longer using raycasting
	}

    // Update is called once per frame
    void Update() {
        r.velocity = Vector3.zero;
        Vector3 temp = me.transform.localRotation.eulerAngles;
        //plane.localRotation = Quaternion.Euler(Input.GetAxis("Vertical")*1 , Input.GetAxis("Horizontal")*1  , Input.GetAxis("Yaw")*1 );
        //me.transform.rotation = Quaternion.Euler(temp.x, temp.y, temp.z - Input.GetAxis("Horizontal"));
        //me.transform.localRotation = Quaternion.Euler(temp.x + Input.GetAxis("Vertical")*1f, temp.y, temp.z - Input.GetAxis("Horizontal") * 1f);
        //me.transform.localRotation = Quaternion.Euler(temp.x + Input.GetAxis("Vertical")*1f, temp.y + Input.GetAxis("Yaw") * 1f, temp.z - Input.GetAxis("Horizontal")*1f ); <- Kinda works

        engineAudio.volume = 0.25f + (0.75f * speedZ / speedMax); // the faster you go, the louder the plane sounds

        if (Input.GetButtonDown("Menu"))
        {
            Debug.Log("Quitting");

            Application.Quit();
        }

        if (speedZ > 4.0f)
        {
            transform.Rotate(Vector3.forward * -0.25f * Input.GetAxis("Horizontal"), Space.Self); //Rotate around -ve Z axis . Zaxis is (0,0,1)
            transform.Rotate(Vector3.right * -0.25f * Input.GetAxis("Vertical"), Space.Self);   //Rotate around -ve X axis. Xaxis  is (1,0,0). Its flipped because joystick is flipped.
            transform.Rotate(Vector3.up * 0.15f * Input.GetAxis("Yaw"), Space.Self);                //Rotate around +ve Y axis which is (0,1,0)
        }
        else if (speedZ > 2.0f)
        {
            transform.Rotate(Vector3.forward * -0.4f * Input.GetAxis("Horizontal"), Space.Self); //Rotate around -ve Z axis . Zaxis is (0,0,1)
            transform.Rotate(Vector3.right * -0.4f * Input.GetAxis("Vertical"), Space.Self);   //Rotate around -ve X axis. Xaxis  is (1,0,0). Its flipped because joystick is flipped.
            transform.Rotate(Vector3.up * 0.2f * Input.GetAxis("Yaw"), Space.Self);                //Rotate around +ve Y axis which is (0,1,0)
        }
        else
        {
            transform.Rotate(Vector3.forward * -0.5f * Input.GetAxis("Horizontal"), Space.Self); //Rotate around -ve Z axis . Zaxis is (0,0,1)
            transform.Rotate(Vector3.right * -0.5f * Input.GetAxis("Vertical"), Space.Self);   //Rotate around -ve X axis. Xaxis  is (1,0,0). Its flipped because joystick is flipped.
            transform.Rotate(Vector3.up * 0.25f * Input.GetAxis("Yaw"), Space.Self);                //Rotate around +ve Y axis which is (0,1,0)
        }
        //transform.Rotate(Vector3.forward * -0.3f * Input.GetAxis("Horizontal"), Space.Self); //Rotate around -ve Z axis . Zaxis is (0,0,1)
        //transform.Rotate(Vector3.right * -0.3f * Input.GetAxis("Vertical"), Space.Self);   //Rotate around -ve X axis. Xaxis  is (1,0,0). Its flipped because joystick is flipped.
        //transform.Rotate(Vector3.up * 0.25f * Input.GetAxis("Yaw"), Space.Self);                //Rotate around +ve Y axis which is (0,1,0)

        if (Input.GetAxis("Throttle") > 0.5) //Right trigger range 0 to 1, so its pressed
        { 
            if (speedZ < speedMax)
            {
                speedZ = speedZ + Time.deltaTime * 0.9f; //5 sec to reach Max speed so 1.8 each second //0.1f Khalique: changing to make it more confortable
                //Debug.Log("pressed Right, speed=" + speedZ);
                enableMax = false;
                activeCoolDown = 0;
            }
            else
            {
                enableMax = true;
                activeCoolDown = speedCoolDown;
            }
        }
        if (Input.GetAxis("Deaccel") > 0.5) //Left trigger range 0 to 1
        {
            if (speedZ > speedMin)
            {
                speedZ = speedZ + Time.deltaTime * -0.9f; //5sec to reach Minimum speed. 0.05f;
                //speedZ *= 0.95f;
                //Debug.Log("pressed Left, speed=" + speedZ);
                enableMin = false;
                activeCoolDown = 0;
            }
            else
            {
                enableMin = true;
                activeCoolDown = speedCoolDown;
            }
        }

        if (enableMin)
        {
            t.enabled = true;
            t.text = "MIN SPEED";
            activeCoolDown -= Time.deltaTime;
            if (activeCoolDown <= 0)
            {
                enableMin = false;
            }
        }

        else if (enableMax)
        {
            t.enabled = true;
            t.text = "MAX SPEED";
            activeCoolDown -= Time.deltaTime;
            if (activeCoolDown <= 0)
            {
                enableMax = false;
            }
        }
        else
            t.enabled = false;

        transform.Translate(Vector3.forward * speedZ, Space.Self);

        Debug.DrawRay(shootBase.transform.position + shootBase.transform.forward * 1f, shootBase.transform.forward);
        //if (Input.GetButtonDown("Fire3"))   // "X button" on the xbox controller
        //{
        //    {
        //        RaycastHit hit;
        //        laser.enabled = true;

        //        laser.SetPosition(0, shootBase.transform.position);
                

        //        if (Physics.Raycast(shooting.transform.position + shooting.transform.forward * 2f, shooting.transform.forward, out hit, 250f))
        //        {
        //            Debug.Log(hit.collider.gameObject.name);
        //            if (hit.collider.gameObject.tag == "Sphere")
        //            {
        //                Sphere_Disappear disappear = hit.collider.gameObject.GetComponent<Sphere_Disappear>();
        //                disappear.isActive = false;
        //            }
        //            laser.SetPosition(1, hit.point);
        //        }
        //        else
        //        {
        //            laser.SetPosition(1, shooting.transform.position + (shooting.transform.forward * 250f));
        //        }
        //    }
            
        //}
        //else
        //{
        //    laser.enabled = false;
        //}
        /*if (Input.GetKey(KeyCode.UpArrow))
        {
            speedZ += 0.05f;
            //transform.rotation *= Quaternion.AngleAxis(180, Vector3.up); //transform.rotation * -1;
            //transform.rotation *= Quaternion.Euler(0, 180, 0);
            Debug.Log("UP pressed. Accelerate");
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            speedZ -= 0.05f;
            //transform.rotation *= Quaternion.AngleAxis(180, Vector3.up); //transform.rotation * -1;
            //transform.rotation *= Quaternion.Euler(0, 180, 0);
            Debug.Log("Down was pressed");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(0.0f, 0.0f, -1.0f);
            //transform.rotation *= Quaternion.AngleAxis(180, Vector3.up); //transform.rotation * -1;
            //transform.rotation *= Quaternion.Euler(0, 180, 0);
            Debug.Log("Left was pressed");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //transform.rotation *= Quaternion.AngleAxis(180, Vector3.up); //transform.rotation * -1;
            //transform.rotation *= Quaternion.Euler(0, 180, 0);
            Debug.Log("Right was pressed");
        }*/

    }

    void OnCollisionEnter (Collision other)
    {
        //Debug.Log("hit something: " + other.gameObject.name);
        if (other.gameObject.name == "Terrain")
        {
            transform.position = initialPos;
            transform.rotation = Quaternion.identity;
        }
    }
}
