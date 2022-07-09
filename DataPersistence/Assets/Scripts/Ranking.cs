using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ranking: MonoBehaviour {
  public TMP_Text[] rankingTexts;
  
  
  void Start() {
    for (int i = 0; i < rankingTexts.Length; i++) {
      if (UserData.Instance.savedData.ranking.Count - 1 >= i && UserData.Instance.savedData.ranking[i] != null) {
        rankingTexts[i].text = (i+1) +". "+ UserData.Instance.savedData.ranking[i].name +" - "+ UserData.Instance.savedData.ranking[i].score;
      }
    }
  }

  public void BackButtonClicked() {
    SceneManager.LoadScene(0);
  }
}
