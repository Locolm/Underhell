using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public GameObject self;

    public void TakeDamage(int damage)
    {
        hp= hp-damage;
        if (hp<=0) {
            Destroy(self);
        }

    }
}
