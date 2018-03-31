﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class ButtonScriptcs : MonoBehaviour
    {
        public GameObject button;
        public DateTime time;

        void OnTriggerEnter2D(Collider2D col)
        {
            button.SetActive(false);
            StatisticData.instance.EndButtonIsActive = false;
            time = DateTime.Now;
        }

        private void Update()
        {
            if (DateTime.Now > time.AddSeconds(10))
            {
                button.SetActive(true);
                StatisticData.instance.EndButtonIsActive = true;
            }
        }
    }
}
