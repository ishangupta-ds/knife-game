using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public Rigidbody rb;
    private Vector3 startSwipe, endSwipe;
    private float timeWhenInAir, timeWhenStartedFlying;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetMouseButtonDown(0))
        {
            startSwipe = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            endSwipe = Input.mousePosition;
            Swipe();
        }
	}
    private void Swipe()
    {
        rb.useGravity = true;
        rb.isKinematic = false;
        Debug.Log("swipe action"); 
        Vector3 swipe = endSwipe - startSwipe;
        rb.AddForce(swipe*0.06f, ForceMode.Impulse);
        rb.AddTorque(0.0f, 0.0f, 20, ForceMode.Impulse);
        timeWhenStartedFlying = Time.time;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("Box"))
        {
            Reset();
        }
        else
        {
            rb.isKinematic = true;
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        timeWhenInAir = Time.time - timeWhenStartedFlying;
        if(!rb.isKinematic && timeWhenInAir >= 0.1f)
        {
            Debug.Log("Failed");
            Reset();
        }
    }
    private void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
