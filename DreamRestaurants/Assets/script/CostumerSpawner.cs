using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumerSpawner : MonoBehaviour
{
    public float minX, minY, minZ;

    public float maxX, maxY, maxZ;

    public GameObject costumerPrefab;

    public static CostumerSpawner instance;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        Spawn();
    }

    public void Spawn()
    {
        Vector3 spawnPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
        GameObject newCostumer = Instantiate(costumerPrefab,spawnPos, Quaternion.identity);
        newCostumer.GetComponent<Costumer>().ChoiceTarget();

    }

    void Update()
    {
        
    }
}
