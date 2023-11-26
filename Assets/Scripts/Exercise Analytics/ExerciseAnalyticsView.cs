using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ExerciseAnalyticsModel;

public class ExerciseAnalyticsView : MonoBehaviour
{
    [SerializeField] private BarView barPrefab;
    [SerializeField] private Transform barsPanel;

	public void ShowAnalyticsOnBarCharts(ExerciseCompletionShowableData exerciseCompletionData)
	{
		ClearPanelOfBarChart();
		for (int i=0; i < exerciseCompletionData.GetAmountOfShowingBargraphs(); i++)
		{
			float regularityFillAmount = exerciseCompletionData.GetRegularityForBargraphByIndex(i);
			float timeFillAmount = exerciseCompletionData.GetTimeForBargraphByIndex(i);
			float difficultyFillAmount = exerciseCompletionData.GetDifficultyForBargraphByIndex(i);
			
			CreateBarChart(regularityFillAmount, timeFillAmount, difficultyFillAmount, $"Week{i+1}");
		}
	}

	private void CreateBarChart(float regularityValue, float timeValue, float difficultyValue, string titleOfBar)
	{
		BarView barView = Instantiate(barPrefab, barsPanel);
		barView.ShowDataOnBar(regularityValue, timeValue, difficultyValue, titleOfBar);
	}

	private void ClearPanelOfBarChart()
	{
		foreach(Transform eachBar in barsPanel)
		{
			Destroy(eachBar.gameObject);
		}
	}
}
