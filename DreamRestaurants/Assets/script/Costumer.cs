using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Costumer : MonoBehaviour
{
    [SerializeField] Image sandwichImage;

    Controller controller;

    public NavMeshAgent agent;

    public Chair targetChair;

    public Animator anim;

    public Vector3 startPos;

   

    void Start()
    {
        
        startPos = transform.position;
        

    }

    private void Awake()
    {
        controller = FindObjectOfType<Controller>();
        anim = gameObject.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Destroy"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (targetChair!=null)
        {
            if (other.gameObject.CompareTag("Chair") && other.gameObject.gameObject == targetChair.m_chair.gameObject)
            {
                agent.enabled = false;
                Vector3 targetChairPos;
                int random = Random.Range(0, 2);
                ChairController chairController = other.gameObject.GetComponent<ChairController>();
                controller.chairs[controller.chairs.IndexOf(targetChair)].m_Full = true;
                if (random == 0)
                {
                    targetChairPos = chairController.chair1.position;
                }
                else
                {
                    targetChairPos = chairController.chair2.position;
                }
                transform.position = targetChairPos;
                transform.parent = random == 0 ? chairController.chair1 : chairController.chair2;
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -0.4f);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                anim.SetBool("sit", true);
                StartCoroutine(WaitForSandwich(chairController.Cbox1, chairController.Cbox2));
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                chairController.currentCostumer = this;
                chairController.gameObject.GetComponent<BoxCollider>().enabled = false;

            }
        }
       
    }

    


    IEnumerator WaitForSandwich(GameObject Cbox1, GameObject Cbox2)
    {
        yield return new WaitForSeconds(3);
        sandwichImage.gameObject.SetActive(true);
        Cbox1.SetActive(true);
        Cbox2.SetActive(true);
    }

    private void LateUpdate()
    {
        if (agent.destination==startPos && transform.position==agent.destination)
        {
            Destroy(this.gameObject);
        }
        
    }

    public IEnumerator GetSandwich(ChairController chairController)
    {
        chairController.sandwichOnTheTable.SetActive(true);
        yield return new WaitForSeconds(3);
        chairController.currentCostumer = null;
        chairController.sandwichOnTheTable.SetActive(false);
        sandwichImage.gameObject.SetActive(false);
        anim.SetBool("sit", false);
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        agent.enabled = true;
        agent.SetDestination(startPos);
        ManyManager.instance.SpawnMoney(new Vector3(chairController.gameObject.transform.position.x + 1,0.03f, chairController.gameObject.transform.position.z));
        controller.chairs[controller.chairs.IndexOf(targetChair)].m_Full = false;
        targetChair = null;
        CostumerSpawner.instance.Spawn();
        chairController.gameObject.GetComponent<BoxCollider>().enabled = true;

    }

    public void ChoiceTarget()
    {
        List<Chair> emptyChairs = new List<Chair>();
        foreach (Chair chair in controller.chairs)
        {
            if (!chair.m_Full)
            {
                emptyChairs.Add(chair);
            }
        }
        targetChair = emptyChairs[Random.Range(0, emptyChairs.Count)];
        Vector3 targetPos = targetChair.m_chair.position;
        agent.SetDestination(targetPos);
        targetChair.m_Full = true;
    }

    void Update()
    {
        
    }
}
