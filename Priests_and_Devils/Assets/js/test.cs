using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class test : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        GameObject[] fuck = new GameObject[4];
        Debug.Log(fuck.Length);

        Stack<GameObject> priestLf = new Stack<GameObject>();
        for (int i = 0; i < 3; i++)
        {
            priestLf.Push(GameObject.CreatePrimitive(PrimitiveType.Sphere));
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
