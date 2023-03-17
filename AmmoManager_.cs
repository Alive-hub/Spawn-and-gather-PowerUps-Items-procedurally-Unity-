using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Tobi @Alive Studios, free to use in any project :)
/// </summary>


//This is a simple Ammo Manager Method. It is just an example on how
//to increase the ammo count on collision with an item. 


public class AmmoManager_ : MonoBehaviour
{
    public int ammo;

    public int Ammo
    {
        get { return ammo; }
    }

    void Start()
    {
        ammo = 100;
        ItemManager.onCollision += AmmoCollected;

    }

    void AmmoCollected(GameObject callBack)
    {
        IncreaseAmmo(10);
    }

    public void IncreaseAmmo(int amount)
    {
        ammo += amount;
    }

    public void DecreaseAmmo(int amount)
    {
        ammo -= amount;
    }
}
