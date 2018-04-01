using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {


    private float speed = 10.0F;
    private Vector3 direction;
    public Vector3 Direction { set { direction = value; } }

    private GameObject myTarget;
    public GameObject Target { set { myTarget = value; } }

    private SpriteRenderer sprite;

    public bool samonavod = false;


    void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        
    }

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 2);
        //transform.LookAt(myTarget.transform);
    }
	
	// Update is called once per frame
	void Update () {
        
        if(samonavod)
        {
            var vecTo = myTarget.transform.position - transform.position;
            transform.position += vecTo.normalized * speed * Time.deltaTime;
            transform.rotation = Quaternion.FromToRotation(transform.position, -(myTarget.transform.position - transform.position));
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        }

    }
}
