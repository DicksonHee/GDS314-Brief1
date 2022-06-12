using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scraper
{
    public class MinigameScraper : MonoBehaviour
    {
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
    }
}