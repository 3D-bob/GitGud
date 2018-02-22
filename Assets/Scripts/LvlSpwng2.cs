using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlSpwng2 : MonoBehaviour {

    public List<GameObject> pooledCells;

    //[SerializeField]
    public GameObject[] CellsToPool;

    float locationY = 0;

    //[SerializeField]
    float locationX = 0f;

    Random random = new Random();
    int levelCell;

    public int AmountOfDupes = 3;
    float updateX = 0f;

   

    // Use this for initialization
    void Start()
    {       
        for (int i = 0; i < CellsToPool.Length; i++)
        {
            for (int c = 0; c < AmountOfDupes; c++)
            {
                GameObject cell = (GameObject)Instantiate(CellsToPool[i]);
                cell.SetActive(false);
                pooledCells.Add(cell);
            }
        }

        generateLevel();
        updateX = 0f;
    }

    public void UpdateY(float Y)
    {
        locationY = Y;
    }

    void generateLevel()
    {
        //Testing generation
        for (int i = 0; i < pooledCells.Count; i++)
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

