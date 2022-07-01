using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MyPlayer.Movement;

public class FanBehaviour : MonoBehaviour
{
    public Transform fanHead;
    public float fanInfluenceRange;
    public float fanStrength;
    public float fanArc;
    public float fanSpeed;
    [Range(0,1)] public float triggerThreshold;

    private Transform _player;
    private Vector3 _forceOnPlayer;

    // Start is called before the first frame update
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        fanHead.transform.DORotate(new Vector3(0, fanArc, 0), fanSpeed).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).Play();
    }

	private void Update()
	{
		CalculateForce();
	}

	private void CalculateForce()
    {
        _forceOnPlayer = Vector3.zero;
        float distance = Vector3.Distance(fanHead.transform.position, _player.transform.position);
        if (distance < fanInfluenceRange)
        {
            Vector3 playerDirection = _player.transform.position - fanHead.transform.position;
            float dot = Vector3.Dot(fanHead.transform.forward.normalized, playerDirection.normalized);
            float distanceMultiplier = 1f - Mathf.Clamp(distance / fanInfluenceRange, 0.5f, 1f);
            if (dot > triggerThreshold) _forceOnPlayer = playerDirection * (Mathf.Clamp01(dot) * distanceMultiplier * fanStrength * Time.deltaTime);
        }
    }

    public Vector3 GetForceOnPlayer() => _forceOnPlayer;
}
