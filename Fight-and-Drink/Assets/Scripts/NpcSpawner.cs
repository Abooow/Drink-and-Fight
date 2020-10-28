using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawner : MonoBehaviour
{
    public Transform NpcRootParent;
    public GameObject PolicePrefab;
    public float DespawnDistance = 15f;
    public int MaxPolice = 10;

    private new Camera camera;
    private List<GameObject> spawnedPolice;

    void Start()
    {
        camera = GetComponent<Camera>();
        spawnedPolice = new List<GameObject>();

        for (int i = 0; i < 10; i++)
        {
            SpawnPolice(0.1f);
        }
    }

    void Update()
    {
        SpawnPolice(0.01f);
        DespawnPolice();
    }
    
    private void SpawnPolice(float odds)
    {
        if (spawnedPolice.Count < MaxPolice && Random.value <= odds)
        {
            Vector2 position;
            do
            {
                position = (Vector2)transform.position + Random.insideUnitCircle * 12f;
            } while (Vector2.Distance(transform.position, position) < 9f && CanSpawnOnPosition(position));

            SpawnNewPolice(position);
        }
    }

    private bool CanSpawnOnPosition(Vector2 position)
    {
        return Physics2D.CircleCast(position, 0.4f, Vector2.zero);
    }

    private void SpawnNewPolice(Vector2 position)
    {
        GameObject police = Instantiate(PolicePrefab, NpcRootParent);

        // Don't use parent scale.
        Vector3 scale = new Vector3(PolicePrefab.transform.localScale.x / NpcRootParent.localScale.x, PolicePrefab.transform.localScale.y / NpcRootParent.localScale.y, PolicePrefab.transform.localScale.z / NpcRootParent.localScale.z);
        police.transform.localScale = scale;
        police.transform.position = position;

        spawnedPolice.Add(police);
    }

    private void DespawnPolice()
    {
        for (int i = spawnedPolice.Count - 1; i >= 0; i--)
        {
            float d = Vector2.Distance(transform.position, spawnedPolice[i].transform.position);
            if (d >= DespawnDistance)
            {
                Destroy(spawnedPolice[i]);
                spawnedPolice.Remove(spawnedPolice[i]);
            }
        }
    }
}
