using System;
using TMPro;
using UnityEngine;

namespace SimplePingPong
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class BestScoreText : MonoBehaviour
	{
		public PlayerSide Side;

		TextMeshProUGUI _textMeshPro;

		void Awake()
		{
			_textMeshPro = GetComponent<TextMeshProUGUI>();
		}

		void OnEnable()
		{
			GameController.OnBestScoresUpdatedEvent += GameControllerOnBestScoresUpdatedEvent;
		}

		void OnDisable()
		{
			GameController.OnBestScoresUpdatedEvent -= GameControllerOnBestScoresUpdatedEvent;
		}

		void GameControllerOnBestScoresUpdatedEvent(int bestTopScore, int bestBottomScore)
		{
			if (Side == PlayerSide.Top)
				_textMeshPro.text = bestTopScore.ToString();
			else if (Side == PlayerSide.Bottom)
				_textMeshPro.text = bestBottomScore.ToString();
		}
	}
}