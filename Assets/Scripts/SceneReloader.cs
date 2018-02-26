using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour {

    GameObject player;
    GameObject WalloD;

	void Start ()
    {
	    	player = GameObject.FindGameObjectWithTag("Player");
        WalloD = GameObject.FindGameObjectWithTag("WallODeath");
	}

	void Update ()
    {
		if(!player.activeInHierarchy)
        {
            WalloD.SetActive(false);
            StartCoroutine(ReloadScene());

        }
	}

    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }
}
