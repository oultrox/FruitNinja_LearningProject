using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Blade component that follows the cursor.
public class Blade : MonoBehaviour {

    [SerializeField] GameObject bladeTrailPrefab;
    [SerializeField] private float minCuttingVelocity = 0.015f;

    private Vector2 previousPosition;
    private Vector2 newPosition;
    
    private Rigidbody2D rbody;
    private CircleCollider2D hitbox;

    private Camera cam;
    private GameObject currentBladeTrail;

    private bool isCutting = false;
    private float velocity;

    //-------API methods--------
    //Initialization
    private void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();
        cam = Camera.main;
        hitbox = this.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            StartCutting();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopCutting();
        }

        if (isCutting)
        {
            UpdateCut();
        }
	}
    //Main method that calculates the velocity of our blades swipe movement
    private void UpdateCut()
    {
        newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        rbody.position = newPosition;

        //Gets the velocity of our directional swipe.
        velocity = (newPosition - previousPosition).magnitude / Time.deltaTime;

        //if it's higher than the minimal, enable the collider in order to cut.
        if (velocity > minCuttingVelocity)
        {
            hitbox.enabled = true;
        }
        else
        {
            hitbox.enabled = false;
        }

        //Updates the position
        previousPosition = newPosition;
    }

    //Starts the cutting instantating the blade.
    private void StartCutting()
    {
        isCutting = true;
        //Always disabling the hitbox because update cut will check if we're slicing at the min speed.
        hitbox.enabled = false;
        currentBladeTrail = Instantiate(bladeTrailPrefab, transform);
        newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    //stops the cutting disabling and destroyinh the blade.
    private void StopCutting()
    {
        isCutting = false;
        hitbox.enabled = false;
        currentBladeTrail.transform.SetParent(null);
        Destroy(currentBladeTrail, 1f);
    }

    
}
