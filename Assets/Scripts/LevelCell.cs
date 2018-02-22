using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCell : MonoBehaviour {

    GameObject spawner;
    float newY;

    Transform location;

    /*private void Awake()
    {
        location = this.gameObject.transform.GetChild(0);

        newY = location.transform.position.y;

        spawner = GameObject.FindGameObjectWithTag("spawner");
        spawner.GetComponent<LevelSpawning>().UpdateY(newY);
    }*/

    void OnEnable()
    {
        location = this.gameObject.transform.GetChild(0);

        newY = location.transform.position.y;

        spawner = GameObject.FindGameObjectWithTag("spawner");
        spawner.GetComponent<LvlSpwng2>().UpdateY(newY);
        ////////////////////^muista vaihtaa^//////////////////
    }
}
