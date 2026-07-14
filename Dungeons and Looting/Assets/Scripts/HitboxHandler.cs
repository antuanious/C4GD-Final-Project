using System;
using System.Reflection;
using UnityEngine;
// I DONT HAVE A DANG CLUE HOW THIS WORKS DONT ASK ME
public class HitboxHandler : MonoBehaviour
{
    private const float DefaultDamage = 1f;

    private static readonly string[] DamageFieldNames =
    {
        "damage",
        "Damage",
        "attackDamage",
        "AttackDamage"
    };

    private bool hasHitPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || hasHitPlayer)
            return;

        hasHitPlayer = true;

        float damage = FindDamageValue();

        TryInvokeTakeDamage(collision.gameObject, damage);
    }

    private float FindDamageValue()
    {
        Component[] components = GetComponentsInParent<Component>(true);

        foreach (Component component in components)
        {
            if (component == null)
                continue;

            Type componentType = component.GetType();

            BindingFlags flags =
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.FlattenHierarchy;

            foreach (string fieldName in DamageFieldNames)
            {
                FieldInfo field = componentType.GetField(fieldName, flags);

                if (field == null)
                    continue;

                object value = field.GetValue(component);

                if (TryConvertToFloat(value, out float damage))
                    return damage;
            }

            foreach (string propertyName in DamageFieldNames)
            {
                PropertyInfo property =
                    componentType.GetProperty(propertyName, flags);

                if (property == null || property.GetGetMethod(true) == null)
                    continue;

                object value = property.GetValue(component);

                if (TryConvertToFloat(value, out float damage))
                    return damage;
            }
        }

        return DefaultDamage;
    }

    private static bool TryConvertToFloat(object value, out float result)
    {
        result = 0f;

        if (value == null)
            return false;

        try
        {
            result = Convert.ToSingle(value);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private static void TryInvokeTakeDamage(GameObject player, float damage)
    {
        Component[] components = player.GetComponents<Component>();

        foreach (Component component in components)
        {
            if (component == null)
                continue;

            MethodInfo method = component.GetType().GetMethod(
                "TakeDamage",
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic
            );

            if (method == null)
                continue;

            ParameterInfo[] parameters = method.GetParameters();

            if (parameters.Length != 1)
                continue;

            try
            {
                object convertedDamage =
                    Convert.ChangeType(damage, parameters[0].ParameterType);

                method.Invoke(component, new[] { convertedDamage });
                return;
            }
            catch
            {
                // Try the next component.
            }
        }

        Debug.LogWarning(
            $"No compatible TakeDamage method was found on {player.name}."
        );
    }

    private void OnDisable()
    {
        hasHitPlayer = false;
    }
}