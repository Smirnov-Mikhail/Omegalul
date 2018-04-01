using UnityEngine;

namespace Assets.Scripts
{
    public class WindowExeption : MonoBehaviour
    {
        private void Update()
        {
            if ((Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.Return)) && (Application.loadedLevelName == "windowsExeption1" 
                || Application.loadedLevelName == "windowsExeption2"
                || Application.loadedLevelName == "windowsExeption3"))
            {
                Application.LoadLevel("scene0");
            }
        }
    }
}
