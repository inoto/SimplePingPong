using System;
using UnityEngine;

namespace SimplePingPong
{
	public class GameUIController : MonoBehaviour
	{
		public static event Action<Color32> OnBallColorChangedEvent; 
		
		[SerializeField] GameObject PauseButton = null;
		[SerializeField] GameObject Menu = null;
		[SerializeField] ColorsGrid ColorsGrid = null;

		void Start()
		{
			ColorsGrid.Init();
		}

		// using by button
		public void PauseButtonPressed()
		{
			Time.timeScale = 0f;
			
			PauseButton.SetActive(false);
			Menu.SetActive(true);
			ColorsGrid.SelectColorCell(GameController.Instance.BallColor);
		}

		// using by button
		public void MenuBackgroundClicked()
		{
			Time.timeScale = 1f;
			
			Menu.SetActive(false);
			PauseButton.SetActive(true);
		}
		
		// using by button
		public void ColorCellClicked(ColorCell cell)
		{
			OnBallColorChangedEvent?.Invoke(cell.ColorImage.color);
		}
	}
}