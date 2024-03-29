using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExerciseAnalyticsNamespace;

public class ExerciseAnalyticsModel : SaveableModel
{
	public const string keyToAccessDataForAnalytics = "Data completion by dates";
	public Dictionary<DateTime, ExerciseCompletionData> completionDataByDate = new Dictionary<DateTime, ExerciseCompletionData>();

	private void OnEnable()
	{
		ExerciseEvents.OnExerciseCompleted += AddCompletedExerciseByDate;
	}

	private void OnDisable()
	{
		ExerciseEvents.OnExerciseCompleted -= AddCompletedExerciseByDate;
	}

	protected override void ReadDataFromMemory()
	{
		string json = PlayerPrefs.GetString(keyToAccessDataForAnalytics);
		if (json != "")
		{
			completionDataByDate = JsonConvert.DeserializeObject<Dictionary<DateTime, ExerciseCompletionData>>(json);
		}
	}

	protected override void SaveDataInMemory()
	{
		string json = JsonConvert.SerializeObject(completionDataByDate);
		PlayerPrefs.SetString(keyToAccessDataForAnalytics, json);
	}

	private void AddCompletedExerciseByDate(DateTime dateTime, int totalExercisesCompleted, float exerciseDurationInSeconds)
	{
		ExerciseCompletionData completionData = new ExerciseCompletionData(totalExercisesCompleted, exerciseDurationInSeconds);
		completionDataByDate.Add(dateTime, completionData);
	}

	public ExerciseCompletionShowableData GetDayDataAnalytics() => new ExerciseCompletionShowableData(completionDataByDate, 4, 1);

	public ExerciseCompletionShowableData GetWeekDataAnalytics() => new ExerciseCompletionShowableData(completionDataByDate, 4, 7);

	public ExerciseCompletionShowableData GetMonthDataAnalytics() => new ExerciseCompletionShowableData(completionDataByDate, 4, 30);

	public ExerciseCompletionShowableData GetYearDataAnalytics() => new ExerciseCompletionShowableData(completionDataByDate, 4, 365);
}
