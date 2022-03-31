using System;
using UnityEngine;
using System.Collections;

namespace Veganimus.Platformer
{
	public class CameraController : MonoBehaviour
	{
		[Header("Setup")]
		[Range(1.1f, 10f)]
		[SerializeField] private float _scrollMultiplier = 1.8f;
		[SerializeField] private float _smoothTime = 0.35f;
		[SerializeField] private Vector2 _movementWindowSize = new Vector2(8, 6);
		[SerializeField] private Vector2 _windowOffset;
		[Header("Camera Boundaries")]
		[SerializeField] private bool _isCamMovementLimited = false;
		[SerializeField] private int _areaID;
		[SerializeField] private float _bottomLimit;
		[SerializeField] private float _leftLimit;
		[SerializeField] private float _rightLimit;
		[SerializeField] private float _topLimit;
		[Header("Debug Visuals")]
		[SerializeField] private bool _showDebug = false;
		private bool _activeTracking = true;
		private float _deltaTime;
		private Rect _windowRect;
		private Vector3 _cameraPosition;
		private Vector3 _playerPosition;
		private Vector3 _previousPlayerPosition;
		private Vector3 _smoothVelocity = Vector3.zero;
		private Transform _transform;
		public int AreaID
		{
			get => _areaID;
			set {  }
		}

		public GameObject trackedObject;

		private void Awake()
		{
			_transform = transform;
			_cameraPosition = _transform.position;
			if (trackedObject == null)
				Debug.LogError("Nothing to track!");

			_previousPlayerPosition = trackedObject.transform.position;
			ValidateLeftAndRightLimits();
			ValidateTopAndBottomLimits();

			//These are the root x/y coordinates that we will use to create our boundary rectangle.
			//Starts at the lower left, and takes the offset into account.
			float windowAnchorX = _cameraPosition.x - _movementWindowSize.x / 2 + _windowOffset.x;
			float windowAnchorY = _cameraPosition.y - _movementWindowSize.y / 2 + _windowOffset.y;

			//From our anchor point, we set the size of the window based on the public variable above.
			_windowRect = new Rect(windowAnchorX, windowAnchorY, _movementWindowSize.x, _movementWindowSize.y);
		}
		private void LateUpdate()
		{
			_deltaTime = Time.deltaTime;
			CameraUpdate();
			if (_showDebug)
				DrawDebugBox();
		}

		private void CameraUpdate()
		{
			_playerPosition = trackedObject.transform.position;
			if (_activeTracking && _playerPosition != _previousPlayerPosition)
			{
				_cameraPosition = _transform.position;
				//Get the distance of the player from the camera.

				Vector3 playerPositionDifference = _playerPosition - _previousPlayerPosition;
				//Move the camera this direction, but faster than the player moved.

				Vector3 multipliedDifference = playerPositionDifference * _scrollMultiplier;
				_cameraPosition += multipliedDifference;

				//updating our movement window root location based on the current camera position
				_windowRect.x = _cameraPosition.x - _movementWindowSize.x / 2 + _windowOffset.x;
				_windowRect.y = _cameraPosition.y - _movementWindowSize.y / 2 + _windowOffset.y;
				// We may have overshot the boundaries, or the player just may have been moving too 
				// fast/popped into another place. This corrects for those cases, and snaps the 
				// boundary to the player.
				if (!_windowRect.Contains(_playerPosition))
				{
					Vector3 positionDifference = _playerPosition - _cameraPosition;
					positionDifference.x -= _windowOffset.x;
					positionDifference.y -= _windowOffset.y;

					//I made a function to figure out how much to move in order to snap the boundary to the player.
					_cameraPosition.x += DifferenceOutOfBounds(positionDifference.x, _movementWindowSize.x);

					_cameraPosition.y += DifferenceOutOfBounds(positionDifference.y, _movementWindowSize.y);
				}

				// Here we clamp the desired position into the area declared in the limit variables.
				if (_isCamMovementLimited)
				{
					_cameraPosition.y = Mathf.Clamp(_cameraPosition.y, _bottomLimit, _topLimit);
					_cameraPosition.x = Mathf.Clamp(_cameraPosition.x, _leftLimit, _rightLimit);
				}
				// and now we're updating the camera position using what came of all the calculations above.
				_transform.position = Vector3.SmoothDamp(_transform.position,_cameraPosition, ref _smoothVelocity, _smoothTime,100f);
			}
			_previousPlayerPosition = _playerPosition;
		}
		//This takes the player distance from the camera, and subtracts the boundary distance to find how far the
		//player has overshot things.
		private static float DifferenceOutOfBounds(float differenceAxis, float windowAxis)
		{
			float difference;

			// We're seeing here if the player has overshot it on this axis. Else, we just set the 
			// difference to zero. This is because if we subtract the boundary distance when the player isn't far 
			// from the camera, we'll needlessly compensate, and screw up the camera.
			if (Mathf.Abs(differenceAxis) <= windowAxis / 2)
				difference = 0f;
			// And if the player legit overshot the boundary, we subtract the boundary from the distance.
			else
				difference = differenceAxis - (windowAxis / 2) * Mathf.Sign(differenceAxis);
			//Returns something if the overshot was legit, and zero if it wasn't.
			return difference;
		}

		// These try to correct for accidents/confusion.
		private void ValidateTopAndBottomLimits()
		{
			if (_topLimit < _bottomLimit)
			{
				Debug.LogError($"You have set the limitBottom ({ _bottomLimit }) to a higher number than limitTop ({ _topLimit }). The top has to be higher than the bottom.");
				Debug.LogError($"I'm correcting this for you, but please make sure the bottom is under the top next time. If you meant to lock the camera, please set top and bottom limits to the same number.");

				float tempFloat = _topLimit;

				_topLimit = _bottomLimit;
				_bottomLimit = tempFloat;
			}
		}

		private void ValidateLeftAndRightLimits()
		{
			if (_rightLimit < _leftLimit)
			{
				Debug.LogError($"You have set the limitLeft ({_leftLimit}) to a higher number than limitRight ({ _rightLimit}). It puts the left limit to the right of the right limit.");
				Debug.LogError($"I'm correcting this for you, but please make sure the left limit is to the left of the right limit. If you meant to lock the camera, please set left and right limits to the same number.");

				float tempFloat = _rightLimit;

				_rightLimit = _leftLimit;
				_leftLimit = tempFloat;
			}
		}

		private void DrawDebugBox()
		{
			Vector3 cameraPos = _transform.position;

			//This will draw the boundaries you are tracking in green, and boundaries you are ignoring in red.
			_windowRect.x = cameraPos.x - _movementWindowSize.x / 2 + _windowOffset.x;
			_windowRect.y = cameraPos.y - _movementWindowSize.y / 2 + _windowOffset.y;

			Color xColorA;
			Color xColorB;
			Color yColorA;
			Color yColorB;

			if (!_activeTracking || _isCamMovementLimited && cameraPos.x <= _leftLimit)
				xColorA = Color.red;
			else
				xColorA = Color.green;

			if (!_activeTracking || _isCamMovementLimited && cameraPos.x >= _rightLimit)
				xColorB = Color.red;
			else
				xColorB = Color.green;

			if (!_activeTracking || _isCamMovementLimited && cameraPos.y <= _bottomLimit)
				yColorA = Color.red;
			else
				yColorA = Color.green;

			if (!_activeTracking || _isCamMovementLimited && cameraPos.y >= _topLimit)
				yColorB = Color.red;
			else
				yColorB = Color.green;

			Vector3 actualWindowCorner1 = new Vector3(_windowRect.xMin, _windowRect.yMin, 0);
			Vector3 actualWindowCorner2 = new Vector3(_windowRect.xMax, _windowRect.yMin, 0);
			Vector3 actualWindowCorner3 = new Vector3(_windowRect.xMax, _windowRect.yMax, 0);
			Vector3 actualWindowCorner4 = new Vector3(_windowRect.xMin, _windowRect.yMax, 0);

			Debug.DrawLine(actualWindowCorner1, actualWindowCorner2, yColorA);
			Debug.DrawLine(actualWindowCorner2, actualWindowCorner3, xColorB);
			Debug.DrawLine(actualWindowCorner3, actualWindowCorner4, yColorB);
			Debug.DrawLine(actualWindowCorner4, actualWindowCorner1, xColorA);

			// And now we display the camera limits. If the camera is inactive, they will show in red.
			// There is an x in the middle of the screen to show what hits against the limit.
			
			if (_isCamMovementLimited)
			{
				Color limitColor;

				if (!_activeTracking)
					limitColor = Color.red;
				else
					limitColor = Color.cyan;

				Vector3 limitCorner1 = new Vector3(_leftLimit, _bottomLimit, 0);
				Vector3 limitCorner2 = new Vector3(_rightLimit, _bottomLimit, 0);
				Vector3 limitCorner3 = new Vector3(_rightLimit, _topLimit, 0);
				Vector3 limitCorner4 = new Vector3(_leftLimit, _topLimit, 0);

				Debug.DrawLine(limitCorner1, limitCorner2, limitColor);
				Debug.DrawLine(limitCorner2, limitCorner3, limitColor);
				Debug.DrawLine(limitCorner3, limitCorner4, limitColor);
				Debug.DrawLine(limitCorner4, limitCorner1, limitColor);

				//And a little center point

				Vector3 centerMarkCorner1 = new Vector3(cameraPos.x - 0.1f, cameraPos.y + 0.1f, 0);
				Vector3 centerMarkCorner2 = new Vector3(cameraPos.x + 0.1f, cameraPos.y - 0.1f, 0);
				Vector3 centerMarkCorner3 = new Vector3(cameraPos.x - 0.1f, cameraPos.y - 0.1f, 0);
				Vector3 centerMarkCorner4 = new Vector3(cameraPos.x + 0.1f, cameraPos.y + 0.1f, 0);

				Debug.DrawLine(centerMarkCorner1, centerMarkCorner2, Color.cyan);
				Debug.DrawLine(centerMarkCorner3, centerMarkCorner4, Color.cyan);
			}
		}

		public void ActivateLimits(int areaID, float leftLimit, float rightLimit, float bottomLimit, float topLimit)
		{
			_areaID = areaID;
			_leftLimit = leftLimit;
			_rightLimit = rightLimit;
			_bottomLimit = bottomLimit;
			_topLimit = topLimit;

			ValidateLeftAndRightLimits();
			ValidateTopAndBottomLimits();

			_isCamMovementLimited = true;
		}
        public void DeactivateLimits() => _isCamMovementLimited = false;

        public void MoveCamera(Vector3 targetPosition, float moveSpeed) => StartCoroutine(MoveToPosition(targetPosition, moveSpeed));

        IEnumerator MoveToPosition(Vector3 targetPosition, float moveSpeed)
		{
			_activeTracking = false;

			targetPosition.z = _transform.position.z;

			while (_transform.position != targetPosition)
			{
				_transform.position = Vector3.SmoothDamp(_transform.position, targetPosition, ref _smoothVelocity, _smoothTime);
				yield return 0;
			}
			_activeTracking = true;
		}
	}
}