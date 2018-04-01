using UnityEngine;
using System.Collections;

public class FlyDangerBurger : MonoBehaviour
{
    // Update is called once per frame
    public float shift = 0.1f;
    public int lifeTime;
    public bool left;
    public bool needDestroy;

    void Start()
    {
        // уничтожить объект по истечению указанного времени (секунд)
        if (needDestroy)
            Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        if (left)
        {
            transform.localPosition = new Vector3(transform.localPosition.x - shift, transform.localPosition.y, -10);
        }
        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x + shift, transform.localPosition.y, -10);
        }

    }
}
