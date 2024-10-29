using UnityEngine;
using UnityEngine.UI;

public class HUDEnergyUse : MonoBehaviour {
  public EnergyCore energyCore;
  public Slider slider;

  public Slider drawPos;
  public Slider drawNeg;

  public GameObject handlePos;
  public GameObject handleNeg;

  public float lerpStrength = 2;

  public float drawMagnitude = 1;

  private void LateUpdate () {
    
    slider.value = Mathf.Lerp ( slider.value, energyCore.drawPercentage, Time.deltaTime * lerpStrength );

    drawPos.value = Mathf.Lerp ( drawPos.value, energyCore.currentDraw < 0 ? -energyCore.currentDraw / drawMagnitude : 0, Time.deltaTime * lerpStrength );
    drawNeg.value = Mathf.Lerp ( drawNeg.value, energyCore.currentDraw > 0 ? energyCore.currentDraw / drawMagnitude : 0, Time.deltaTime * lerpStrength );

    handlePos.SetActive ( energyCore.currentDraw < 0 );
    handleNeg.SetActive ( energyCore.currentDraw > 0 );
  }
}
