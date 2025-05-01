using System;
using UnityEngine;

public class AutoArrowAbility : Ability
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float damage = 10f;

    protected override void Effect()
    {
        Enemy nearestEnemy = FindNearestEnemy();
        if (nearestEnemy != null)
        {
            Vector3 direction = (nearestEnemy.transform.position - transform.position).normalized;
            GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.LookRotation(direction));
            arrow.GetComponent<ArrowProjectile>().SetDamage(damage);
        }
    }

    
    private Enemy FindNearestEnemy()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        Enemy nearest = null;
        float minDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= range && distance < minDistance)
            {
                minDistance = distance;
                nearest = enemy;
            }
        }
        Debug.Log(nearest != null ? "Enemigo encontrado" : "No hay enemigos cerca");
        return nearest;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
