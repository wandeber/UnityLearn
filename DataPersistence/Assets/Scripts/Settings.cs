using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Settings: MonoBehaviour {
  public Slider sliderLines;
  public Slider sliderPaddleSpeed;
  
  
  void Start() {
    sliderLines.value = UserData.Instance.savedData.lineCount;
    sliderPaddleSpeed.value = UserData.Instance.savedData.paddleSpeed;
  }


  private void BeforeToLeave() {
    if (UserData.Instance.savedData.lineCount != sliderLines.value) {
      UserData.Instance.savedData.lineCount = (int)sliderLines.value;
    }
    if (UserData.Instance.savedData.paddleSpeed != sliderPaddleSpeed.value) {
      UserData.Instance.savedData.paddleSpeed = sliderPaddleSpeed.value;
    }
    UserData.Instance.SaveData();
  }

  public void BackButtonClicked() {
    BeforeToLeave();
    SceneManager.LoadScene(0);
  }
}
