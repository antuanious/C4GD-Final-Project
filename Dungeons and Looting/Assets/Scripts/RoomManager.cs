using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class RoomSpawns : MonoBehaviour
{
    [Header("Room")]
    public GameObject room;

    [Header("Enemies")]
    public GameObject[] enemies;
    public int numToSpawn = 3;

    [Header("Spawn Settings")]
    public float edgePadding = 0.5f;

    private bool hasEnteredPreviously;
    public Canvas HealthBar;
    public void SpawnInRoom(GameObject targetRoom, GameObject enemy)
    {
        if (targetRoom == null || enemy == null)
        {
            Debug.LogWarning("SpawnInRoom was called with a null room or enemy.");
            return;
        }

        Bounds roomBounds = GetBounds(targetRoom);

        

        float minX = roomBounds.min.x + edgePadding;
        float maxX = roomBounds.max.x - edgePadding;

        float minY = roomBounds.min.y + edgePadding;
        float maxY = roomBounds.max.y - edgePadding;

        // Prevent invalid ranges when the room is very small.
        if (minX > maxX)
        {
            minX = maxX = roomBounds.center.x;
        }

        if (minY > maxY)
        {
            minY = maxY = roomBounds.center.y;
        }

        Vector3 spawnPosition = new Vector3(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY),
            targetRoom.transform.position.z
        );

        GameObject q2 = Instantiate(enemy, spawnPosition, Quaternion.identity);

        Canvas p2 = Instantiate(HealthBar, q2.transform.position, Quaternion.identity);

        HealthBarFollow follow = p2.GetComponent<HealthBarFollow>();
        follow.target = q2.transform;
        
    }

    private Bounds GetBounds(GameObject obj)
    {
        if (obj == null)
        {
            return new Bounds(Vector3.zero, Vector3.zero);
        }

        Collider2D roomCollider = obj.GetComponent<Collider2D>();

        if (roomCollider != null)
        {
            return roomCollider.bounds;
        }

        Renderer roomRenderer = obj.GetComponent<Renderer>();

        if (roomRenderer != null)
        {
            return roomRenderer.bounds;
        }

        Collider2D[] childColliders =
            obj.GetComponentsInChildren<Collider2D>();

        if (childColliders.Length > 0)
        {
            Bounds combinedBounds = childColliders[0].bounds;

            for (int i = 1; i < childColliders.Length; i++)
            {
                combinedBounds.Encapsulate(childColliders[i].bounds);
            }

            return combinedBounds;
        }

        Renderer[] childRenderers =
            obj.GetComponentsInChildren<Renderer>();

        if (childRenderers.Length > 0)
        {
            Bounds combinedBounds = childRenderers[0].bounds;

            for (int i = 1; i < childRenderers.Length; i++)
            {
                combinedBounds.Encapsulate(childRenderers[i].bounds);
            }

            return combinedBounds;
        }

        return new Bounds(obj.transform.position, Vector3.zero);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || hasEnteredPreviously)
        {
            return;
        }

        hasEnteredPreviously = true;

        foreach (GameObject enemy in enemies)
        {
            for (int i = 0; i < numToSpawn; i++)
            {
                SpawnInRoom(room, enemy);
            }
        }
    }
}