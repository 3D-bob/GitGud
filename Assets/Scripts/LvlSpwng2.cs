using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlSpwng2 : MonoBehaviour {

    //[SerializeField]
    //private List<GameObject> pooledCells;

    public GameObject[] cell1;
    public GameObject[] cell2;
    public GameObject[] cell3;

    public int arrayLength;

    public List<GameObject[]> list;

    [SerializeField]
    GameObject[] levelCell;

    float locationY = 0;

    [SerializeField]
    float locationX = 30.0f;

    int nextCell;
    int loc = 1;

    void Start()
    {


        //Instantiate(Cell[1], new Vector3(0, 0, 0), Quaternion.identity);

        /*for (int i = 0; i < arrayLength; i++)
        {
            GameObject obj = levelCell[i];
            obj.SetActive(false);
            pooledCells.Add(obj);
        }*/

        //generateLevel();
    }

    public void UpdateY(float Y)
    {
        locationY = Y;
    }

    /*void generateLevel()
    {
        nextCell = (int)Random.Range(0, levelCell.Length);

        pooledCells[nextCell].transform.position = new Vector3(loc * locationX, locationY, 0);
        pooledCells[nextCell].transform.rotation = Quaternion.identity;

        pooledCells[nextCell].SetActive(true);

        loc++;
        /*for (int i = 1; i < 20; i++)
        {
            levelCell = (int)Random.Range(0, Cell.Length);

            Instantiate(Cell[levelCell], new Vector3(i * locationX, locationY, 0), Quaternion.identity);
        }
    }

    public GameObject ReturnFromPool()
    {
        for (int i = 0; i < pooledCells.Count; i++)
        {
            if (!pooledCells[i].activeInHierarchy)
            {
                return pooledCells[i];
            }
        }
        return null;
    }*/
}

