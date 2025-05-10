using System;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public event Action<float> OnRangeChanged;

    [Header("Base Ability Settings")]
    public float cooldown = 2f;
    public string abilityName = "Default Ability";
    [SerializeField] private float _range;

    protected float nextActivationTime = 0f;

    public abstract void Effect();

    public bool CanActivate()
    {
        return Time.time >= nextActivationTime;
    }

    public void Activate()
    {
        if (CanActivate())
        {
            Effect();
            nextActivationTime = Time.time + cooldown;
            //Debug.Log($"{abilityName} ha sido activada");
        }
    }
    public float Range
    {
        get => _range;
        set
        {
            if (_range != value)
            {
                _range = value;
                OnRangeChanged?.Invoke(_range);
            }
        }
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
