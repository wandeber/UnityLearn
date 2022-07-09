using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UserData: MonoBehaviour {
  // ENCAPSULATION
  public static UserData Instance {get; private set;}
  public SavedData savedData;

  void Awake() {
    if (Instance != null) {
      Destroy(gameObject);
      return;
    }
    Instance = this;

    LoadData();
  }

  [System.Serializable]
  public class ScoreInfo {
    public string name = "";
    public int score = 0;
  }

  [System.Serializable]
  public class SavedData {
    public string name = "";
    public int lineCount = 6;
    public float paddleSpeed = 2.0f;
    public List<ScoreInfo> ranking = new List<ScoreInfo>();

    public ScoreInfo getHighscore() {
      return ranking.Count > 0 ? ranking[0] : null;
    }
  }

  public void SaveData() {
    string json = JsonUtility.ToJson(savedData);
    File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
  }

  public void LoadData() {
    string path = Application.persistentDataPath + "/savefile.json";
    if (File.Exists(path)) {
      string json = File.ReadAllText(path);
      savedData = JsonUtility.FromJson<SavedData>(json);
    }
    if (savedData == null) {
      savedData = new SavedData();
    }
  }
}
