using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlSpwng2 : MonoBehaviour {

    //Tehnyt Joona Jäppinen
    //Scripti luo Startin yhteydessä Objectpoolin CellsToPool arrayn sisältämistä Kenttäpaloista, jotka määritetään editorin puolella.
    //Scripti generoi pelikenttää satunnaisesti kun sitä kutsutaan Objectpoolissa ei aktiivisena olevista Kenttäpaloista.

    [HideInInspector]
    public List<GameObject> pooledCells = new List<GameObject>(); //Listi joka toimii kenttäpalat sisältävänä objectpoolina

    //[SerializeField]
    public GameObject[] CellsToPool; //Array johon editorin puolella määritetään käytettävät kenttäpalat

    //Koordinaatit joiden mukaan määritetään seuraavan kenttäpalan lokaatio
    float locationY = 0f;
    float locationX = 0f;


    [SerializeField]
    int AmountOfDupes = 2; //määrittää kuinka monta kopiota kustakin CellsToPool arrayssa olevista kenttäpaloista luodaan Objectpoolin.

    float updateX = 0f;
 

    void Start()
    {       
        for (int i = 0; i < CellsToPool.Length - 1; i++)
        {
            for (int c = 0; c < AmountOfDupes; c++) //Määritetään kopioiden määrä
            {
                //Instantioidaan Kenttäpalaset järjestyksessä, deactivoidaan ne ja lisätään objectpooliin
                GameObject cell = (GameObject)Instantiate(CellsToPool[i]);
                cell.SetActive(false);
                pooledCells.Add(cell);
            }
        }

        UpdateY(0);
        generateLevel();
        updateX = 0f;
    }

    public void UpdateY(float Y) //päivittää kutsuttaessa y koordinaatin jonka mukaan Kenttäpalanen sijoitetaan generoinnin yhteydessä
    {
        locationY = Y;
        Debug.Log("Y is:"+Y);
    }

    public void generateLevel()
    {
       
        GameObject nextCell = GetNextCell(); //Hakee objectpoolista ei käytössä olevan kenttäpalasen

        if (nextCell != null) //Käyttää haettua kenttäpalasta ja asettaa sen paikalleen.
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

