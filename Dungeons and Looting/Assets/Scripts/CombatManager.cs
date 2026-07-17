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
    public int bestWeaponID = 0;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {


        weapons.Add(0, new WeaponData()
        {
            weaponName = "KnightSword",
            prefab = weapons1[0],
            damage = 25,
            attackSpeed = 0.8f,
            range = 1.5f,
        });

        weapons.Add(1, new WeaponData()
        {
            weaponName = "DesertDagger",
            prefab = weapons1[1],
            damage = 15,
            attackSpeed = 0.4f,
            range = 0.9f,
        });

        weapons.Add(2, new WeaponData()
        {
            weaponName = "AssassinDagger",
            prefab = weapons1[2],
            damage = 15,
            attackSpeed = 0.35f,
            range = 1.0f,
        });

        weapons.Add(3, new WeaponData()
        {
            weaponName = "BronzeLongsword",
            prefab = weapons1[3],
            damage = 28,
            attackSpeed = 0.75f,
            range = 1.5f,
        });

        weapons.Add(4, new WeaponData()
        {
            weaponName = "CrimsonSword",
            prefab = weapons1[4],
            damage = 34,
            attackSpeed = 0.7f,
            range = 1.6f,
        });

        weapons.Add(5, new WeaponData()
        {
            weaponName = "PirateCutlass",
            prefab = weapons1[5],
            damage = 32,
            attackSpeed = 0.55f,
            range = 1.4f,
        });

        weapons.Add(6, new WeaponData()
        {
            weaponName = "NomadScimitar",
            prefab = weapons1[6],
            damage = 34,
            attackSpeed = 0.6f,
            range = 1.7f,
        });

        weapons.Add(7, new WeaponData()
        {
            weaponName = "GoldenSaber",
            prefab = weapons1[7],
            damage = 50,
            attackSpeed = 0.65f,
            range = 1.6f,
        });

        weapons.Add(8, new WeaponData()
        {
            weaponName = "SerpentBlade",
            prefab = weapons1[8],
            damage = 50,
            attackSpeed = 0.7f,
            range = 1.8f,
        });

        weapons.Add(9, new WeaponData()
        {
            weaponName = "IronSword",
            prefab = weapons1[9],
            damage = 52,
            attackSpeed = 0.8f,
            range = 1.8f,
        });

        weapons.Add(10, new WeaponData()
        {
            weaponName = "SteelLongsword",
            prefab = weapons1[10],
            damage = 55,
            attackSpeed = 0.95f,
            range = 2.0f,
        });

        weapons.Add(11, new WeaponData()
        {
            weaponName = "DarkKatana",
            prefab = weapons1[11],
            damage = 49,
            attackSpeed = 0.45f,
            range = 1.7f,
        });

        weapons.Add(12, new WeaponData()
        {
            weaponName = "CrescentSaber",
            prefab = weapons1[12],
            damage = 50,
            attackSpeed = 0.6f,
            range = 1.9f,
        });

        weapons.Add(13, new WeaponData()
        {
            weaponName = "RoyalSaber",
            prefab = weapons1[13],
            damage = 55,
            attackSpeed = 0.65f,
            range = 1.8f,
        });

        weapons.Add(14, new WeaponData()
        {
            weaponName = "ExecutionersGreatsword",
            prefab = weapons1[14],
            damage = 125,
            attackSpeed = 2.0f,
            range = 2.4f,
        });


    }

    void Update()
    {
        // Equip / Unequip
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (equipped == null)
            {
                StateManager.instance.ChangeState(
                    StateManager.GameState.HasEquipped1
                );

                EquipBestWeapon();
            }
            else
            {
                Destroy(equipped);
                equipped = null;

                StateManager.instance.ChangeState(
                    StateManager.GameState.HasEquipped2
                );
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

            // Flip weapon orientation horizontally when facing right
            if (equipped != null)
            {
                Vector3 eqScale = equipped.transform.localScale;
                float baseX = Mathf.Abs(eqScale.x);
                eqScale.x = facingRight ? -baseX : baseX;
                equipped.transform.localScale = eqScale;
            }

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

        WeaponData weapon = weapons[bestWeaponID];

        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0f;

        bool facingRight = mouse.x >= plr.transform.position.x;

        weaponPivot.localPosition = facingRight
            ? rightHandOffset
            : leftHandOffset;

        playerSprite.flipX = !facingRight;

        // Ensure weapon flips during swing too
        if (equipped != null)
        {
            Vector3 eqScale = equipped.transform.localScale;
            float baseX = Mathf.Abs(eqScale.x);
            eqScale.x = facingRight ? -baseX : baseX;
            equipped.transform.localScale = eqScale;
        }

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
    public void CheckAndUpgradeWeapon(string newWeaponName)
    {
        int newWeaponID = -1;

        // Find the weapon ID using its name
        foreach (KeyValuePair<int, WeaponData> entry in weapons)
        {
            if (entry.Value.weaponName == newWeaponName)
            {
                newWeaponID = entry.Key;
                break;
            }
        }

        if (newWeaponID == -1)
        {
            Debug.LogWarning("Weapon not found: " + newWeaponName);
            return;
        }

        WeaponData currentWeapon = weapons[bestWeaponID];
        WeaponData newWeapon = weapons[newWeaponID];

        float currentDPS = currentWeapon.damage / currentWeapon.attackSpeed;
        float newDPS = newWeapon.damage / newWeapon.attackSpeed;

        if (newDPS > currentDPS)
        {
            bestWeaponID = newWeaponID;

            Debug.Log(
                $"Upgraded from {currentWeapon.weaponName} " +
                $"to {newWeapon.weaponName}. New weapon ID: {bestWeaponID}"
            );

            EquipBestWeapon();
        }
        else
        {
            Debug.Log(
                $"{newWeapon.weaponName} is not better than " +
                $"{currentWeapon.weaponName}."
            );
        }
    }


    public void EquipBestWeapon()
    {
        if (!weapons.ContainsKey(bestWeaponID))
        {
            Debug.LogWarning("Invalid bestWeaponID: " + bestWeaponID);
            return;
        }

        if (equipped != null)
        {
            Destroy(equipped);
        }

        WeaponData weapon = weapons[bestWeaponID];

        equipped = Instantiate(weapon.prefab, weaponPivot);

        equipped.transform.localPosition = Vector3.zero;
        equipped.transform.localRotation = Quaternion.identity;

        // Set initial orientation based on current mouse/player facing
        if (Camera.main != null && plr != null)
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = 0f;
            bool facingRight = mouse.x >= plr.transform.position.x;

            Vector3 eqScale = equipped.transform.localScale;
            float baseX = Mathf.Abs(eqScale.x);
            eqScale.x = facingRight ? -baseX : baseX;
            equipped.transform.localScale = eqScale;

            weaponPivot.localPosition = facingRight ? rightHandOffset : leftHandOffset;
            playerSprite.flipX = !facingRight;
        }

        currentWeaponDamage = weapon.damage;

        Debug.Log("Equipped: " + weapon.weaponName);
    }
}