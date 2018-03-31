using UnityEngine;
using System.Collections;

public class DangerBurger : MonoBehaviour
{
    // Update is called once per frame
    public int count;
    public float shift = 0.1f;

    bool left;
    void Update()
    {
        var curPos = transform.localPosition;
        if (count == 0)
            left = true;

        if (count == 100)
            left = false;

        if (left)
        {
            transform.localPosition = new Vector3(transform.localPosition.x - shift, transform.localPosition.y, -10);
            count++;
        }
        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x + shift, transform.localPosition.y, -10);
            count--;
        }

    }
}
