using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearController : MonoBehaviour
{
    //RigidBody and Transform For each Gear
    private Transform Gear;
    Rigidbody2D body;

    //Referenced for Unfinished Shuffle Function.
    GearController[] gears;
    public Transform CurrentPos;

    // Thrust Force of each Gear
    public float Thrust = 10120f;
    public float ReverseThrust;
    public float SavedThrust;

    //Reverse and Pause Bools
    public bool reverse;
    public bool isPaused;
    
    
  // Zoom functionality Variables
    public Camera cam;
    float defaultFov;
    Vector3 DefaultCamPos;
    public bool zoomOn;
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        defaultFov = Camera.main.fieldOfView;
        DefaultCamPos = cam.transform.position;
        Gear = GetComponent<Transform>();
        SavedThrust = Thrust;
        body = GetComponent<Rigidbody2D>();
        ReverseThrust = Thrust * -1;
        gears = FindObjectsOfType<GearController>();
        CurrentPos = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Check to see if zoom is on then focus in.
        if (zoomOn)
        {
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity,5))
            {
                cam.transform.position = hit.point;
            }

           
        }

        if (reverse == true && isPaused == false)
        {
            //Reverse Direction
            Thrust = ReverseThrust;
        }

        else if(reverse == false && isPaused == false)
        {
            Thrust = SavedThrust;
        }
        

        

        //Add Force to the Gears
        body.AddTorque(Thrust);

    }

    
    private void OnMouseOver()
    {
        if (zoomOn)
        {
            //Zoom in when mouse is over this gear.
            Vector3 zoomPos = new Vector3(transform.position.x, transform.position.y, -361);
            cam.transform.position = zoomPos;
        }
    }

    private void OnMouseExit()
    {
        if (zoomOn)
        {
            //Zoom out when mouse isn't over this gear.
            Vector3 zoomPos = new Vector3(1004.7f, 557.6f, -981);
            cam.transform.position = zoomPos;
        }
    }

    public void Play()
    {
        Thrust = SavedThrust;
        isPaused = false;
    }

    public void Pause()
    {
        Thrust = 0;
        isPaused = true;
    }

    public void Reverse()
    {
        reverse = !reverse;
    }

    public void Zoom()
    {
        zoomOn = !zoomOn;

        if (zoomOn)
        {
            //Change Camera settings and position to zoom in showing transition to zoom mode.
            cam.fieldOfView = 50;
            Vector3 zoomPos = new Vector3(1004.7f, 557.6f, -981);
            cam.transform.position = zoomPos;
            canvas.renderMode = RenderMode.WorldSpace;
            
        }

        else
        {
            //Change camera settings back to default
            cam.fieldOfView = defaultFov;
            cam.transform.position = DefaultCamPos;
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }

    }

    //Shuffle Function unfinished can uncomment to see current condition.
    //public void Shuffle()
    //{
    //    Vector3 newpos = new Vector3(Random.Range(-650, 650), Random.Range(-330, 330), 0);
    //    foreach (var Gear in gears)
    //    {

    //        while (Vector3.Distance(Gear.CurrentPos.localPosition, newpos) < Vector3.Distance(Gear.CurrentPos.localPosition, newpos) / 2)
    //        {

    //            newpos = new Vector3(Random.Range(-650, 650), Random.Range(-330, 330), 0);
    //        }

    //    }
    //    Gear.localPosition = newpos;
    //    Gear.localRotation = new Quaternion(0, 0, Random.Range(-180, 180), 0);
    //    float scaleValue = Random.Range(0.3f, 1);
    //    Vector3 randScaleVec = new Vector3(scaleValue, scaleValue, scaleValue);
    //    Gear.localScale = randScaleVec;

    //}
}
