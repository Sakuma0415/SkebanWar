using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Select : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private GameObject yesButtonObj;
    private GameObject noButtonObj;
    private Button yesButton;
    private Button noButton;

    // 選択表示を取得
    private IEnumerator Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        yesButtonObj = transform.Find("YesButton").gameObject;
        yesButton = yesButtonObj.GetComponent<Button>();
        noButtonObj = transform.Find("NoButton").gameObject;
        noButton = noButtonObj.GetComponent<Button>();
        yesButton.enabled = false;
        noButton.enabled = false;

        yield return null;

        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public IEnumerator ShowWindow(string textToDisplay)
    {
        // ウィンドウを表示
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        // ボタンを有効化
        yesButton.enabled = true;
        noButton.enabled = true;
        // noButtonにフォーカス
        EventSystem.current.SetSelectedGameObject(noButtonObj);
        // ウィンドウに指定されたテキストを表示
        transform.Find("SelectText").gameObject.GetComponent<Text>().text = textToDisplay;

        GameObject currentSelected;

        // インタラクトとウィンドウの同時に開かないようにする
        yield return null;

        // 現在選択中のゲームオブジェクト
        currentSelected = EventSystem.current.currentSelectedGameObject;

        // 押したボタンがYesかどうか?
        yield return currentSelected == yesButtonObj;
    }
    public void HideWindow()
    {
        // ウィンドウを非表示
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        // ボタンを有効化
        yesButton.enabled = false;
        noButton.enabled = false;
    }
}
