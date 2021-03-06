﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadingScript_Leveled : MonoBehaviour
{
    public List<LevelObjectGroup> groups;
    public GameObject Game;
    public characterController Character;
    private int count;
    private IEnumerator _corrutine;
    private SpriteRenderer[] sprites;

    // Use this for initialization
    void Start () {
        _corrutine = MyCoroutine();
        sprites = Game.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < sprites.Length; i++)
        {
            var color = sprites[i].color;
            sprites[i].color = new Color(color.r, color.g, color.b, 0);
        }
        foreach(var obj in groups)
        {
            obj.SetActive(false);
        }
        Game.SetActive(true);
        StartCoroutine(_corrutine);
    }
	
    IEnumerator MyCoroutine()
    {
        while (true)
        {
            if (count > 100)
            {
                for (int i = 0; i < sprites.Length; i++)
                {
                    var color = sprites[i].color;
                    sprites[i].color = new Color(color.r, color.g, color.b, 255);
                }
                StopCoroutine(_corrutine);
                gameObject.SetActive(false);
                Game.SetActive(true);
            }

            foreach( var group in groups)
            {
                if(count == group.myAppearanceTime)
                {
                    group.SetActive(true);
                }
            }

            for (int i = 0; i < sprites.Length; i++)
            {
                var color = sprites[i].color;
                sprites[i].color = new Color(color.r, color.g, color.b, count * 0.01f);
            }
            count++;

            yield return new WaitForSeconds(0.1f);
        }

    }
}
