using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    /*
    public GameObject player;

    private Vector3 offset;

    
    void Start()
    {
      
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
    */

    [SerializeField]
    private Transform playerTransform;

    private Vector3 offset;

    private Vector3 newPosition;


    [SerializeField]
    private float lerpValue;

    private void Start()
    {
        offset = transform.position - playerTransform.transform.position;

    }

    private void LateUpdate()
    {
        SetCameraFollow();
    }

    private void SetCameraFollow()
    {
        newPosition = Vector3.Lerp(transform.position, new Vector3(0f, playerTransform.position.y, playerTransform.position.z) + offset, lerpValue * Time.deltaTime);

        transform.position = newPosition;
    }




}
