using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Keyboard
{
    public class KeyboardManager: MonoBehaviour
    {
        public static KeyboardManager Instance;
        private string keySequence = "";

        private void Awake()
        {
            // Singleton pattern to ensure only one instance of the manager exists
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void RegisterKeyPress(string key)
        {
            keySequence += key;
        }

        public string GetKeySequence()
        {
            return keySequence;
        }

        public void ClearKeySequence()
        {
            keySequence = "";
        }
    }


}