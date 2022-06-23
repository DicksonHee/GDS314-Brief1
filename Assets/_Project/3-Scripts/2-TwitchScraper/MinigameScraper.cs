using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scraper
{
    public class MinigameScraper : MonoBehaviour
    {
        public int maxPollAmount = 100;
        public bool showDebug;

        protected readonly Queue<string> _pollList = new();
        protected readonly Dictionary<string, int> _countList = new();

        private void OnEnable()
        {
            ChatReader.OnMessageReceived += GetMessage;
        }

        private void OnDisable()
        {
            ChatReader.OnMessageReceived -= GetMessage;
        }

        protected virtual void GetMessage(string author, string message)
        {
            
        }

        public virtual void GetMessageTest(string message)
        {
            
        }
        protected void DebugMessage(string debugMessage)
        {
            if (!showDebug) return;

            Debug.Log(debugMessage);
        }

        protected void DebugMessage(Dictionary<string, int> dict)
        {
            if (!showDebug) return;

            string debugMessage = "";
            foreach (var item in _countList)
            {
                debugMessage += item.Key + ": " + item.Value;
            }

            Debug.Log(debugMessage);
        }
    }
}