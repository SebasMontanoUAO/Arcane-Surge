using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [Header("Base Ability Settings")]
    [SerializeField] protected float cooldown = 2f;
    [SerializeField] protected float range = 5f;
    [SerializeField] protected string animationTrigger = "Cast";

    protected float nextReadyTime;
    protected Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextReadyTime)
        {
            Effect();
            nextReadyTime = Time.time + cooldown;

            if (animator != null)
                animator.SetTrigger(animationTrigger);
        }
    }

    protected abstract void Effect();

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
