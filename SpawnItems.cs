using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Created by Tobi @Alive Studios, free to use in any project :)
/// </summary>

public class SpawnItems : MonoBehaviour
{
    [Tooltip("This is the powerup / object that will be spawned")]
    public GameObject item;


    [Tooltip("What tag do obstacles have (Items will not spawn inside of Obstacles)")]
    //Note: This could also be done with layers or components or any other way of comparison. Just change the if statement in the
    //TestForObstacle method.
    public String obstacleObjectTag;


    [Tooltip("How many powerups should be there at once? IMPORTANT: too many items on too less space will lead to Stackoverflow exception")]
    public int itemAmount = 4;

    [Tooltip("What should be the minimum distance between obstacles and powerups?")]
    public float minDistance = .1f;

    List<GameObject> spawnedBallons = new List<GameObject>();
    List<GameObject> destroyedBallons = new List<GameObject>();

    [Tooltip("What is the origin / place the powerups should be spawned around")]
    public GameObject origin;


    [Tooltip("Define the Range around the origin")]
    public float xRange;
    public float yRange;
    public float zRange;

    Vector3 rangeVector; 

    private void Start()
    {
        //Spawn inital Powerups
        for (int i = 0; i < itemAmount; i++)
        {
            SpawnItem();
        }

        //Subscribe to ItemManager in Case Item is destroyed 
        ItemManager.onCollision += DestroyItem;

    }


    //Spawn a new Item on a random position within range and not inside obstacles 
    void SpawnItem()
    {
        List<GameObject> hitObjects =  TestForObstacle(origin.transform.position);


        GameObject bal;
        Vector3 vec = randomVector(origin.transform, hitObjects);
        bal = Instantiate(item, vec, Quaternion.identity);
    }

    Vector3 randomVector(Transform origin, List<GameObject> Obstacles) {
        Vector3 ranVec = new Vector3(
           UnityEngine.Random.Range(origin.transform.position.x - xRange  , origin.transform.position.x +  xRange),
           UnityEngine.Random.Range(origin.transform.position.y - yRange   , origin.transform.position.y + yRange),
           UnityEngine.Random.Range(origin.transform.position.z - zRange  , origin.transform.position.z + zRange)
            );

        if (Obstacles != null || Obstacles.Count > 0) {
            foreach (GameObject obstac in Obstacles)
            {
                if (Vector3.Distance(ranVec, obstac.transform.position) < .5f)
                {
                    randomVector(origin, Obstacles);
                    break; 
                }
            }
        }
        return ranVec;
    }


    //Vizualize the spawn range 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(origin.transform.position, new Vector3(xRange*2,yRange*2,zRange*2));
    }


    //Destroy a item and spawn a new one
    void DestroyItem(GameObject callBackObject)
    {
        spawnedBallons.Remove(callBackObject);
        destroyedBallons.Add(callBackObject);
        Destroy(callBackObject);
        SpawnItem();
    }


    //Test for an Obstacle 
    List<GameObject> TestForObstacle(Vector3 goj) {
        rangeVector = new Vector3(xRange/2, yRange/2, zRange/2);
        RaycastHit[] hits;
        List<GameObject> hitGojs = new List<GameObject>(); 

        hits = Physics.SphereCastAll(goj - rangeVector, zRange, Vector3.forward);

        foreach (RaycastHit ht in hits) {

            if (ht.transform.gameObject.tag == obstacleObjectTag) {
                hitGojs.Add(ht.transform.gameObject);
            }
        }
        return hitGojs; 
    }
}
