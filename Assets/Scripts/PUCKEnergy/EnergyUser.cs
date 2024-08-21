using UnityEngine;

public class EnergyUser : MonoBehaviour {
  public EnergyCore core;

  public float draw = 1;
  public int priority;

  private void Start () {
    core.RegisterUser ( this );
  }
}
