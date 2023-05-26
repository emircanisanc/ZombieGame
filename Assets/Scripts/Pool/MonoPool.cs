using UnityEngine;
using System.Collections.Generic;

public class MonoPool : MonoBehaviour
{
    private List<GameObject> pool;
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize;


    protected void Awake()
    {

        pool = new List<GameObject>();

        for(int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject Get()
    {
        GameObject obj = null;
        foreach (GameObject temp in pool) {
            if (!temp.activeSelf) {
                obj = temp;
            }
        }
        if(obj == null)
        {
            obj = Instantiate(prefab);
            pool.Add(obj);
            obj.SetActive(false);
        }
        return obj;
    }

}