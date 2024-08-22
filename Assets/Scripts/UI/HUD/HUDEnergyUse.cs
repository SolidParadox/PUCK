using UnityEngine;
using UnityEngine.UI;

public class HUDEnergyUse : MonoBehaviour {
  public EnergyCore energyCore;
  public Slider slider;

  public float lerpStrength = 2;

  private void LateUpdate () {
    slider.value = Mathf.Lerp ( slider.value, 1 + energyCore.drawPercentage, Time.deltaTime * lerpStrength );
  }
}
