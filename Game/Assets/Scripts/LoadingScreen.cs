using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class LoadingScreen : MonoBehaviour
    {
        public GameObject Game;
        public GameObject Fire;
        private int count;
        private IEnumerator _corrutine;
        private SpriteRenderer[] sprites;

        void Start()
        {
            _corrutine = MyCoroutine();
            sprites = Game.GetComponentsInChildren<SpriteRenderer>();
            for (int i = 0; i < sprites.Length; i++)
            {
                var color = sprites[i].color;
                sprites[i].color = new Color(color.r, color.g, color.b, 0);
            }
            Game.SetActive(true);
            StartCoroutine(_corrutine);
        }

        void Update()
        {
            Fire.transform.Rotate(new Vector3(0f, 0f, -2f));
        }

        IEnumerator MyCoroutine()
        {
            while (true)
            {
                if (count > 10)
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

                for (int i = 0; i < sprites.Length; i++)
                {
                    var color = sprites[i].color;
                    sprites[i].color = new Color(color.r, color.g, color.b, count * 0.1f);
                }
                count++;

                yield return new WaitForSeconds(0.05f);
            }

        }
    }
}
