using UnityEngine;

public class EnergyUser : MonoBehaviour {
  public EnergyCore core;

  public float draw = 1;
  public int priority;

  public float drawEfficiency;

  private void Start () {
    if ( core != null ) core.RegisterUser ( this );
  }
}
