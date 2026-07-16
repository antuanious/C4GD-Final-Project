using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    // Start is called before the first frame update

    public static Dictionary<string, float> weaponTable = new Dictionary<string, float>();
    public GameObject[] weapons;
    void Start()
    {
        weaponTable[weapons[0].name] = 0f;      // Knight Sword (Starting Weapon)
        weaponTable[weapons[1].name] = 20f;     // Desert Dagger
        weaponTable[weapons[2].name] = 17f;     // Assassin Dagger
        weaponTable[weapons[3].name] = 15f;     // Bronze Longsword
        weaponTable[weapons[4].name] = 12f;     // Crimson Sword
        weaponTable[weapons[5].name] = 10f;     // Pirate Cutlass
        weaponTable[weapons[6].name] = 10f;     // Nomad Scimitar
        weaponTable[weapons[7].name] = 4f;      // Golden Saber
        weaponTable[weapons[8].name] = 4f;      // Serpent Blade
        weaponTable[weapons[9].name] = 3f;      // Iron Sword
        weaponTable[weapons[10].name] = 2f;     // Steel Longsword
        weaponTable[weapons[11].name] = 1.5f;   // Dark Katana
        weaponTable[weapons[12].name] = 1f;     // Crescent Saber
        weaponTable[weapons[13].name] = 0.4f;   // Royal Saber
        weaponTable[weapons[14].name] = 0.1f;   // Executioner's Greatsword
    }

}
