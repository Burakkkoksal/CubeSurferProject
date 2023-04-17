using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerMoverRunner : MonoBehaviour
{
    private bool canMotion = true;

    public float velocityOfPlayer;

    public bool CanMotion { get => canMotion; set => canMotion = value; }

    public GameObject Effect;

    


    private void FixedUpdate()
    {

        if (!canMotion)
            return;

        transform.position += new Vector3(0f, 0f, 1f) * Time.deltaTime * velocityOfPlayer;
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.name == "FinalZone")
        {
            Debug.Log($"OnTriggerEnter{other.gameObject.name}");
            AccessEndPoint();
        }
    }

    public void AccessEndPoint()
    {
        DOTween.To(() => velocityOfPlayer, x => velocityOfPlayer = x, 0, 1f)
            .OnUpdate(() => {
                Debug.Log("DotweenUpdate");

            }).OnComplete(() => {

                Debug.Log("Oncomplete");
                canMotion = false;

                Effect.gameObject.SetActive(true);

            });


        
    }

    IEnumerator DecreaseSpeedOfPlayer()
    {
        var yieldReturn = new WaitForEndOfFrame();

        while (true)
        {

        }
    }
}
