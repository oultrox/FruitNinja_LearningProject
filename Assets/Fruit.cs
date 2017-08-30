using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Little fruit class that spawns it's sliced version.
public class Fruit : MonoBehaviour {

    [SerializeField] private GameObject fruitSlicedPrefab;
    private Vector3 direction;
    private Quaternion rotation;


    [SerializeField] private float startForce = 15f;
    private Rigidbody2D rb;
    private GameObject slicedFruit;
    private Transform dynamicParent;

    //------API methods-----
    //Initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * startForce,ForceMode2D.Impulse);
        dynamicParent = GameObject.FindGameObjectWithTag("DynamicObject").transform;

    }

    //gets direction of the transform to the blade and calculates the angle.
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Blade"))
        {
            
            direction = (col.transform.position - transform.position).normalized; 
            rotation = Quaternion.LookRotation(direction);
            rotation.y = 0.7f; //just for cosmetic purposes
            slicedFruit = Instantiate(fruitSlicedPrefab,transform.position, rotation);
            slicedFruit.transform.SetParent(dynamicParent);
            Destroy(gameObject);
            Destroy(slicedFruit, 3f);
        }
    }
}
