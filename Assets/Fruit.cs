using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Little fruit class that spawns it's sliced version.
public class Fruit : MonoBehaviour {

    [SerializeField] private GameObject fruitSlicedPrefab;
    private Vector3 direction;
    private Quaternion rotation;

    //------API methods-----
    //gets direction of the transform to the blade and calculates the angle.
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Blade"))
        {
            
            direction = (col.transform.position - transform.position).normalized; 
            rotation = Quaternion.LookRotation(direction);
            rotation.y = 0.7f; //just for cosmetic purposes
            Instantiate(fruitSlicedPrefab,transform.position, rotation);
            Destroy(gameObject);
        }
    }
}
