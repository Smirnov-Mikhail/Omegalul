using UnityEngine;

namespace Assets.Scripts
{
    public class Gate : MonoBehaviour
    {
        void Update()
        {
            if (StatisticData.instance.Rebooted)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
