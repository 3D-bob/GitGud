using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testProj : MonoBehaviour {

	void OnEnable ()
    {
        StartCoroutine(LifeTime());
	}
	
	void Update ()
    {
        transform.Translate(00000.1f, 0, 0 * Time.deltaTime);
                
	}

    IEnumerator LifeTime()
    {
       yield return new WaitForSeconds(2);
       gameObject.SetActive(false);
    }
}
