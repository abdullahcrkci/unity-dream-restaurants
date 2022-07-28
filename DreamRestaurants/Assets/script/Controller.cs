using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Chair
{
    public Transform m_chair;
    public bool m_Full;
}
public class Controller : MonoBehaviour
{
    public static Controller instance;
    public List<Chair> chairs = new List<Chair>();
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
