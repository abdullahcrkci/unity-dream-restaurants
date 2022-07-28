using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManyManager : MonoBehaviour
{
    public static ManyManager instance;

    public GameObject monyPrefab;

    public Text monyText;

    public int monyCount;

    public void GetMoney()
    {
        monyCount += 3;
        monyText.text = monyCount.ToString();
    }
    public void SpawnMoney(Vector3 Position)
    {
        Instantiate(monyPrefab, Position, Quaternion.identity);
    }

    public void GiveMoney()
    {
        monyCount--;
        monyText.text = monyCount.ToString();
    }

    void Start()
    {
        if (instance==null)
        {
            instance = this;
        }
    }

    void Update()
    {
        
    }
}
