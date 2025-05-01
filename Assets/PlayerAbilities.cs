using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public List<Ability> activeAbilities = new List<Ability>();
    [SerializeField] private AutoArrowAbility autoArrowAbility;

    // Llamado cuando el jugador recoge un pickup
    public void AddAbility(Ability newAbility)
    {
        Ability instance = gameObject.AddComponent(newAbility.GetType()) as Ability;
        activeAbilities.Add(instance);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<PlayerAbilities>().activeAbilities.Add(autoArrowAbility);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
