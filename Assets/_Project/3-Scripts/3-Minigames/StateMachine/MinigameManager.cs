using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scraper;

namespace PA.MinigameManager
{
    public class MinigameManager : MonoBehaviour
    {
        [SerializeField] protected MinigameScraper scraper;

        public static MinigameManager current;

        public float _initialStartDelay = 2f;
        public float _acceptingInputDuration = 2f;
        public float _notAcceptingInputDuration = 2f;
        public string nextScene;
        
        public event Action OnTimerStart;
        public event Action OnTimerStop;

        protected virtual void Awake()
        {
            current = this;
        }

        protected virtual void Update()
        {
        }

        protected virtual void PreGameState()
        {
        }

        protected virtual void AcceptingInputsState()
        {
        }

        protected virtual void RunningInputsState()
        {
        }

        protected virtual void NotAcceptingInputsState()
        {
        }

        protected virtual void EndGameState()
        {
        }

        public virtual void EndGame()
        {
        }
        
        public virtual void KillPlayer()
        {
        }
        
        public void StartProtocol()
        {
            StartCoroutine(MinigameProtocol_CO());
        }

        protected virtual IEnumerator MinigameProtocol_CO()
        {
            yield return null;
        }
    }
}
