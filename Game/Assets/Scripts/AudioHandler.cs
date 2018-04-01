using UnityEngine;
using System.Collections;

public class AudioHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
    void Awake()
    {
        var comp = GetComponent<AudioSource>();
        
        comp.PlayDelayed(1);
        Debug.Log("LOGGING!!");
    }

	// Update is called once per frame
	void Update () {
	
	}
}
