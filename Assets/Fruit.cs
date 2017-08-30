using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Little fruit class that spawns it's sliced version.
public class Fruit : MonoBehaviour {

    [SerializeField] private GameObject fruitSlicedPrefab;
    private Vector3 direction;
    private Quaternion rotation;

    [SerializeField] private float startForce = 15f;
    private Rigidbody2D rbody;
    private GameObject slicedFruit;
    private bool isSliced = false;

    //------API methods-----
    //Initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        startForce = Random.Range(startForce - 3, startForce + 3);
        rbody.AddForce(transform.up * startForce,ForceMode2D.Impulse);
    }

    public void SliceFruit(Collider2D col, Vector3 velocity, Transform dynamicObject)
    {
        if (!isSliced)
        {
            isSliced = true;
            direction = (velocity.magnitude != 0 ? velocity : (col.transform.position - transform.position).normalized);
            rotation = Quaternion.LookRotation(direction);
            rotation.y = 0.7f; //just for cosmetic purposes
            slicedFruit = Instantiate(fruitSlicedPrefab, transform.position, rotation);
            slicedFruit.transform.SetParent(dynamicObject);
            Destroy(gameObject);
            Destroy(slicedFruit, 3f);
        }
        
    }
}
