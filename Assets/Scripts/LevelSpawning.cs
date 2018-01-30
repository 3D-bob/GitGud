using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawning : MonoBehaviour {

    [SerializeField]
    Transform[] Cell;

    float locationY = 0;

    [SerializeField]
    float locationX = 30.0f;

    Random random = new Random();
    int levelCell;



    // Use this for initialization
    void Start ()
    {        
        Instantiate(Cell[1], new Vector3(0, 0, 0), Quaternion.identity);

        generateLevel();
    }
	
    public void UpdateY(float Y)
    {
        locationY = Y;
    }

    void generateLevel()
    {
        

        for (int i = 1; i < 20; i++)
        {
            levelCell = (int) Random.Range(0, 3);

            Instantiate(Cell[levelCell], new Vector3(i * locationX, locationY, 0), Quaternion.identity);
        }
    }

  
}
