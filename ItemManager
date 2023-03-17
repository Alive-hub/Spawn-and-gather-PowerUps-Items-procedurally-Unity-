using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Created by Tobi @Alive Studios, free to use in any project :)
/// </summary>

public class ItemManager : MonoBehaviour
{
    [Tooltip("This script only works if the item and colliding gameobject have a collider on it")]
    public static event Action<GameObject> onCollision;


    //If Collision happens, trigger the event 
    private void OnCollisionEnter(Collision collision)
    {
        onCollision?.Invoke(transform.gameObject);

    }
}
