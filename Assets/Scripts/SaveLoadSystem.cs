using System;
using UnityEngine;

namespace SimplePingPong
{
	public class SaveLoadSystem : MonoBehaviour
	{
		public static event Action<int, int> OnScoresLoadedEvent;
		public static event Action<Color32> OnBallColorLoadedEvent;

		void Start()
		{
			int bestTopScore = PlayerPrefs.GetInt("BestTopScore");
			int bestBottomScore = PlayerPrefs.GetInt("BestBottomScore");
			
			OnScoresLoadedEvent?.Invoke(bestTopScore, bestBottomScore);

			string colorString = PlayerPrefs.GetString("BallColor");
			Color color;
			if (ColorUtility.TryParseHtmlString(colorString, out color))
				OnBallColorLoadedEvent?.Invoke(color);
		}

		void OnEnable()
		{
			GameController.OnBestScoresUpdatedEvent += GameControllerOnBestScoresUpdatedEvent;
			GameUIController.OnBallColorChangedEvent += GameUIControllerOnBallColorChangedEvent;
		}
		
		void OnDisable()
		{
			GameController.OnBestScoresUpdatedEvent -= GameControllerOnBestScoresUpdatedEvent;
			GameUIController.OnBallColorChangedEvent -= GameUIControllerOnBallColorChangedEvent;
		}
		
		void GameUIControllerOnBallColorChangedEvent(Color32 color)
		{
			PlayerPrefs.SetString("BallColor", "#" + ColorUtility.ToHtmlStringRGBA(color));
			PlayerPrefs.Save();
		}

		void GameControllerOnBestScoresUpdatedEvent(int bestTopScore, int bestBottomScore)
		{
			PlayerPrefs.SetInt("BestTopScore", bestTopScore);
			PlayerPrefs.SetInt("BestBottomScore", bestBottomScore);
			PlayerPrefs.Save();
		}
	}
}