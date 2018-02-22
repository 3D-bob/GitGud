using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour {

    public List<GameObject> pooledObjects;
    public GameObject[] objectToPool;
  
	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < objectToPool.Length; i++)
         {
             GameObject obj = (GameObject)Instantiate(objectToPool[i]);
             obj.SetActive(false);
             pooledObjects.Add(obj);
         }
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
                int r = Random.Range(0, pooledObjects.Count);
                Debug.Log("new r:" + r);

                while(pooledObjects[r].activeInHierarchy)
                {
                    r = Random.Range(0, pooledObjects.Count);
                    Debug.Log("updated r:" + r);
                }
                return pooledObjects[r];
            }
        }

        return null; 
    }
}
