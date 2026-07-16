using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestManager : MonoBehaviour
{
    public static ChestManager instance;
    public WeaponData weaponData;
    public bool hasOpenedThisChest = false;
    public GameObject plr;

    // Interaction settings
    public float interactRadius = 2f;
    public KeyCode interactKey = KeyCode.E;

    // Internal state
    private bool playerNearby = false;

    void Start()
    {
        instance = this;

        // Try to auto-find player if not assigned (expects player GameObject to have tag "Player")
        if (plr == null)
        {
            var found = GameObject.FindWithTag("Player");
            if (found != null)
                plr = found;
        }
    }

    void Update()
    {
        if (hasOpenedThisChest)
            return;

        // Keep trying to find player if it becomes available later
        if (plr == null)
        {
            var found = GameObject.FindWithTag("Player");
            if (found != null)
                plr = found;
        }

        if (plr == null)
            return;

        // Check distance to player
        float dist = Vector3.Distance(transform.position, plr.transform.position);
        playerNearby = dist <= interactRadius;

        if (playerNearby)
        {
            // Optional feedback -- replace with UI prompt if available
            Debug.Log("Press " + interactKey + " to open the chest.");

            if (Input.GetKeyDown(interactKey))
            {
                OpenChest();
                hasOpenedThisChest = true;
            }
        }
    }

    // Visualize interaction radius in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }

    // Public method to open chest and choose a drop from LootTable.weaponTable
    public void OpenChest()
    {
        string chosen = ChooseWeaponDrop();

        if (!string.IsNullOrEmpty(chosen))
        {
            Debug.Log($"Chest dropped: {chosen}");

            if (CombatManager.instance != null)
            {
                CombatManager.instance.CheckAndUpgradeWeapon(chosen);
            }
            else
            {
                Debug.LogWarning("CombatManager instance was not found.");
            }
        }
        else
        {
            Debug.Log("Chest dropped nothing or weapon table invalid.");
        }
    }

    // Chooses a weapon key from LootTable.weaponTable using weighted random selection.
    // Returns the chosen key or null if selection failed.
    private string ChooseWeaponDrop()
    {
        var table = LootTable.weaponTable;
        if (table == null || table.Count == 0)
            return null;

        // Compute total weight
        float total = 0f;
        foreach (var kvp in table)
        {
            if (kvp.Value > 0f)
                total += kvp.Value;
        }

        if (total <= 0f)
            return null;

        // Sample a random value in [0, total)
        float sample = Random.Range(0f, total);

        // Iterate and find the bucket
        float running = 0f;
        foreach (var kvp in table)
        {
            float weight = Mathf.Max(0f, kvp.Value);
            running += weight;
            if (sample < running)
            {
                return kvp.Key;
            }
        }

        // Fallback (shouldn't normally reach here)
        foreach (var kvp in table)
        {
            if (kvp.Value > 0f)
                return kvp.Key;
        }

        return null;
    }
}
