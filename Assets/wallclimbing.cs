using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallclimbing : MonoBehaviour
{
    public float open = 100f;
    public float range = 1f;
    public bool Touchingwall = false;
    public float UpwardSpeed = 3.3f;
    public Camera Cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shoot();

        if(Input.GetKey("w") & Touchingwall == true)
        {
            transform.position += Vector3.up * Time.deltaTime * UpwardSpeed;
            Touchingwall = false;
            GetComponent<Rigidbody>().isKinematic = false;
        }

        if (Input.GetKeyUp("w"))
        {
            GetComponent<Rigidbody>().isKinematic = false;
            Touchingwall = false;
        }
    }

    void shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, range))
        {
            //.Log(hit, transform.name);
            TargetWall target = hit.transform.GetComponent<TargetWall>();
            if (target != null) 
            {
                Touchingwall = true;
            }
        }
    }

}
