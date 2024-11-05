using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private BulletsPool bulletsPool;
    private List<BulletController> bullets = new List<BulletController>();
    [Header("Gun parts")]
    [SerializeField]
    private Transform firePoint;
    [Header("Shooting")]
    private float shootTimer;
    private bool canShoot;
    [SerializeField, Range(0, 3)]
    private int fireRate = 10;
    private float timeBetweenShots = 0.5f;
    private float nextTimeToFire = 0;
    [SerializeField, Range(10, 100)]
    private float shootSpeed = 25f;

    public Camera playerCamera;
    [SerializeField]
    private LayerMask raycastLayerMask;


    void Start()
    {
        //canShoot = false;
        ResetTimer();
    }

    
    void Update()
    {
        ShootTimer();

        if (CheckFireInput() && shootTimer <= 0)
            Shoot();
    }
    private bool CheckFireInput()
    {
        if(Input.GetButton("Fire1"))
        { return true; }
        return false;
    }
    //Выстрел
    void Shoot()
    {
        ResetTimer();

        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit, maxDistance:50f, raycastLayerMask))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(10);
        }
        CheckDistance(ref targetPoint, 1f);
        Debug.DrawLine(firePoint.position, targetPoint, Color.white, 2f);

        Vector3 direction = Vector3.Normalize(targetPoint - firePoint.position);
        RevisedProjectile spawnedBullet = bulletsPool.
            SpawnFromPool(firePoint.position, Quaternion.LookRotation(direction));
        
        BulletController bullet = spawnedBullet.GetComponent<BulletController>();
        bullet.Launch(direction);
    }
    private Vector3 CheckDistance(ref Vector3 targetVec, float closeDistance)
    {
        // Проверка на близость к стене
        if (Vector3.Distance(firePoint.position, targetVec) < closeDistance)
        {
            // Если слишком близко к стене, измените targetPoint
            targetVec = firePoint.position + firePoint.forward * 1.0f;
        }
        return targetVec;
    }
    void ResetTimer()
    {
        shootTimer = timeBetweenShots;
    }
    void ShootTimer()
    {
        shootTimer -= Time.deltaTime;
    }
}
