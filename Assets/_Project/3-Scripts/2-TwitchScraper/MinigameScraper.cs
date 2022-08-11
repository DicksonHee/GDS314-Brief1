using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scraper
{
    public class MinigameScraper : MonoBehaviour
    {
        // place to grab certain repeatable codes

        public int maxPollAmount = 100;
        public bool showDebug;
        
        protected readonly Queue<string> _pollList = new(); // List of incoming inputs
        protected readonly Dictionary<string, int> _countList = new(); // Dictionary of sorted inputs

        private void OnEnable()
        {
            ChatReader.OnMessageReceived += GetMessage;
        }

        private void OnDisable()
        {
            ChatReader.OnMessageReceived -= GetMessage;
        }

        public virtual void GetMessage(string author, string message)
        {
            if (!_countList.ContainsKey(message)) return;
            
            if (_pollList.Count >= maxPollAmount)
            {
                RemoveMessage(_pollList.Dequeue());
            }

            _pollList.Enqueue(message);
            AddMessage(message);
        }

        public virtual void GetMessageTest(string message)
        {
            if (!_countList.ContainsKey(message)) return;
            
            if (_pollList.Count >= maxPollAmount)
            {
                RemoveMessage(_pollList.Dequeue());
            }

            _pollList.Enqueue(message);
            AddMessage(message);
        }
        
        protected void AddMessage(string message)
        {
            _countList[message]++;
            //ChatInputVisualiser.current.AcceptInput(message);
        }

        protected void RemoveMessage(string message)
        {
            _countList[message]--;
            //ChatInputVisualiser.current.RemoveInput(message);
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