using System.Collections.Generic;
using UnityEngine;

public class Scr_ObjectPooler : MonoBehaviour
{
    public static Scr_ObjectPooler instance = null;

    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool, transform.GetChild(0));
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }
}
