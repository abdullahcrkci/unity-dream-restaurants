using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColletManager : MonoBehaviour
{
    public List<GameObject> foodList = new List<GameObject>();
    public GameObject foodPrefab;
    public Transform collectPoint;

    int FoodLimit = 3;

    private void OnEnable()
    {
        TriggerManager.OnFoodCollect += GetFood;
    }

    private void OnDisable()
    {
        TriggerManager.OnFoodCollect -= GetFood;

    }
    void GetFood()
    {
        if (foodList.Count<=FoodLimit)
        {
            GameObject temp = Instantiate(foodPrefab,collectPoint);
            temp.transform.position = new Vector3(collectPoint.position.x, collectPoint.position.y+((float)foodList.Count / 20), collectPoint.position.z);
            foodList.Add(temp);
        }
    }
  

}
