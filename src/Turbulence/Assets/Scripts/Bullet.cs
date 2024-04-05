using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Stats stats = collision.gameObject.GetComponent<Stats>();
        if (stats != null)
        {
            stats.statDictionary["hp"].CurValue -= Damage;
        }
        Destroy(gameObject);
    }
}
