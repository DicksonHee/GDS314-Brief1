using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scraper;
using UnityEditor;
using Random = UnityEngine.Random;

namespace Testing
{
    public class DebugController : MonoBehaviour
    {
        private bool _showConsole;

        private string _input;

        public List<string> stringInputs;
        public int inputAmount = 100;
        public MinigameScraper Scraper;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Backslash)) _showConsole = !_showConsole;
            if (Input.GetKeyDown(KeyCode.Return) && _showConsole) HandleInput();
        }

        private void OnGUI()
        {
            if (!_showConsole)
            {
                return;
            }

            float y = 0f;
            GUI.Box(new Rect(0, y, Screen.width, 30), "");
            GUI.backgroundColor = new Color(0, 0, 0, 0);
            _input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), _input);
        }

        private void HandleInput()
        {
            Scraper.GetMessageTest(_input);
            _input = "";
            _showConsole = false;
        }

        public void RandomInputs()
        {
            for (int ii = 0; ii < inputAmount; ii++)
            {
                string randString = stringInputs[Random.Range(0, stringInputs.Count)];
                Scraper.GetMessageTest(randString);
            }
        }
    }
    
    [CustomEditor(typeof(DebugController))]
    public class DebugEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DebugController controller = (DebugController)target;
            if(GUILayout.Button("RandomInputs")) controller.RandomInputs();
        }
    }
}

