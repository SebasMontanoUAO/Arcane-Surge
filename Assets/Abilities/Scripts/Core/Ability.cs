using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [Header("Base Ability Settings")]
    public float cooldown = 2f;
    public float range = 5f;
    public string abilityName = "Default Ability";

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
