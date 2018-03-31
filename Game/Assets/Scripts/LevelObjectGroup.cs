using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelObjectGroup : MonoBehaviour
{

    public int myAppearanceTime = 0;
    public List<GameObject> objects;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

   public void SetActive(bool active)
    {
        foreach (var child in objects)
        {
            child.SetActive(active);
        }
    }
    
}
