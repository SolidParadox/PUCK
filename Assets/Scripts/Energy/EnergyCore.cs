using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnergyCore : MonoBehaviour {
  public List<EnergyUser> users;
  public float storedEnergy;

  public float efficiencyHyperOffset;
  public float efficiencyHypoOffset;

  public float efficiencyHyperBonus;
  public float efficiencyHypoBonus;

  public float mxStoredEnergyPlus;
  public float mxStoredEnergyMinus;
  
  public float currentDraw { get; private set; }
  public float drawPercentage { get { return ( storedEnergy - mxStoredEnergyMinus ) / ( mxStoredEnergyPlus - mxStoredEnergyMinus ); } }

  private void Awake () {
    users = new List<EnergyUser> ();
  }

  public void RegisterUser ( EnergyUser user ) {
    users.Add ( user );
    users.OrderBy ( u => Comparer<EnergyUser>.Create ( ( x, y ) => x.priority > y.priority ? 1 : x.priority < y.priority ? -1 : 0 ) );
  }

  private bool safeguardHYPER = true;
  private bool safeguardHYPO = true;

  private void Start () {
    if ( mxStoredEnergyPlus == efficiencyHyperOffset ) { safeguardHYPER = false; Debug.LogError ( "FUCKED HYPER EFFICIENCY PARAMTERS" ); }
    if ( mxStoredEnergyMinus == efficiencyHyperOffset ) { safeguardHYPO = false; Debug.LogError ( "FUCKED HYPO EFFICIENCY PARAMTERS" ); }
  }

  public void LateUpdate () {
    currentDraw = 0;
    for ( int i = 0; i < users.Count; i++ ) {
      if ( !users[i].enabled ) { continue; }
      currentDraw += users[i].draw;
      storedEnergy -= users[i].draw * Time.deltaTime;

      if ( storedEnergy < mxStoredEnergyMinus ) {
        storedEnergy = mxStoredEnergyMinus;
      }
      if ( storedEnergy > mxStoredEnergyPlus ) {
        storedEnergy = mxStoredEnergyPlus;
      }

      users[i].drawEfficiency = 1;
      if ( safeguardHYPER && storedEnergy > efficiencyHyperOffset ) {
        users[i].drawEfficiency += efficiencyHyperBonus * ( storedEnergy - efficiencyHyperOffset ) / Mathf.Max( mxStoredEnergyPlus - efficiencyHyperOffset );
      }
      if ( safeguardHYPO && storedEnergy < efficiencyHypoOffset ) {
        users[i].drawEfficiency += efficiencyHypoBonus * ( efficiencyHypoOffset - storedEnergy ) / Mathf.Max ( mxStoredEnergyMinus - efficiencyHypoOffset );
      }
    }
  }
}
