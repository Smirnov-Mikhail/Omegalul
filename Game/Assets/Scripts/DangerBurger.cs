using UnityEngine;
using System.Collections;

public class DangerBurger : MonoBehaviour
{
    // Update is called once per frame
    public float shift = 0.1f;

    private int count;
    private bool left;

    void Flip()
    {
        // Switch the way the player is labelled as facing
        left = !left;

        // Multiply the player's x local scale by -1
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;


        var sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        float width = sprite.bounds.max.x - sprite.bounds.min.x;
        float delta = width;// / 2.0F;

        Debug.Log(count);

        if(left)
        {
            delta *= -1;
        }

        transform.localPosition = new Vector3(transform.localPosition.x + delta, transform.localPosition.y, transform.localPosition.z);
    }

    void Update()
    {
        var curPos = transform.localPosition;
        if (count == 0 || count == 100)
        {
            Flip();
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
