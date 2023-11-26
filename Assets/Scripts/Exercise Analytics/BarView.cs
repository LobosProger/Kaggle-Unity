using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarView : MonoBehaviour
{
    [SerializeField] private Image reqularityBar;
    [SerializeField] private Image timeBar;
    [SerializeField] private Image difficultyBar;
    [SerializeField] private TMP_Text titleOfBar;

	public void ShowDataOnBar(float regularityValue,  float timeValue, float difficultyValue, string titleOfBar)
    {
        reqularityBar.fillAmount = regularityValue;
        timeBar.fillAmount = timeValue;
        difficultyBar.fillAmount = difficultyValue;
		this.titleOfBar.text = titleOfBar;
	}
}
