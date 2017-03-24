using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {

    [SerializeField]
    private GameObject Board;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Rotate ()
    {
        Board.transform.Rotate(Vector3.forward, 90f * (Random.value > 0.5 ? -1 : 1));
    }

   // private IEnumerator RotateRoutine ()
   // {

   // }
}
