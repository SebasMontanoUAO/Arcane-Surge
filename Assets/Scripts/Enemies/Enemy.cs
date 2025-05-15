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

    // Update is called once per frame
    void Update()
    {
        
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

        yield return new WaitForSeconds(hitStunDuration);

        if (meshRenderer != null) meshRenderer.material.color = originalColor;
        SetPaused(false);
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
