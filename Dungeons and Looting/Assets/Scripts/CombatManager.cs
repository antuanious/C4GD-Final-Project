using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;
    public GameObject plr;
    public Dictionary<int, WeaponData> weapons = new Dictionary<int, WeaponData>();
    public GameObject[] weapons1;
    public GameObject equipped;
    public Transform weaponPivot;

    public bool attacking = false;
    public float swingAngle = 70f;

    public Vector2 rightHandOffset = new Vector2(0.25f, 0f);
    public Vector2 leftHandOffset = new Vector2(-0.25f, 0f);
    public SpriteRenderer playerSprite;
    public float currentWeaponDamage = 0f;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {


        weapons.Add(0, new WeaponData()
        {
            weaponName = "DefaultSword",
            prefab = weapons1[0],
            damage = 25,
            attackSpeed = 0.4f,
            range = 1.5f,
        });


    }

    void Update()
    {
        // Equip / Unequip
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (equipped == null)
            {
                StateManager.instance.ChangeState(StateManager.GameState.HasEquipped1);

                WeaponData weapon = weapons[0];
                equipped = Instantiate(weapon.prefab, weaponPivot);

                equipped.transform.localPosition = Vector3.zero;
                equipped.transform.localRotation = Quaternion.identity;

                currentWeaponDamage = weapon.damage;
                //Debug.Log(weapon.damage);
                //Debug.Log(weapon.attackSpeed);
            }
            else
            {
                Destroy(equipped);
                equipped = null;

                StateManager.instance.ChangeState(StateManager.GameState.HasEquipped2);
            }
        }

        if (equipped == null)
            return;

        // Attack
        if (Input.GetMouseButtonDown(0) && !attacking)
        {
            Attack();
        }

        // Aim weapon when not attacking
        if (!attacking)
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = 0f;

            bool facingRight = mouse.x >= plr.transform.position.x;

            weaponPivot.localPosition = facingRight
                ? rightHandOffset
                : leftHandOffset;

            playerSprite.flipX = !facingRight;

            Vector2 dir =
                ((Vector2)mouse - (Vector2)weaponPivot.position).normalized;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            float rotationOffset = -90f;

            weaponPivot.rotation = Quaternion.Euler(
                0f,
                0f,
                angle + rotationOffset
            );
        }
    }

    public void Attack()
    {
        StartCoroutine(Swing());
    }

    IEnumerator Swing()
    {
        attacking = true;

        WeaponData weapon = weapons[0];

        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0f;

        bool facingRight = mouse.x >= plr.transform.position.x;

        weaponPivot.localPosition = facingRight
            ? rightHandOffset
            : leftHandOffset;

        playerSprite.flipX = !facingRight;

        Vector2 dir =
            ((Vector2)mouse - (Vector2)weaponPivot.position).normalized;

        float baseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float rotationOffset = -90f;

        float timer = 0f;

        while (timer < weapon.attackSpeed)
        {
            timer += Time.deltaTime;

            float progress =
                Mathf.Clamp01(timer / weapon.attackSpeed);

            progress = Mathf.SmoothStep(0f, 1f, progress);

            float offset;

            if (facingRight)
            {
                // Starts above and swings downward
                offset = Mathf.Lerp(
                    swingAngle,
                    -swingAngle,
                    progress
                );
            }
            else
            {
                // Mirrored downward attack
                offset = Mathf.Lerp(
                    -swingAngle,
                    swingAngle,
                    progress
                );
            }

            weaponPivot.rotation = Quaternion.Euler(
                0f,
                0f,
                baseAngle + rotationOffset + offset
            );

            yield return null;
        }

        weaponPivot.rotation = Quaternion.Euler(
            0f,
            0f,
            baseAngle + rotationOffset
        );

        attacking = false;
    }

}