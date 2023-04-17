using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    //public float PlayerSpeed = 0.3f;

    //private float horizontalMove;

    //private int collecteddiamond = 0;

   // public Rigidbody rgb;

    public List<string> valuables; //keep track of diamonds

    private void Start()
    {
        valuables = new List<string>();
   
    }



    private void FixedUpdate()
    {
       // MoveForward();

       // MoveHorizontal();

    
    }

    /*
    private void MoveHorizontal()
    {
        horizontalMove = Input.GetAxis("Horizontal") * PlayerSpeed * Time.deltaTime;
        transform.Translate(Vector3.right * horizontalMove, Space.World);
        

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -0.15f, 0.15f), transform.position.y, transform.position.z);
    }

    private void MoveForward()
    {
        transform.position += Vector3.forward * Time.fixedDeltaTime;
    }
    */

    
    private void OnTriggerEnter(Collider other)  //Collect Diamonds
    {
        Debug.Log("Collision");
        if (other.CompareTag("diamond"))
        {

            string valuableType = other.gameObject.GetComponent<Diamond>().valuableType;
           // Debug.Log("you have collected a:"+ valuableType);
            valuables.Add(valuableType);
           // Debug.Log("treasure number:" + valuables.Count);
            Destroy(other.gameObject);
        }

    }

    



}
