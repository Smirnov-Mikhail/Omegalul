using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class StartGameScene : MonoBehaviour
    {
        public List<GameObject> WindowsExeptions;
        public FlyDangerBurger FlyBurger;
        public Vector3 FlyBurgerStartPosition;

        private IEnumerator _corrutine;
        void Start()
        {
            for (int i = 0; i < WindowsExeptions.Count; i++)
            {
                if (i >= StatisticData.instance.FinishLevels)
                    WindowsExeptions[i].SetActive(false);
                else
                    WindowsExeptions[i].SetActive(true);

            }

            _corrutine = MyCoroutine();
            StartCoroutine(_corrutine);
        }

        IEnumerator MyCoroutine()
        {
            while (true)
            {
                FlyDangerBurger rocketClone = (FlyDangerBurger)Instantiate(FlyBurger, FlyBurgerStartPosition, transform.rotation);
                rocketClone.needDestroy = true;
                yield return new WaitForSeconds(2f);
            }
        }
    }
}
