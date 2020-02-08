using System.Collections.Generic;
using UnityEngine;

namespace SimplePingPong
{
	public class SinglePlayerGame : GameController
	{
		List<Racket> players = new List<Racket>();

		protected override void Awake()
		{
			base.Awake();

			players.Add(TopPlayer);
			players.Add(BottomPlayer);
		}

		void Start()
		{
			RestartGame();
			SubscribeEvents();
			StartGame();
		}

		void SubscribeEvents()
		{
			MouseController.OnMouseMovedEvent += OnInput;
			TouchController.OnTouchEvent += OnInput;
			TopGate.OnGoal += OnGoal;
			BottomGate.OnGoal += OnGoal;
		}

		void UnSubscribeEvents()
		{
			MouseController.OnMouseMovedEvent -= OnInput;
			TouchController.OnTouchEvent -= OnInput;
			TopGate.OnGoal -= OnGoal;
			BottomGate.OnGoal -= OnGoal;
		}

		void OnGoal(PlayerSide side)
		{
			if (side == PlayerSide.Bottom)
			{
				TopScore++;
				if (TopScore > BestTopScore)
				{
					BestTopScore = TopScore;
					RaiseOnBestScoresUpdatedEvent();
				}
			}
			else if (side == PlayerSide.Top)
			{
				BottomScore++;
				if (BottomScore > BestBottomScore)
				{
					BestBottomScore = BottomScore;
					RaiseOnBestScoresUpdatedEvent();
				}
			}

			ResetBall();
		}

		void CheckGameOver()
		{
			if (TopScore >= Config.ScoresToWin || BottomScore >= Config.ScoresToWin)
			{
				GameOver();
			}
			else
			{
				ResetBall();
			}
		}

		void StartGame()
		{
			GameStarted = true;
			ResetBall();
		}

		void OnInput(float ratio)
		{
			if (!GameStarted) return;

			foreach (var player in players)
			{
				player.Move(ratio);
			}
		}

		void OnDestroy()
		{
			UnSubscribeEvents();
		}
	}
}