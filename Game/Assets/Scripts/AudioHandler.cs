using UnityEngine;
using System.Collections;

public class AudioHandler : MonoBehaviour {


    public int delay = 1;
	// Use this for initialization
	void Start () {
	
	}
	
    void Awake()
    {
        var comp = GetComponent<AudioSource>();
        
        comp.PlayDelayed(delay);
        Debug.Log("LOGGING!!");
    }

	// Update is called once per frame
	void Update () {
	
	}
}
