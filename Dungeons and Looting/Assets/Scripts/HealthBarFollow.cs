using UnityEngine;
using UnityEngine.UI;
public class HealthBarFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, -100f, 0f);
    public EnemyData enemy;

    public Image fillBar;
    public Image outerBar;

    void Awake()
    {
        fillBar = transform.Find("HPInner").GetComponent<Image>();
        outerBar = transform.Find("HPOuter").GetComponent<Image>();
    }
    void Start()
    {
        outerBar.rectTransform.sizeDelta = new Vector2(enemy.enemyHealth, 10);

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
        fillBar.rectTransform.sizeDelta = new Vector2(enemy.enemyHealth, 10);
    }
}