using UnityEngine;

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

    public void DestroyThis()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        if (left)
        {
            transform.localPosition = new Vector3(transform.localPosition.x - shift, transform.localPosition.y, transform.localPosition.z);
        }
        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x + shift, transform.localPosition.y, transform.localPosition.z);
        }

    }
}
