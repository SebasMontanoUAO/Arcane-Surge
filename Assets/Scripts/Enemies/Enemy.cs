using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float health = 15;
    [SerializeField] private Camera mainCamera;
    public float hitStunDuration = 0.2f; // Tiempo que se queda quieto al recibir daño
    public Color hitColor = Color.white; // Color de resaltado

    [Header("Damage Popup")]
    public TextMeshProUGUI damagePopup;

    [Header("Attack Settings")]
    [SerializeField] private float attackDamage = 5f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackCooldown = 1f;
    private float lastAttackTime;

    public event Action OnDeath;

    private Rigidbody rb;
    private bool isPaused = false;
    private SkinnedMeshRenderer meshRenderer;
    private Color originalColor;
    private Coroutine hitEffectCoroutine;
    private EnemyFollow movement;

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        if(meshRenderer != null) originalColor = meshRenderer.material.color;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movement = GetComponent<EnemyFollow>();
    }

    void Update()
    {
        if (!isPaused && Player.Instance != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);

            if (distanceToPlayer <= attackRange && Time.time > lastAttackTime + attackCooldown)
            {
                AttackPlayer();
                lastAttackTime = Time.time;
            }
        }
    }

    private void AttackPlayer()
    {
        Player.Instance?.TakeDamage(attackDamage);
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;

        StartCoroutine(ShowDamagePopup((int)Math.Round(dmg)));

        if(hitEffectCoroutine != null) StopCoroutine(hitEffectCoroutine);
        hitEffectCoroutine = StartCoroutine(HitEffect());

        if(health <= 0)
        {
            Die();
        }
    }

    IEnumerator HitEffect()
    {
        if (rb != null)
        {
            Vector3 knockbackDir = (Player.Instance.transform.position - transform.position).normalized;
            rb.AddForce(knockbackDir * 2f, ForceMode.Impulse);
        }
        if (meshRenderer != null) meshRenderer.material.color = hitColor;

        SetPaused(true);
        movement.enemy.speed = 0;

        yield return new WaitForSeconds(hitStunDuration);

        if (meshRenderer != null) meshRenderer.material.color = originalColor;
        SetPaused(false);
        movement.enemy.speed = 3.5f;
    }

    IEnumerator ShowDamagePopup(int damage)
    {
        if (damagePopup != null)
        {
            damagePopup.text = damage.ToString();
            damagePopup.enabled = true;

            yield return new WaitForSeconds(1);

            damagePopup.enabled = false;
        }
    }

    public void SetPaused(bool paused)
    {
        isPaused = paused;
        rb.isKinematic = paused;
        GetComponent<Animator>().enabled = !paused;
    }

    private void Die()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
