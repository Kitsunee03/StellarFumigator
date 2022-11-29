using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitRed;

    private Vector3 target;
    private float lifeTime;
    private float m_bulletSpeed;
    private int m_damage;

    private void Start()
    {
        target = Utils.GetMouseWorldPosition();
        if (lifeTime == 0f) { lifeTime = 3f; }
        if (m_bulletSpeed == 0f) { m_bulletSpeed = 50f; }
        if (m_damage == 0) { m_damage = 50; }

        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        //Set Bullet Destination
        Vector3 dir = target - transform.position;
        float distanceThisFrame = m_bulletSpeed * Time.deltaTime;

        /*if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }*/

        //Move and rotate
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    private void HitTarget()
    {
        Instantiate(vfxHitRed, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            Instantiate(vfxHitRed, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}