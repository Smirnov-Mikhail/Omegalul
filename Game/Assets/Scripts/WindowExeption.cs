﻿using UnityEngine;

namespace Assets.Scripts
{
    public class WindowExeption : MonoBehaviour
    {
        private void Update()
        {
            if (Input.anyKey && (Application.loadedLevelName == "windowsExeption1" || Application.loadedLevelName == "windowsExeption2"))
            {
                Application.LoadLevel("scene0");
            }
        }
    }
}
