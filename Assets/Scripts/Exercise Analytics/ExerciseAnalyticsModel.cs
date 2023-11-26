using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseAnalyticsModel : MonoBehaviour
{
	public const string keyToAccessDataForAnalytics = "Data completion by dates";
	public Dictionary<DateTime, ExerciseCompletionData> completionDataByDate = new Dictionary<DateTime, ExerciseCompletionData>();

	private void Awake()
	{
		LoadCollectionForModelFromMemory();
	}

	private void OnEnable()
	{
		ExerciseEvents.OnExerciseCompleted += AddCompletedExerciseByDate;
	}

	private void OnDisable()
	{
		ExerciseEvents.OnExerciseCompleted -= AddCompletedExerciseByDate;
	}

	private void LoadCollectionForModelFromMemory()
	{
		string json = PlayerPrefs.GetString(keyToAccessDataForAnalytics);

		if (json != "")
		{
			completionDataByDate = JsonConvert.DeserializeObject<Dictionary<DateTime, ExerciseCompletionData>>(json);
		}
	}

	private void SaveNeedingForWorkCollectionInMemory()
	{
		string json = JsonConvert.SerializeObject(completionDataByDate);
		PlayerPrefs.SetString(keyToAccessDataForAnalytics, json);
	}

	private void AddCompletedExerciseByDate(DateTime dateTime, int totalExercisesCompleted, float exerciseDurationInSeconds)
	{
		ExerciseCompletionData completionData = new ExerciseCompletionData(totalExercisesCompleted, exerciseDurationInSeconds);
		completionDataByDate.Add(dateTime, completionData);
	}

	private void OnApplicationQuit()
	{
		SaveNeedingForWorkCollectionInMemory();
	}

	public ExerciseCompletionShowableData GetDataDataAnalytics() => new ExerciseCompletionShowableData(completionDataByDate, 4, 1);

	public ExerciseCompletionShowableData GetWeekDataAnalytics() => new ExerciseCompletionShowableData(completionDataByDate, 4, 7);

	public ExerciseCompletionShowableData GetMonthDataAnalytics() => new ExerciseCompletionShowableData(completionDataByDate, 4, 30);

	public ExerciseCompletionShowableData GetYearDataAnalytics() => new ExerciseCompletionShowableData(completionDataByDate, 4, 365);



	public class ExerciseCompletionData
	{
		public int totalExercisesCompleted;
		public float exerciseDurationInSeconds;

		public ExerciseCompletionData(int totalExercisesCompleted, float exerciseDurationInSeconds)
		{
			this.totalExercisesCompleted = totalExercisesCompleted;
			this.exerciseDurationInSeconds = exerciseDurationInSeconds;
		}
	}

	public class ExerciseCompletionShowableData
	{
		public const int maxScaleYForBarGraph = 10;
		public const int maxAmountOfExercises = 6;
		public const float maxAmountOfCompletionExerciseInMinutes = 10; // Maximum completion - 10 minutes - 600 seconds

		// Here we provide statistics for 3 bars in each bar graph - they will represent fillAmount property of image on UI
		private List<float> totalRegularityForEachBarGraph;
		private List<float> totalAverageTimeForEachBarGraph;
		private List<float> totalAverageDifficultyForEachBarGraph;

		private int amountOfShowableBarGraphs;

		public ExerciseCompletionShowableData(Dictionary<DateTime, ExerciseCompletionData> data, int amountOfShowableBarGraphs, int amountOfCollectableDaysForEachBarGraph)
		{
			this.amountOfShowableBarGraphs = amountOfShowableBarGraphs;
			totalRegularityForEachBarGraph = new List<float>(amountOfShowableBarGraphs);
			totalAverageTimeForEachBarGraph = new List<float>(amountOfShowableBarGraphs);
			totalAverageDifficultyForEachBarGraph = new List<float>(amountOfShowableBarGraphs);

			DateTime todayDay = DateTime.Now.Date;
			int offsetDay = 0;

			for (int i = 1; i <= amountOfShowableBarGraphs; i++)
			{
				float overallTimeForBarGraphInMinutes = 0; // Express in minutes, not seconds to avoid overflowing variable when calculating for months, years
				float overallDifficultyForBarGraph = 0; // Difficulty is about how many exercises completed
				int amountOfCompletedDaysWithExercise = 0; // Capture days ONLY of activity for correctly calculating values difficulty and consumed time

				// By this code below (cycle for), we capturing data from time scale which relates to bar graph
				// and capturing data also relates to dates, which are NOT capture, we just by logic of application
				// setuping code below and prepare needing variables
				int maxOffsetDay = offsetDay + amountOfCollectableDaysForEachBarGraph;
				for (; offsetDay < maxOffsetDay; offsetDay++)
				{
					DateTime offsetDate = todayDay.AddDays(-offsetDay);
					if (data.ContainsKey(offsetDate))
					{
						ExerciseCompletionData exerciseCompletionData = data[offsetDate];
						overallDifficultyForBarGraph += exerciseCompletionData.totalExercisesCompleted;
						overallTimeForBarGraphInMinutes += (exerciseCompletionData.exerciseDurationInSeconds / 60.0f); // Divide on 60 to express value in minutes, see comments above
						amountOfCompletedDaysWithExercise++;
					}
				}

				float regularityForBarGraph = (float)amountOfCompletedDaysWithExercise / amountOfCollectableDaysForEachBarGraph;
				float averageTimeForBarGraph = overallTimeForBarGraphInMinutes / (float)(amountOfCompletedDaysWithExercise * maxAmountOfCompletionExerciseInMinutes);
				float averageDifficultyForBarGraph = overallDifficultyForBarGraph / (float)(amountOfCompletedDaysWithExercise * maxAmountOfExercises);

				totalRegularityForEachBarGraph.Add(regularityForBarGraph);
				totalAverageTimeForEachBarGraph.Add(averageTimeForBarGraph);
				totalAverageDifficultyForEachBarGraph.Add(averageDifficultyForBarGraph);
			}
		}

		public float GetRegularityForBargraphByIndex(int index) => totalRegularityForEachBarGraph[index];

		public float GetTimeForBargraphByIndex(int index) => totalAverageTimeForEachBarGraph[index];

		public float GetDifficultyForBargraphByIndex(int index) => totalAverageDifficultyForEachBarGraph[index];

		public int GetAmountOfShowingBargraphs() => amountOfShowableBarGraphs;
	}
}
