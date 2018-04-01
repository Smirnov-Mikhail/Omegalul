using UnityEngine;
using System.Collections;

public class Vrag : MonoBehaviour
{

    private Animator animator;
    private CircleCollider2D colider;
    public int ShotEveryFrames = 20;
    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("State", 0);
        colider = GetComponent<CircleCollider2D>();
        shot = Resources.Load<Shot>("Shot");
    }

    private Shot shot;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetInteger("State") == 1)
        {
            if (pulCOunter == ShotEveryFrames)
            {
                Shoot(currentPos, go);
                pulCOunter = 0;
            }
            else
            {
                pulCOunter++;
            }
        }

    }

    void Shoot(Vector3 to, GameObject go)
    {
        Vector3 position = transform.position;
        var pulya = Instantiate(shot, position, shot.transform.rotation) as Shot;
        pulya.Direction = to - position;
        pulya.Target = go;


    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "hero")
        {

            if (animator.GetInteger("State") != 1)
            {
                animator.SetInteger("State", 1);
                Shoot(col.gameObject.transform.position, col.gameObject);
            }
        }
    }
    private int pulCOunter = 0;
    private Vector3 currentPos;
    private GameObject go;
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name == "hero")
        {
            currentPos = col.gameObject.transform.position;
            go = col.gameObject;
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "hero")
        {
            if (animator.GetInteger("State") != 0)
                animator.SetInteger("State", 0);
        }
    }

}
