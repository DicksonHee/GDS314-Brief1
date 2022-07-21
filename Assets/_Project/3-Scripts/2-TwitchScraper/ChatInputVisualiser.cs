using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scraper
{
    public class ChatInputVisualiser : MonoBehaviour
    {
        public static ChatInputVisualiser current;

		private void Awake()
		{
			current = this;
		}

		public void AcceptInput(string message)
		{
			Debug.Log(message);
		}

		public void RemoveInput(string message)
		{
			Debug.Log(message);
		}
	}
}