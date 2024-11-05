using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private GameObject particlesPrefab;
    [SerializeField, Range(1, 100)]
    private float bulletSpeed;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    } 
    
    public void Launch(Vector3 shootDirection)
    {
        MakeEnable();
        rb.velocity = shootDirection * bulletSpeed;
    }
   
    private void OnCollisionEnter(Collision collision)
    {
        ResetBullet();
    }
    public void ResetBullet()
    {
        rb.velocity = Vector3.zero;
        transform.position = Vector3.one * 1000;
        MakeDisable();
    }
    public void MakeEnable()
    {
        gameObject.SetActive(true);
    }
    public void MakeDisable()
    {
        gameObject.SetActive(false);
    }


    //При прикосновении пули
    private void OnTriggerEnter(Collider other)
    {      
        if (other.gameObject.CompareTag("Enemy"))
        {
            //EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            //if(enemy.TryGetComponent(out IDamagable damagable))
            //{
            //    damagable.ApplyDamage(bulletDamage);

            //    GameObject partsClone = Instantiate(particlesPrefab,transform.position, 
            //        Quaternion.identity);
            //    ParticleSystem parts = partsClone.GetComponent<ParticleSystem>();
            //    float totalDuration = parts.main.duration + parts.main.startLifetime.constantMax;
            //    Destroy(partsClone, totalDuration);               
            //}                     
            //ResetBullet();
        }
    }

   

}
