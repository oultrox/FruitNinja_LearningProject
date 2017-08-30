using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Blade component that follows the cursor.
public class Blade : MonoBehaviour {

    [SerializeField] GameObject bladeTrailPrefab;
    [SerializeField] private float minCuttingVelocity = 1f;
    [SerializeField] private float bladeSpeed = 80f;

    private Vector3 velocity;
    private Vector2 previousPosition;
    private Vector2 newPosition;

    private Rigidbody2D rbody;
    private CircleCollider2D hitbox;

    private Camera cam;
    private GameObject currentBladeTrail;
    private Transform dynamicObject;

    private bool isCutting = false;
    private float distance;
    private float speed;

    //-------API methods--------
    //Initialization
    private void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();
        hitbox = this.GetComponent<CircleCollider2D>();
        dynamicObject = GameObject.FindGameObjectWithTag("DynamicObject").transform;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            StartCutting();
        }
        
        if(Input.GetMouseButtonUp(0))
        {
            StopCutting();
        }

        
    }

    private void FixedUpdate()
    {
        UpdateCut();
    }

    //Main method that calculates the velocity of our blades swipe movement
    private void UpdateCut()
    {
        hitbox.enabled = false;
        newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        //Gets the velocity of our directional swipe.
        velocity = (newPosition - previousPosition);
        distance = velocity.magnitude;
        speed = distance / Time.deltaTime;

        if (!isCutting)
        {
            rbody.position = newPosition;
        }
  
        //if it's higher than the minimal, enable the collider in order to cut.
        if (isCutting)
        {
            rbody.position = Vector3.MoveTowards(rbody.position, newPosition, bladeSpeed * Time.deltaTime);
            if (speed > minCuttingVelocity)
            {
                hitbox.enabled = true;
            }
            else
            {
                hitbox.enabled = false;
            }
        }
        
        //Updates the position
        previousPosition = newPosition;
    }

    //Starts the cutting instantating the blade.
    private void StartCutting()
    {
        //Always disabling the hitbox because update cut will check if we're slicing at the min speed.
        hitbox.enabled = false;
        isCutting = true;
        currentBladeTrail = Instantiate(bladeTrailPrefab, transform);
        newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    //stops the cutting disabling and destroyinh the blade.
    private void StopCutting()
    {
        hitbox.enabled = false;
        currentBladeTrail.transform.SetParent(null);
        isCutting = false;
        Destroy(currentBladeTrail, 1f);
    }

    //if collides with fruit, cut it!
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Fruit"))
        {
            col.GetComponent<Fruit>().SliceFruit(hitbox,this.velocity, dynamicObject);
        }
    }

    #region Properties
    public bool IsCutting
    {
        get
        {
            return isCutting;
        }

        set
        {
            isCutting = value;
        }
    }

    public Vector3 Velocity
    {
        get
        {
            return velocity;
        }

        set
        {
            velocity = value;
        }
    }

    public float MinCuttingVelocity
    {
        get
        {
            return minCuttingVelocity;
        }

        set
        {
            minCuttingVelocity = value;
        }
    }

    public Vector2 PreviousPosition
    {
        get
        {
            return previousPosition;
        }

        set
        {
            previousPosition = value;
        }
    }

    public Vector2 NewPosition
    {
        get
        {
            return newPosition;
        }

        set
        {
            newPosition = value;
        }
    }

    public Rigidbody2D Rbody
    {
        get
        {
            return rbody;
        }

        set
        {
            rbody = value;
        }
    }

    public CircleCollider2D Hitbox
    {
        get
        {
            return hitbox;
        }

        set
        {
            hitbox = value;
        }
    }

    public Camera Cam
    {
        get
        {
            return cam;
        }

        set
        {
            cam = value;
        }
    }

    public GameObject CurrentBladeTrail
    {
        get
        {
            return currentBladeTrail;
        }

        set
        {
            currentBladeTrail = value;
        }
    }
    #endregion Properties
}
