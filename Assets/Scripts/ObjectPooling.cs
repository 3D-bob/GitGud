using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour {

    public List<GameObject> pooledObjects;
    //public GameObject objectToPool;
    public GameObject[] objectToPool;

    //public int poolSize;
    //public GameObject[] levelCell; 
    

	// Use this for initialization
	void Start ()
    {
       for (int i = 0; i < objectToPool.Length; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool[i]);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }

        /*for (int i = 0; i < levelCell.Length; i++)
        {
            GameObject obj = levelCell[i];
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }*/
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("space"))
        {
            GameObject bullet = ReturnFromPool();

            if (bullet != null)
            {
                bullet.transform.position = this.transform.position;
                bullet.transform.rotation = this.transform.rotation;
                bullet.SetActive(true);
            }
        }

    }

    public GameObject ReturnFromPool()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;  
    }
}
