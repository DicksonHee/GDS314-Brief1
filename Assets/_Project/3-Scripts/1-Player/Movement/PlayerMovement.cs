using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace MyPlayer.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        public static PlayerMovement current;

        private Rigidbody _rigidbody;
        private Vector3 _playerInput;
        private Vector3 _twitchForceModifier;
        private Vector3 _outwardForceModifier;
        
        private Vector3 _inputDir;
        private Vector3 _forceDir;
        
        private bool _isGrounded;
        private bool _canMove;
        
        public Transform playerModel;
        public Animator playerAnimator;

        public Transform groundDetector;
        public float groundDetectorRange;
        public LayerMask groundLayer;
        public float movementSpeed = 5f;

        public GameObject stepUpperDetector;
        public GameObject stepLowerDetector;
        public float stepSmoothAmount;

        public static Action<float, float> OnApplyForce;
        private static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");
        private static readonly int Landed1 = Animator.StringToHash("Landed");
        private static readonly int Fall = Animator.StringToHash("Fall");

        private void Awake()
		{
			current = this;
            playerAnimator.SetTrigger(Fall);
		}

		private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _playerInput = Vector3.zero;
            _twitchForceModifier = Vector3.zero;
        }

        private void OnEnable()
        {
            OnApplyForce += SetTwitchForceModifier;
        }

        private void OnDisable()
        {
            OnApplyForce -= SetTwitchForceModifier;
        }

        private void Update()
        {
            CheckInput();
            CheckGrounded();
            CheckCanMove();
            HandleAnimation();
        }

        private void FixedUpdate()
        {
            ApplyMovement();
            ApplyRotation();
            CheckStep();
        }

        private void CheckInput()
        {
            if (!_canMove) return;
            
            _playerInput.x = Input.GetAxisRaw("Horizontal");
            _playerInput.z = Input.GetAxisRaw("Vertical");
        }

        private void ApplyMovement()
        {
            if (!_canMove) _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y);
            else
            {
                _inputDir = _playerInput * (movementSpeed * Time.deltaTime);
                _forceDir = Vector3.ClampMagnitude(_twitchForceModifier.normalized + _outwardForceModifier.normalized, 1) * (movementSpeed * 0.8f * Time.deltaTime);

                Vector3 movementDir = _inputDir + _forceDir;
                movementDir.y = _rigidbody.velocity.y;
                _rigidbody.velocity = movementDir;
            }
        }

        private void ApplyRotation()
        {
            float yRotation = Mathf.Rad2Deg * Mathf.Atan2(_inputDir.x, _inputDir.z);
            playerModel.DORotate(new Vector3(0,yRotation, 0f), 0.3f);
        }
        
        private void SetTwitchForceModifier(float xVal, float zVal)
        {
            _twitchForceModifier.x = xVal;
            _twitchForceModifier.z = zVal;
        }

        public void ApplyOutwardForce(Vector3 forceDir)
        {
            Vector3 force = forceDir;
            if (Math.Abs(_twitchForceModifier.x - 1) < 0.01f) force.x = 0;
            if (Math.Abs(_twitchForceModifier.z - 1) < 0.01f) force.z = 0;
            _outwardForceModifier = force;
        }

        private void CheckGrounded()
        {
            bool wasGrounded = _isGrounded;
            _isGrounded = Physics.Raycast(groundDetector.position, Vector3.down, groundDetectorRange, groundLayer);
            if (!wasGrounded && _isGrounded) Landed();
        }

        private void CheckCanMove()
        {
            if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle") ||
                playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Running")) _canMove = true;
            else _canMove =  false;
        }

        private void CheckStep()
        {
            Vector3 position = _rigidbody.position;

            if (Physics.Raycast(stepLowerDetector.transform.position, transform.TransformDirection(Vector3.forward), out _, 0.1f))
            {
                if (!Physics.Raycast(stepUpperDetector.transform.position, transform.TransformDirection(Vector3.forward), out _, 0.2f))
                {
                    position = new Vector3(position.x, stepSmoothAmount + position.y, position.z);
                    _rigidbody.position = position;
                }
            }

            if (Physics.Raycast(stepLowerDetector.transform.position, transform.TransformDirection(1.5f,0f,1f), out _, 0.1f))
            {
                if (!Physics.Raycast(stepUpperDetector.transform.position, transform.TransformDirection(1.5f,0f,1f), out _, 0.2f))
                {
                    position = new Vector3(position.x, stepSmoothAmount + position.y, position.z);
                    _rigidbody.position = position;
                }
            }
            
            if (Physics.Raycast(stepLowerDetector.transform.position, transform.TransformDirection(-1.5f,0f,1f), out _, 0.1f))
            {
                if (!Physics.Raycast(stepUpperDetector.transform.position, transform.TransformDirection(-1.5f,0f,1f), out _, 0.2f))
                {
                    position = new Vector3(position.x, stepSmoothAmount + position.y, position.z);
                    _rigidbody.position = position;
                }
            }
        }
        
        public void SetCanMove(bool val) => _canMove = val;
        
        private void Landed()
        {
            playerAnimator.SetTrigger(Landed1);
        }
        
        private void HandleAnimation()
        {
            playerAnimator.SetFloat(MoveSpeed, _inputDir.magnitude);
        }

        public void SetTrigger(string param)
        {
            playerAnimator.SetTrigger(param);
        }
    }
}

