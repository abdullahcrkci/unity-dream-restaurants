using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinterMamager : MonoBehaviour
{
    public List<GameObject> foodList = new List<GameObject>();
    public GameObject foodPrefab;
    public Transform exitPoint;
    bool isWorking;
    int stackCount = 6;
    void Start()
    {
        StartCoroutine(TakeFood());
    }

    public void RemoveLast()
    {
        if (foodList.Count>0)
        {
            Destroy(foodList[foodList.Count - 1]);
            foodList.RemoveAt(foodList.Count - 1);
        }
    }

    IEnumerator TakeFood()
    {

        while (true)
        {
            float foodCount = foodList.Count;
            int rowCount = (int)foodCount / stackCount;
            if (isWorking==true)
            {
                GameObject temp = Instantiate(foodPrefab);
                temp.transform.position = new Vector3(exitPoint.position.x+((float)rowCount%3), (foodCount%stackCount) / 20, exitPoint.position.z);
                foodList.Add(temp);
                if (foodList.Count >= 6)
                {
                    isWorking = false;
                }
            }
            else if(foodList.Count<6)
            {
                isWorking = true;
            }
            yield return new WaitForSeconds(1);

        }
    }
}
