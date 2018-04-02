using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    private AudioSource _AudioSource;

    public float range = 100f;// bullet range (maximum)
    public int bulletsPerMag = 30;// bullets per mag
    public int bulletsLeft = 200;// total bullets

    public int currentBullets;// current bulletss in mag

    public Transform ShootPoint;// current bullet leaves gun muzzle
    public float fireRate = 0.1f;//time delay between shots
    public ParticleSystem muzzleFlash;//muzzleflash
    public AudioClip ShootSound;

    float fireTimer;

	// Use this for initialization
	void Start () {

        currentBullets = bulletsPerMag; // asign bullets on start

        _AudioSource = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButton("Fire1"))
        {
            if (currentBullets > 0)
                Fire();//call fire function by presing m1
            else
                Reload();
        }
        if (fireTimer < fireRate)
            fireTimer += Time.deltaTime;
	}
    private void Fire()
    {
        if (fireTimer < fireRate || currentBullets <= 0)// check can we shoot
            return;
        

        RaycastHit hit;
        if(Physics.Raycast(ShootPoint.position, ShootPoint.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name + "found");// hit detect
        }

        muzzleFlash.Play();
        PlayShootSound();

        currentBullets--;// reduce bullet count by one

        fireTimer = 0.0f; // reset fire timer
    }

    private void Reload()
    {
        if (bulletsLeft <= 0) return;

        int bulletsToLoad = bulletsPerMag - currentBullets;
        int bulletsToDeduct = (bulletsLeft >= bulletsToLoad) ? bulletsToLoad : bulletsLeft;
        bulletsLeft -= bulletsToDeduct;
        currentBullets += bulletsToDeduct;
    }


    private void PlayShootSound()
    {
        _AudioSource.clip = ShootSound;
        _AudioSource.Play();
    }
}
