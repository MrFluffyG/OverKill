using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthControler : MonoBehaviour {

	[SerializeField] private float health = 100f;


    public void ApplyDamageP(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {


            transform.position = new Vector3(1049, -4.35f, 1);
        }
    }
}
