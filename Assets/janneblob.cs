using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class janneblob : MonoBehaviour {
    public float speed = 2;
    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
    Vector2 input;
	// Update is called once per frame
	void Update () {
        input = new Vector2(Input.GetAxis("Horizontal"), 0);
	}
    private void FixedUpdate()
    {
        rb.angularVelocity += input.x * speed * Time.deltaTime;
        Debug.Log(rb.velocity);
    }
}
