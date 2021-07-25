using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public Text healthText;
    private Vector2 startingScale;
    public bool isEnemy;

    void Awake()
    {
        startingScale = healthBar.rectTransform.sizeDelta;
        if (isEnemy)
        {
            GameManager.Instance.onEnemyDamage += UpdateHealthBar;
        }
        else
        {
            GameManager.Instance.onPlayerDamage += UpdateHealthBar;
        }
    }

    public void UpdateHealthBar()
    {
        float maxHealth = isEnemy ? GameManager.Instance.enemyMaxHealth : GameManager.Instance.playerMaxHealth;
        float currentHealth = isEnemy ? GameManager.Instance.enemyHealth : GameManager.Instance.playerHealth;
        float healthPCT = (currentHealth / maxHealth);
        healthBar.rectTransform.sizeDelta = new Vector2(startingScale.x * healthPCT, startingScale.y);
        healthText.text = $"{healthPCT * 100f:F0}%";
    }
}
