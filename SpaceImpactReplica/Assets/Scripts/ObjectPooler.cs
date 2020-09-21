using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

    public GameObject pooledObject;
    public int pooledAmount;
    public bool willGrow;

    public List<GameObject> pooledObjects;

	// Use this for initialization
	void Start () {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)(Instantiate(pooledObject));
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
	}
	
	public GameObject GetPooledObject(Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
            if (!pooledObjects[i].activeInHierarchy)
            {
                pooledObjects[i].transform.position = position;
                pooledObjects[i].transform.rotation = rotation;
                pooledObjects[i].SetActive(true);
                return pooledObjects[i];
            }
        if (willGrow)
        {
            GameObject obj = (GameObject)(Instantiate(pooledObject));
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
            pooledObjects.Add(obj);
            return obj;
        }

        return null;
    }

    public GameObject GetPooledObject(Vector3 position)
    {
        return GetPooledObject(position, pooledObject.transform.rotation);
    }
}
