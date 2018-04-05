using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    private bool isSick;

	void Start ()
    {
	}
	
	void Update ()
    {
		// TODO Code here to infect others periodically? 
	}


    public void Infect()
    {
        isSick = true;
    }

    public bool IsSick()
    {
        return isSick;
    }

    public bool IsHealthy()
    {
        return !isSick;
    }
}
