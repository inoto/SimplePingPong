using System;
using UnityEngine;

namespace SimplePingPong
{
	public class TouchController : MonoBehaviour
	{
		public static event Action<float> OnTouchEvent;
		
		Touch touch;
		float lastTouchPositionX = 0f;
		float ratio;

		void Start()
		{
#if UNITY_EDITOR
			Destroy(this);
#endif
		}
		
		void FixedUpdate()
		{
			if (Input.touchCount > 0)
			{
				touch = Input.GetTouch(0);
				if (lastTouchPositionX.Equals(0f))
					lastTouchPositionX = touch.position.x;
				else
				{
					ratio = (touch.position.x - lastTouchPositionX) / Screen.width;
					lastTouchPositionX = touch.position.x;
					OnTouchEvent?.Invoke(ratio);
				}
			}
			else
			{
				lastTouchPositionX = 0f;
			}
		}
	}
}