using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scraper;
using TMPro;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

namespace PA.MinigameManager
{
    public class MinigameManager : MonoBehaviour
    {
        [Header("Manager Variables")]
        [SerializeField] protected MinigameScraper scraper;

        protected TMP_Text timerText;
        public static MinigameManager current;

        public TimerAnimation timerAnimator;
        public int maxTime = 0;
        public float _initialStartDelay = 2f;
        public float _acceptingInputDuration = 2f;
        public float _notAcceptingInputDuration = 2f;

        public event Action OnTimerStart;
        public event Action OnTimerStop;
        public event Action OnGameEnded;

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
            OnGameEnded?.Invoke();

            if (hasWon)
            {
                AnalyticsData.SaveWinLoseData("Level Win " + gameObject.scene.name, 1);
                AnalyticsData.SaveTimeRemainingData("Time Remaining " + gameObject.scene.name, maxTime);
                LoadElevatorScene();
            }
            else
            {
                AnalyticsData.SaveWinLoseData("Level Lose " + gameObject.scene.name, 1);
                AnalyticsData.SaveLosePositionData("Position On Lose " + gameObject.scene.name, GameObject.FindGameObjectWithTag("Player").transform.position);
                LoadLoseScene();
            }
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
        
        protected virtual void LoadLoseScene()
        {
            SceneLoad_Manager.LoadSpecificScene("LoseScene");
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
