using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCell : MonoBehaviour {

    GameObject spawner;
    float newY;

    Transform Cell;
    Transform location;

    List<GameObject> Variants = new List<GameObject>();

    void OnEnable()
    {
        location = this.gameObject.transform.GetChild(0);

        newY = location.transform.position.y;

        spawner = GameObject.FindGameObjectWithTag("spawner");
        spawner.GetComponent<LvlSpwng2>().UpdateY(newY);

        Cell = transform;

        for (int i = 0; i < Cell.childCount; i++)
        {
            Transform obj = Cell.GetChild(i);
            if(obj.tag == "Variant")
            {
                if(obj.gameObject.activeInHierarchy)
                {
                    obj.gameObject.SetActive(false);
                }
              
                Variants.Add(obj.gameObject);
            }
        }

        if(Variants.Count > 0)
        {
            Variants[Random.Range(0, Variants.Count)].SetActive(true);
        }
       
    }
}
