using System;
using UnityEngine;

namespace SimplePingPong
{
	public class MouseController : MonoBehaviour
	{
		public static event Action<float> OnMouseMovedEvent;
		
		float lastMousePositionX = 0f;
		float ratio;

		void Start()
		{
#if !UNITY_EDITOR
			Destroy(this);
#endif
		}
		
		void FixedUpdate()
		{
			if (Input.GetMouseButton(0))
			{
				if (lastMousePositionX.Equals(0f))
					lastMousePositionX = Input.mousePosition.x;
				else
				{
					ratio = (Input.mousePosition.x - lastMousePositionX) / Screen.width;
					lastMousePositionX = Input.mousePosition.x;
					OnMouseMovedEvent?.Invoke(ratio);
				}
			}
			else
			{
				lastMousePositionX = 0f;
			}
		}
	}
}