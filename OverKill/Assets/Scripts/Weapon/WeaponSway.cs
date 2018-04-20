using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour {
    public float amount;
    public float smoothAmount;
    public float maxAmount;

    private Vector3 initialPosition;
	// Use this for initialization
	void Start () {
        initialPosition = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        float movementX = -Input.GetAxis("Mouse X") * amount;
        float movementY = -Input.GetAxis("Mouse Y") * amount;
        movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
        movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

        Vector3 finalPosotion = new Vector3(movementX, movementY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosotion + initialPosition, Time.deltaTime * smoothAmount);
    }
}
