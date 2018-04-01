using UnityEngine;
using System.Collections;

public class DangerBurger : MonoBehaviour
{
    // Update is called once per frame
    public float shift = 0.1f;

    private int count;
    private bool left;
    void Update()
    {
        var curPos = transform.localPosition;
        if (count == 0)
        {
            left = true;
            transform.localScale = new Vector3(-1.0F * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        if (count == 100)
        {
            left = false;
            transform.localScale = new Vector3(-1.0F * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        if (left)
        {
            transform.localPosition = new Vector3(transform.localPosition.x - shift, transform.localPosition.y, transform.localPosition.z);
            count++;
        }
        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x + shift, transform.localPosition.y, transform.localPosition.z);
            count--;
        }

    }
}
