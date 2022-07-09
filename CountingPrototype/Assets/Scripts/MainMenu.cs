using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu: MonoBehaviour {
  public TMP_InputField inputName;
  public Button btnStart;


  private void Awake() {
    btnStart.enabled = false;
  }

  void Start() {
    if (UserData.Instance.savedData.name != "") {
      inputName.text = UserData.Instance.savedData.name;
    }
    ControlStartAvailability();
  }

  void ControlStartAvailability() {
    btnStart.enabled = inputName.text == "" ? false : true;
  }

  private void BeforeToLeave() {
    if (UserData.Instance.savedData.name != inputName.text) {
      UserData.Instance.savedData.name = inputName.text;
      UserData.Instance.SaveData();
    }
  }
  public void StartButtonClicked() {
    BeforeToLeave();
    SceneManager.LoadScene(1);
  }

  public void RankingButtonClicked() {
    BeforeToLeave();
    SceneManager.LoadScene(2);
  }

  public void ExitButtonClicked() {
    BeforeToLeave();
#if UNITY_EDITOR
    EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
  }

  public void NameChanged() {
    ControlStartAvailability();
  }
}
