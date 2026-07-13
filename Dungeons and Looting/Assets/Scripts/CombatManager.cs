using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public GameObject plr;
    public Dictionary<int, WeaponData> weapons = new Dictionary<int, WeaponData>();
    public GameObject[] weapons1;
    public GameObject equipped;
    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (equipped == null)
            {
                // Equip
                StateManager.instance.ChangeState(StateManager.GameState.HasEquipped1);

                WeaponData weapon = weapons[0];
                equipped = Instantiate(weapon.prefab, plr.transform);

                Debug.Log(weapon.damage);
                Debug.Log(weapon.attackSpeed);
            }
            else
            {
                // Unequip
                Destroy(equipped);
                equipped = null;

                StateManager.instance.ChangeState(StateManager.GameState.HasEquipped2); // or your unequipped state
            }
        }
    
        
        
        
    }
}
