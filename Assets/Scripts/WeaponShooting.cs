using System.Collections;
using UnityEngine;

public class WeaponShooting : MonoBehaviour
{
    #region Variables
    [Header("General")]
    [SerializeField] Camera mainCamera;
    float TimeBetweenShots;
    Animator weaponAnimation;
    public Movement player;
    float EmptyReloadTime = 1.25f;
    float Reloadtime = 1f;
    bool isReloading = false;

    [Header("Weapon Settings")]
    [SerializeField] float FireRate = 10f;
    [SerializeField] float damage = 10f; 
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] ParticleSystem bulletEffect;
    [SerializeField] GameObject crossHair;

    [Header("Ammo Manager")]
    [SerializeField] int AmmoCount = 35;
    [SerializeField] int TotalAvailableAmmo = 150;
    [SerializeField] int MaxAmmo = 35;
    [SerializeField] int Ammo;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        weaponAnimation = gameObject.GetComponent<Animator>();
        weaponAnimation.SetInteger("Movement", 0);
    }

    // Update is called once per frame
    void Update()
    {
        float xMov = Input.GetAxis("Horizontal");
        float zMov = Input.GetAxis("Vertical");

        if (Time.time >= TimeBetweenShots)
        {
            if(xMov == 0 && zMov == 0)
            {
                weaponAnimation.SetInteger("Fire", -1);
                weaponAnimation.SetInteger("Movement", 0);
            }
            else
            {
                if (player.isWalking == false)
                {
                    weaponAnimation.SetInteger("Fire", -1);
                    weaponAnimation.SetInteger("Movement", 2);
                }
                else
                {
                    weaponAnimation.SetInteger("Fire", -1);
                    weaponAnimation.SetInteger("Movement", 1);
                }
            }
            
                   
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            weaponAnimation.SetBool("Sight", true);
            crossHair.SetActive(false);
        }
        else
        {
            weaponAnimation.SetBool("Sight", false);
            crossHair.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.R) && AmmoCount <= MaxAmmo)
        {
            Ammo = MaxAmmo - AmmoCount;
            StartCoroutine(Reload());
            return;
        }

        if (AmmoCount <= 0)
        {
            StartCoroutine(EmptyReload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= TimeBetweenShots)
        {
            TimeBetweenShots = Time.time + 1f / FireRate;
            Shoot();
            weaponAnimation.SetInteger("Fire", 2);
            weaponAnimation.SetInteger("Movement", -1);
        }

    }

    private void Shoot()
    {
        
        if(isReloading != true)
        {
            AmmoCount--;
            muzzleFlash.Play();
            bulletEffect.Play();
        }
        
        
        RaycastHit hit;
        bool hasHit = Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit);
        if (hasHit)
        {
            Damage damageDealer = hit.transform.GetComponent<Damage>();
            if (damageDealer)
            {
                damageDealer.DamageDealer(damage);
            }
        }
    }

    public IEnumerator EmptyReload()
    {
        isReloading = true;
        weaponAnimation.SetInteger("Reload", 0);
        AmmoCount = MaxAmmo;
        TotalAvailableAmmo -= MaxAmmo;

        yield return new WaitForSeconds(EmptyReloadTime);

        isReloading = false;
        weaponAnimation.SetInteger("Reload", -1);
    }

    public IEnumerator Reload()
    {
        isReloading = true;
        weaponAnimation.SetInteger("Reload", 1);
        AmmoCount = MaxAmmo;
        TotalAvailableAmmo -= Ammo;

        yield return new WaitForSeconds(Reloadtime);

        isReloading = false;
        weaponAnimation.SetInteger("Reload", -1);
        weaponAnimation.SetInteger("Movement", 0);
    }

}
