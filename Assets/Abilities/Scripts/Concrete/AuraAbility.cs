using System.Collections.Generic;
using UnityEngine;

public class AuraAbility : Ability
{
    [Header("Aura Settings")]
    public float damageInterval = 1f;
    public int damagePerTick = 5;
    public GameObject auraEffectPrefab;

    [SerializeField] private SphereCollider auraCollider;
    private float nextDamageTime;
    private List<Enemy> enemiesInAura = new List<Enemy>();
    private GameObject auraEffectInstance;
    private ParticleSystem _auraParticles;


    public override void Effect()
    {
        if (auraEffectPrefab != null && auraEffectInstance == null)
        {
            Debug.Log("Aura Activada");
            auraEffectInstance = Instantiate(auraEffectPrefab, transform.position, auraEffectPrefab.transform.rotation, transform);

            _auraParticles = auraEffectInstance.GetComponent<ParticleSystem>();
            if (_auraParticles != null)
            {
                var shapeModule = _auraParticles.shape;
                shapeModule.radius = Range;
            }

            _auraParticles.Play();
        }

        if (Time.time >= nextDamageTime && enemiesInAura.Count > 0)
        {
            foreach (Enemy enemy in enemiesInAura)
            {
                if (enemy != null)
                    enemy.TakeDamage(damagePerTick);
            }
            nextDamageTime = Time.time + damageInterval;
        }

        nextDamageTime = Time.time + damageInterval;
    }

    void Start()
    {
        auraCollider.radius = Range;
        auraEffectInstance = null;
        _auraParticles = GetComponentInChildren<ParticleSystem>();
        UpdateAuraRadius(Range);
        OnRangeChanged += UpdateAuraRadius;
        OnRangeChanged += UpdateColliderRadius;
    }

    void Update()
    {
        Activate();
    }

    private void UpdateAuraRadius(float newRange)
    {
        if (_auraParticles != null)
        {
            var shape = _auraParticles.shape;
            shape.radius = newRange;
            var emission = _auraParticles.emission;
            emission.rateOverTime = newRange*15;
        }
    }
    private void UpdateColliderRadius(float newRange)
    {
        auraCollider.radius = newRange;
    }

    private void OnDestroy()
    {
        OnRangeChanged -= UpdateAuraRadius;
        OnRangeChanged -= UpdateColliderRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other != null && !enemiesInAura.Contains(other.GetComponent<Enemy>()))
            {
                enemiesInAura.Add(other.GetComponent<Enemy>());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemiesInAura.Contains(enemy))
                enemiesInAura.Remove(enemy);
        }
    }

    void OnDisable()
    {
        enemiesInAura.Clear();
        if (auraEffectInstance != null)
            Destroy(auraEffectInstance);
    }
}
