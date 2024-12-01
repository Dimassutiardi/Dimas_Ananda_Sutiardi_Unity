using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

public class Weapon : MonoBehaviour
{   
    [Header("Weapon Stats")]
    [SerializeField] public float shootIntervalInSeconds = 3f; // Change to public
    [SerializeField] private bool isEnemyBossWeapon; // Add this line

    [Header("Bullets")]
    public Bullet bullet;
    [SerializeField] private Transform bulletSpawnPoint;


    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;


    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer;
    public Transform parentTransform;

    void Awake(){
        objectPool = new ObjectPool<Bullet>(
            CreateBulletItem, 
            OnGetFromPool, 
            OnReleaseToPool, 
            OnDestroyPoolObject, 
            collectionCheck, 
            defaultCapacity, 
            maxSize
        );
    }    
    void Update()
    {
        timer += Time.deltaTime;
        float interval =  shootIntervalInSeconds; // Adjust the intervalot
        if (timer >= interval)
        {
            timer = 0f;
            Bullet bulletInstance = objectPool.Get();
            // No need to start the coroutine here
        }
    }

    public void Fire(Vector3 position, Quaternion rotation)
    {
        float interval =  shootIntervalInSeconds; // Adjust the interval
        if (timer >= interval)
        {
            timer = 0f;
            Bullet bulletInstance = objectPool.Get();
            bulletInstance.transform.position = position;
            bulletInstance.transform.rotation = rotation;
            bulletInstance.Initialize();
        }
    }

    Bullet CreateBulletItem()
    {
        Bullet bulletInstance = Instantiate(bullet);
        bulletInstance.ObjectPool = objectPool; 
        return bulletInstance;
    }

    void OnGetFromPool(Bullet bullet)
    {
        bullet.ObjectPool = objectPool; // Ensure the ObjectPool property is set
        bullet.gameObject.SetActive(true);
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;
        bullet.Initialize(); 
    }

    void OnReleaseToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    void OnDestroyPoolObject(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}