using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStackController : MonoBehaviour
{

    public List<GameObject> blockList = new List<GameObject>(); ///////

    GameObject lastBlock; ///////

    void Start()
    {
        UpdateLastBlock();    ////////
    }


    private void UpdateLastBlock()  //////////
    {
        lastBlock = blockList[blockList.Count - 1];
    }

    public void AddBlock(GameObject gameObject)  /////////
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.04f, transform.position.z);
        gameObject.transform.position = new Vector3(lastBlock.transform.position.x, lastBlock.transform.position.y - 0.04f, lastBlock.transform.position.z);
        gameObject.transform.SetParent(transform);
        blockList.Add(gameObject);
        UpdateLastBlock();
    }

    public void RemoveBlock(GameObject gameObject)
    {

        gameObject.transform.parent = null;
        blockList.Remove(gameObject);
        UpdateLastBlock();

    }
}
