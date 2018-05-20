using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartField : MonoBehaviour {


    Renderer sP;
    public float speed;

    public static StartField stars;


    private void Awake()
    {
      
    }

    // Use this for initialization
    void Start () {
        sP = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 offset = new Vector2(0, speed * Time.time * .1f);
        sP.material.mainTextureOffset = offset;
	}
}
