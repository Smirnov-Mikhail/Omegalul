using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class ExitLoadBarScene : MonoBehaviour
    {
        public GameObject ExitCollider;

        int count;
        void Update()
        {
            if (!StatisticData.instance.EndButtonIsActive && count <= 180)
            {
                transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y, 1));
                count++;
                ExitCollider.SetActive(count >= 180);
            }
        }
    }
}
