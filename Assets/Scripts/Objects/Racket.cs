using System;
using UnityEngine;

namespace SimplePingPong
{
	[RequireComponent(typeof(BoxCollider2D))]
	public class Racket : MonoBehaviour
	{
		public PlayerSide Side;

		float speed;
		public float Speed
		{
			get => speed;
			set => speed = value;
		}
		
		float leftLimiter, rightLimiter;
		float newDirectionX;
		Vector2 buffVector = Vector2.zero;

		Transform _transform;

		void Awake()
		{
			_transform = GetComponent<Transform>();
		}

		public void SetLimiterExtent(float x)
		{
			rightLimiter = x;
			leftLimiter = -x;
		}

		public void Move(float ratio)
		{
			buffVector = Vector2.zero;
			buffVector.x = ratio * speed * 16;
			_transform.Translate(buffVector);

			buffVector = _transform.position;
			buffVector.x = Mathf.Clamp(buffVector.x, leftLimiter, rightLimiter);
			_transform.position = buffVector;
		}
	}
}