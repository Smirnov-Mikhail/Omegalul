﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class LoadingScreen : MonoBehaviour
    {
        public GameObject Game;
        public characterController Character;
        public List<GameObject> FistShow;
        public List<GameObject> SecondShow;
        public Image LoadingCircle;
        private int count;
        private IEnumerator _corrutine;
        private SpriteRenderer[] sprites;

        void Start()
        {
            _corrutine = MyCoroutine();
            if (Character != null)
                Character.locked = true;
            sprites = Game.GetComponentsInChildren<SpriteRenderer>();
            for (int i = 0; i < sprites.Length; i++)
            {
                var color = sprites[i].color;
                sprites[i].color = new Color(color.r, color.g, color.b, 0);
            }

            if (FistShow != null)
                foreach (var x in FistShow)
                {
                    x.SetActive(false);
                }

            if (SecondShow != null)
                foreach (var x in SecondShow)
                {
                    x.SetActive(false);
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
                    if (Character != null)
                        Character.locked = false;
                    StopCoroutine(_corrutine);
                    gameObject.SetActive(false);
                    Game.SetActive(true);
                }

                if (count == 50)
                {
                    if (SecondShow != null)
                        foreach (var x in SecondShow)
                        {
                            x.SetActive(true);
                        }
                }

                if (count == 30)
                {
                    if (FistShow != null)
                        foreach (var x in FistShow)
                        {
                            x.SetActive(true);
                        }
                }

                for (int i = 0; i < sprites.Length; i++)
                {
                    var color = sprites[i].color;
                    sprites[i].color = new Color(color.r, color.g, color.b, count * 0.01f);
                }
                LoadingCircle.fillAmount = count * 0.01f;
                count++;

                yield return new WaitForSeconds(0.02f);
            }

        }
    }
}
