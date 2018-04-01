using UnityEngine;

namespace Assets.Scripts
{
    public class Gate : MonoBehaviour
    {
        public GameObject MissingNo;

        void Update()
        {
            if (StatisticData.instance.Rebooted)
            {
                MissingNo.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}
