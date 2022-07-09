using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu: MonoBehaviour {
  public TMP_InputField inputName;
  public TMP_Text bestScore;
  
  
  void Start() {
    UserData.ScoreInfo highscore = UserData.Instance.savedData.getHighscore();
    Debug.Log(UserData.Instance.savedData.name);
    if (UserData.Instance.savedData.name != "") {
      inputName.text = UserData.Instance.savedData.name;
    }
    Debug.Log(highscore);
    if (highscore != null) {
      bestScore.text = $"Best Score : {highscore.name} : {highscore.score}";
    }
  }

  void Update() {

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

  public void SettingsButtonClicked() {
    BeforeToLeave();
    SceneManager.LoadScene(3);
  }

  public void Exit() {
    BeforeToLeave();
#if UNITY_EDITOR
    EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
  }

  public void NameChanged() {}
}
