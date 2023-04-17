using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{

    [SerializeField] private PlayerStackController playerStackController;

    private RaycastHit hit;

    private bool isStack = false;

    private Vector3 direction = Vector3.forward;

    int index;

   
    void Start()
    {
        playerStackController = GameObject.FindObjectOfType<PlayerStackController>();
    }


    private void FixedUpdate()
    {
        SetCubeRaycast();
    }

    private void SetCubeRaycast()
    {
        if (Physics.Raycast(transform.position, direction, out hit, 0.05f))
        {
            if (!isStack)
            {
                isStack = true;
                playerStackController.AddBlock(gameObject);
                SetDirection();
            }

            if(hit.transform.name == "ObstacleCube")
            {
                playerStackController.RemoveBlock(gameObject);
            }
        }
    }

    private void SetDirection()
    {
        direction = Vector3.forward;
    }
   
}
