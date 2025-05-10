using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 15;

    public event Action OnDeath;

    private Rigidbody rb;
    private bool isPaused = false;

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
