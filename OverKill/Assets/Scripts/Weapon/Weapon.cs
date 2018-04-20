using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Weapon : MonoBehaviour {

    private AudioSource _AudioSource;

    public float range = 100f;// bullet range (maximum)
    public int bulletsPerMag = 35;// bullets per mag
    public int bulletsLeft = 500;// total bullets

    public int currentBullets;// current bulletss in mag
    public Text ammoText;

    public Transform ShootPoint;// current bullet leaves gun muzzle
    public GameObject hitParticles;
    public GameObject bulletImpact;
    public float fireRate = 0.1f;//time delay between shots
    public ParticleSystem muzzleFlash;//muzzleflash
    public AudioClip ShootSound;

    public float damage = 25f;

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
            else if(bulletsLeft > 0)
                Reload();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentBullets < bulletsPerMag && bulletsLeft > 0)
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
            GameObject hitParticleEffect = Instantiate(hitParticles, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            GameObject bulletHole = Instantiate(bulletImpact, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));

            Destroy(hitParticleEffect, 1f);
            Destroy(bulletHole, 2f);

            if (hit.transform.GetComponent<HealthController>())
            {
                hit.transform.GetComponent<HealthController>().ApplyDamage(damage);
            }
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
       //AudioSource.PlayOneShot(ShootSound);
       _AudioSource.clip = ShootSound;
       _AudioSource.Play();
    }
    
}
