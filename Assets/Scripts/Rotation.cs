using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Epic script made by Tuomas Ahonen

public class Rotation : MonoBehaviour {

    public float x, y, z;

	void Update () {
        transform.Rotate(x *Time.deltaTime, y * Time.deltaTime, z*Time.deltaTime);
		
	}
}
