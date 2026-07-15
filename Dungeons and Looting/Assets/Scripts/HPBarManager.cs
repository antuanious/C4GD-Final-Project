using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform fill;

    private Vector3 originalScale;
    public EnemyData enemy;

    void Start()
    {
        originalScale = fill.localScale;
    }

    void Update()
    {
        float percent = enemy.enemyHealth/enemy.enemyMaxHealth;

        fill.localScale = new Vector3(
            originalScale.x * percent,
            originalScale.y,
            originalScale.z);
    }
}
