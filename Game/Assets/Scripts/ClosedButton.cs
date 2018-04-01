using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class ClosedButton : MonoBehaviour
    {
        public GameObject ObjectToDestroy;

        public void OnClick()
        {
            ObjectToDestroy.SetActive(false);
        }
    }
}
