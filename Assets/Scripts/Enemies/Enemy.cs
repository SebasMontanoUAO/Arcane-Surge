using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float health = 15;
    public float hitStunDuration = 0.2f; // Tiempo que se queda quieto al recibir daño
    public Color hitColor = Color.white; // Color de resaltado

    [Header("Damage Popup")]
    public GameObject damagePopupPrefab;

    public event Action OnDeath;

    private Rigidbody rb;
    private bool isPaused = false;
    private MeshRenderer meshRenderer;
    private Color originalColor;
    private Coroutine hitEffectCoroutine;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if(meshRenderer != null) originalColor = meshRenderer.material.color;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;

        if(health <= 0)
        {
            Die();
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
