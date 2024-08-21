using UnityEngine;
using UnityEngine.UI;

public class HUDEnergyUse : MonoBehaviour {
  public EnergyCore energyCore;
  public Slider slider;

  private void LateUpdate () {
    slider.value = energyCore.drawPercentage;
  }
}
