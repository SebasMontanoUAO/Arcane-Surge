using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private Image healthBarFill;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Instance.healthBarFill = healthBarFill;
            Instance.UpdateHealthBar();
            Destroy(gameObject);
            return;
        }
    }

    public void UpdateHealthBar()
    {
        if (healthBarFill != null && Player.Instance != null)
        {
            float fillAmount = Player.Instance.CurrentHealth / Player.Instance.MaxHealth;
            healthBarFill.fillAmount = fillAmount;
        }
    }
}