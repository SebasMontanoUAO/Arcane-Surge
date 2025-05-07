using System;
using UnityEngine;

public class ArrowAbility : Ability
{

    [Header("Arrow Settings")]
    public GameObject arrowPrefab;
    public int damage = 10;

    public override void Effect()
    {
        GameObject nearestEnemy = FindNearestEnemy();

        if (nearestEnemy != null)
        {
            Vector3 direction = (nearestEnemy.transform.position - transform.position).normalized;

            GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.LookRotation(direction));
            
            arrow.transform.LookAt(nearestEnemy.transform);

            arrow.GetComponent<ArrowProjectile>().SetDamage(damage);
        }
    }

    private GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= range && distance < minDistance)
            {
                minDistance = distance;
                nearest = enemy;
            }
        }
        return nearest;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Activate();
    }
}
