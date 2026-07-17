using UnityEngine;
using UnityEngine.UI;
public class HealthBarFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, -1000f, 0f);
    public EnemyData enemy;

    public Image fillBar;
    public Image outerBar;

    float maxHealth; 
    void Awake()
    {
        fillBar = transform.Find("HPInner").GetComponent<Image>();
        outerBar = transform.Find("HPOuter").GetComponent<Image>();
    }
    void Start()
    {
        outerBar.rectTransform.sizeDelta = new Vector2(enemy.enemyHealth, 10);
        fillBar.rectTransform.sizeDelta = new Vector2(enemy.enemyHealth, 10);
        maxHealth = enemy.enemyHealth;

    }
    void LateUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = target.position + offset;
    }

    void Update()
    {
        //fillBar.rectTransform.sizeDelta = new Vector2(enemy.enemyHealth, 10);
        fillBar.fillAmount = enemy.enemyHealth / maxHealth;
    }
}