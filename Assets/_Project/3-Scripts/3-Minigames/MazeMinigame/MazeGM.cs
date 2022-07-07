using System.Collections;
using System.Collections.Generic;
using MyPlayer.Movement;
using PA.MinigameManager;
using UnityEngine;

public class MazeGM : MinigameManager
{
    public MazeScraper mazeScraper;
    public Camera cam;

    private int turnDirection;
	private float timer;
    private bool startGame;

	protected override void Awake()
	{
		Invoke(nameof(StartGame), _initialStartDelay);
	}

	protected override void Update()
	{
		if (!startGame) return;

		timer += Time.deltaTime;
		if (timer > 0.05f)
		{
			timer = 0;
			cam.transform.Rotate(0, 0, mazeScraper.GetInput() * Time.deltaTime * 100f);
		}
	}

	public override void EndGame()
	{
		StopAllCoroutines();
		PlayerMovement.current.movementSpeed = 0;
		SceneLoad_Manager.LoadSpecificScene(nextScene);
	}

	public void StartGame() => startGame = true;
	public void StopGame() => startGame = false;
}