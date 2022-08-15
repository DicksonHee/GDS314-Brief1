using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scraper;
using TMPro;

namespace PA.MinigameManager
{
    public class MinigameManager : MonoBehaviour
    {
        [SerializeField] protected MinigameScraper scraper;

        public static MinigameManager current;

        protected TMP_Text timerText;

        public int maxTime = 0;
        public TimerAnimation timerAnimator;
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

        public void PlayerWin() => EndGame(true);
        public void PlayerLose() => EndGame();
        
        public virtual void EndGame(bool hasWon = false)
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

        protected virtual void LoadElevatorScene()
        {
            SceneLoad_Manager.LoadSpecificScene("ElevatorScene");
        }
        
        protected virtual void LoadMainMenuScene()
        {
            SceneLoad_Manager.LoadSpecificScene("NewStartScene");
        }

        protected void StartTimer()
        {
            OnTimerStart?.Invoke();
            StartCoroutine(DecrementGameTimer());
        }
        
        private IEnumerator DecrementGameTimer()
        {
            maxTime--;
            timerAnimator.StartAnim("" + maxTime);
            yield return new WaitForSeconds(1f);

            if (maxTime > 0) StartCoroutine(DecrementGameTimer());
            else OnTimerStop?.Invoke();
        }
    }
}
