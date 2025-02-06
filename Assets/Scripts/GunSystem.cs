using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunSystem : MonoBehaviour
{
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloading;

    public Camera fpsCam;
	public Transform cameraTransform;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnnemy;
    
    public GameObject muzzleFlash, bulletHoleGraphic; 
    public AudioSource audioSource;
    public AudioClip gunShotSound;
    // public CamShake camShake;
    public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI text;
    

    public void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        myInput();
        
        text.SetText(bulletsLeft + " / " + magazineSize);
		transform.rotation = Quaternion.Euler(cameraTransform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
		attackPoint.rotation = transform.rotation;
    }
    
    private void myInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0 ) {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }
    
    private void Shoot()
    {
        readyToShoot = false;
        
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

		Debug.DrawRay(attackPoint.position, direction * range, Color.red, 0.01f);
        
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out rayHit, range, whatIsEnnemy))
        {
            Debug.Log("Hit : " + rayHit.collider.name);

            if (rayHit.collider.CompareTag("Ennemy"))
                rayHit.collider.GetComponent<ShootingAi>().TakeDamage(damage);

			Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        }
        // camShake.Shake(camShakeMagnitude, camShakeDuration);

       
        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        audioSource.PlayOneShot(gunShotSound);
        
        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShooting);
    }

    private void ResetShot()	
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
