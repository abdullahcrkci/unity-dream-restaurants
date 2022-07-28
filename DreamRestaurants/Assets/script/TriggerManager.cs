using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public delegate void OncOllectArea();

    public static event OncOllectArea OnFoodCollect;

    bool isColleting;

    ColletManager colletManager;

    void Start() 
    {
        colletManager = FindObjectOfType<ColletManager>();


        StartCoroutine(CollectEnum());
    }

    IEnumerator CollectEnum()
    {
        while (true)
        {
            if (isColleting==true)
            {
                OnFoodCollect();
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("CollectArea"))
        {
            isColleting = true;
        }

        if (other.gameObject.CompareTag("Buy"))
        {
            if (ManyManager.instance.monyCount>0)
            {
                ManyManager.instance.GiveMoney();
                BuyChair buychair = other.gameObject.GetComponent<BuyChair>();
                buychair.price--;
                buychair.pricetext.text = buychair.price.ToString();
                if (buychair.price <= 0)
                {
                    buychair.chair.transform.parent = null;
                    buychair.chair.SetActive(true);
                    Destroy(buychair.gameObject);
                    Chair newchair = new Chair();
                    newchair.m_chair = buychair.chair.transform;
                    newchair.m_Full = false;
                    Controller.instance.chairs.Add(newchair);
                }
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.CompareTag("CollectArea"))
        {
            isColleting = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.CompareTag("Cbox"))
        {
            if (other.transform.parent.GetComponent<ChairController>().currentCostumer != null)
            {
                Destroy(colletManager.foodList[colletManager.foodList.Count - 1]);
                colletManager.foodList.RemoveAt(colletManager.foodList.Count - 1);
                other.transform.parent.GetComponent<ChairController>().StartCoroutine(other.transform.parent.GetComponent<ChairController>().currentCostumer.GetSandwich(other.transform.parent.GetComponent<ChairController>()));
                other.transform.parent.GetComponent<ChairController>().Cbox1.gameObject.SetActive(false);
                other.transform.parent.GetComponent<ChairController>().Cbox2.gameObject.SetActive(false);
            }
            

        }
        if (other.gameObject.CompareTag("Many"))
        {
            ManyManager.instance.GetMoney();
            Destroy(other.gameObject);
        }
    }
    
}
