using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonAura : Ability
{
    [Header("Poison Aura Settings")]
    public float damagePerTick = 2f;
    public float tickInterval = 1f;
    public float duration = 5f;
    public GameObject auraVFX;

    private List<Enemy> affectedEnemies = new List<Enemy>();

    public override void Effect()
    {
        if (auraVFX != null)
        {
            GameObject vfxInstance = Instantiate(auraVFX, transform.position, Quaternion.identity);
            Destroy(vfxInstance, duration);
        }
        
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        foreach (var collider in hitColliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null && !affectedEnemies.Contains(enemy))
            {
                affectedEnemies.Add(enemy);
                StartCoroutine(ApplyPoison(enemy));
            }
        }
    }

    private IEnumerator ApplyPoison(Enemy enemy)
    {
        float endTime = Time.time + duration;

        while (Time.time < endTime && enemy != null)
        {
            enemy.TakeDamage(damagePerTick);
            yield return new WaitForSeconds(tickInterval);
        }

        affectedEnemies.Remove(enemy);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
