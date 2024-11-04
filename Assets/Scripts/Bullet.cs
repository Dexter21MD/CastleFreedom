using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] ParticleSystem explosion;
    [SerializeField] float speed = 1f;
    [SerializeField] float foo = 0.2f;
    int damageToDeal;
    Collider collider;
    Tower tower;
    bool shooted = false;
    Vector3 target;

    private void Start()
    {
        collider = GetComponent<SphereCollider>();
        tower = GetComponentInParent<Tower>();
        target = tower.GetTargetPos();
        collider.enabled = false;
        damageToDeal = tower.TowerDamage;
    }

    private void Update()
    {
        //zoptymalizować
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed);

            float distance = Vector3.Distance(transform.position, target);
            if (distance <= 1.5f)
            {
                collider.enabled = true;
            }
        }
    }

    IEnumerator OnTriggerEnter(Collider other)
    {
        
        if (other.GetComponent<WarriorAI>().Mark != tower.GetMark && !shooted)
        {
            explosion.Play();
            shooted = true;
            other.GetComponent<Warrior>().Health -= damageToDeal;
            yield return new WaitForSeconds(5f);
            Destroy(gameObject);
        }
    }

}
