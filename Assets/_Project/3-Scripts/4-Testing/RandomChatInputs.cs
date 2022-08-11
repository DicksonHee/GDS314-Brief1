using System;
using System.Collections;
using System.Collections.Generic;
using Scraper;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace  Testing
{
    public class RandomChatInputs : MonoBehaviour
    {
        public List<string> inputs = new() {"up", "down", "left", "right"};
        private int lowRange;
        private int highRange;
        
        private readonly List<string> firstName = new()
        {
            "Lori", "Veronica", "Rhonda", "Phillip", "Melinda", "Joy", "Jessica", "Elizabeth", "Christopher", "Ashley"
        };

        private readonly List<string> lastName = new()
        {
            "Taylor", "Coleman", "Tucker", "Chapman", "Griffin", "Glover", "Hicks", "Acosta", "Mendoza", "Shepard"
        };

        private void Awake()
        {
            lowRange = 0;
            highRange = inputs.Count;
            StartCoroutine(UpdateRanges());
            StartCoroutine(Inputs());
        }
        
        private IEnumerator Inputs()
        {
            yield return new WaitForSeconds(0.1f);
            string author = firstName[Random.Range(0,firstName.Count)] + " " + lastName[Random.Range(0,lastName.Count)];
            string message = inputs[Random.Range(lowRange, highRange)];
            ChatReader.current.ProcessTestInput(author, message);
            StartCoroutine(Inputs());
        }
        
        private IEnumerator UpdateRanges()
        {
            yield return new WaitForSeconds(Random.Range(0f, 2f));
            lowRange = Random.Range(0, inputs.Count);
            highRange = Mathf.Clamp(Random.Range(lowRange, inputs.Count), 0, inputs.Count);
            StartCoroutine(UpdateRanges());
        }
    }
}