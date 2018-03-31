using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class StartGameScene : MonoBehaviour
    {
        public List<GameObject> WindowsExeptions;

        void Start()
        {
            for (int i = 0; i < WindowsExeptions.Count; i++)
            {
                if (i >= StatisticData.instance.FinishLevel)
                    WindowsExeptions[i].SetActive(false);
                else
                    WindowsExeptions[i].SetActive(true);

            }
        }
    }
}
