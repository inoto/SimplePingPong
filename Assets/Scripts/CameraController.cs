using UnityEngine;

namespace SimplePingPong
{
	public class CameraController : MonoBehaviour
	{
		Camera _camera;

		void Awake()
		{
			_camera = GetComponent<Camera>();
		}

		public void SetCameraSize(Vector2 fieldSize)
		{
			if (Screen.width / (float) Screen.height <= fieldSize.x / fieldSize.y)
				_camera.orthographicSize = (fieldSize.x * Screen.height) / (Screen.width * 2f);
			else
				_camera.orthographicSize = (fieldSize.y / 2f);
		}
	}
}