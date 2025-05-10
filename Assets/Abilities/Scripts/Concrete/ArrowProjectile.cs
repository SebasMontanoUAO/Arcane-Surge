using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    private int damage;
    public float arrowSpeed = 10f;

    public void SetDamage(int amount) => damage = amount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * arrowSpeed * Time.deltaTime);
    }
}