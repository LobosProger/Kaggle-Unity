using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PageSwitcherView : MonoBehaviour
{
	[SerializeField] private CanvasGroup currentPage;

	public void HidePageAndDisableRaycastInteracting()
	{
		currentPage.DOFade(0, 0.3f).SetEase(Ease.Linear);
		currentPage.blocksRaycasts = false;
	}

	public void ShowPageAndActivateRaycastInteracting()
	{
		currentPage.DOFade(1, 0.3f).SetEase(Ease.Linear);
		currentPage.blocksRaycasts = true;
	}

	public bool IsTransparencyOfPageIsZero() => currentPage.alpha == 0;
}
