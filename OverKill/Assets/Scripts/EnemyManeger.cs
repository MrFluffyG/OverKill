using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManeger : MonoBehaviour {

    public static EnemyManeger instance;
    private void Awake()
    {
        instance = this;

    }

    public GameObject enemy;
}
