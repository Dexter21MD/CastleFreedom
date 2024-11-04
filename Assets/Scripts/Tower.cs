using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int towerDamage = 5;
    [SerializeField] float rotateSpeed;
    [SerializeField] float timeRotation = 0.1f;
    [SerializeField] float detectionTime = 0.5f;
    [SerializeField] PlayerEnemyMarks identityMark;

    [SerializeField] GameObject towerTop;
    [SerializeField] Bullet bullet;
    [SerializeField] ParticleSystem fireSmoke;
    [SerializeField] Transform bulletTransform;

    Transform target;
    Animator animator;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        StartCoroutine(RotateCanon());
    }


    IEnumerator OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Warrior>().Alive && identityMark != other.GetComponent<WarriorAI>().Mark)
        {
            target = other.transform;
            animator.SetTrigger("shoot");
            yield return new WaitForSeconds(detectionTime);
        }
    }

    public PlayerEnemyMarks GetMark
    {
        get
        {
            return identityMark;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        target = null;
    }

    IEnumerator RotateCanon()
    {
        while (true)
        {
            LookAtEnemy();
            yield return new WaitForSeconds(timeRotation);
        }
    }
    private void LookAtEnemy()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - towerTop.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            towerTop.transform.rotation = Quaternion.Slerp(towerTop.transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
        }
    }
    public void Shoot()
    {
        if (target != null)
        {
            fireSmoke.Play();
            Bullet newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            newBullet.transform.parent = bulletTransform;
        }
    }

    public Vector3 GetTargetPos() { return target.position; }

    public int TowerDamage 
    {
        get
        {
            return towerDamage;
        }
        set
        {
            towerDamage = value;
        }
    }
}
