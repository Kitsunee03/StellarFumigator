using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{

    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitRed;
    private float lifeTime;
    private float m_bulletSpeed;

    private Rigidbody bulletRigidbody;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (lifeTime == 0f) { lifeTime = 3f; }
        if (m_bulletSpeed == 0f) { m_bulletSpeed = 50f; }
        
        bulletRigidbody.velocity = transform.forward * m_bulletSpeed;
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { return; }

        if (other.CompareTag("Enemy"))
        {
            Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(vfxHitRed, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}