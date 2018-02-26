using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlSpwng2 : MonoBehaviour {

    [HideInInspector]
    public List<GameObject> pooledCells = new List<GameObject>();

    //[SerializeField]
    public GameObject[] CellsToPool;

    //[SerializeField]
    float locationY = 0f;

    //[SerializeField]
    float locationX = 0f;

    int levelCell;

    [SerializeField]
    int AmountOfDupes = 2;

    float updateX = 0f;
 

    void Start()
    {       
        for (int i = 0; i < CellsToPool.Length - 1; i++)
        {
            for (int c = 0; c < AmountOfDupes; c++)
            {
                GameObject cell = (GameObject)Instantiate(CellsToPool[i]);
                cell.SetActive(false);
                pooledCells.Add(cell);
            }
        }

        UpdateY(0);
        generateLevel();
        updateX = 0f;
    }

    public void UpdateY(float Y)
    {
        locationY = Y;
        Debug.Log("Y is:"+Y);
    }

    public void generateLevel()
    {
       
        GameObject nextCell = GetNextCell();

        if (nextCell != null)
        {
            nextCell.transform.position = new Vector3(locationX, locationY, 0);
            nextCell.transform.rotation = Quaternion.identity;
            nextCell.SetActive(true);
            locationX += 30;
        }
    }

    public GameObject GetNextCell()
    {
        updateX++;

        for (int i = 0; i < pooledCells.Count; i++)
        {
            if(!pooledCells[i].activeInHierarchy)
            {
                int r = Random.Range(0, pooledCells.Count);

                while(pooledCells[r].activeInHierarchy)
                {
                    r = Random.Range(0, pooledCells.Count);
                }
                return pooledCells[r];
            }
        }

        return null;
    }
}

