using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnergyCore : MonoBehaviour {
  public float maxEnergyDraw;
  public float currentDraw;

  public float overdrawStun;

  public List<EnergyUser> users;

  public float storedEnergy;

  public float mxStoredEnergyPlus;
  public float mxStoredEnergyMinus;

  public float drawPercentage { get { return currentDraw / maxEnergyDraw; } }

  private void Awake () {
    users = new List<EnergyUser> ();
  }

  public void RegisterUser ( EnergyUser user ) {
    users.Add ( user );
    users.OrderBy ( u => Comparer<EnergyUser>.Create ( ( x, y ) => x.priority > y.priority ? 1 : x.priority < y.priority ? -1 : 0 ) );
  }

  public void LateUpdate () {
    currentDraw = 0;
    for ( int i = 0; i < users.Count; i++ ) {
      currentDraw += users[i].draw;
      users[i].drawEfficiency = 1;

      if ( currentDraw > maxEnergyDraw ) {
        float overuse = currentDraw - maxEnergyDraw;

        users[i].drawEfficiency = 1 - Mathf.Clamp ( storedEnergy / mxStoredEnergyMinus, 0, 1 ) - overuse / users[i].draw;
        currentDraw = maxEnergyDraw;

        storedEnergy -= overuse * Time.deltaTime;
        if ( storedEnergy < mxStoredEnergyMinus ) {
          storedEnergy = mxStoredEnergyMinus;
        }
      }

      if ( currentDraw < 0 ) {
        float overuse = -currentDraw;

        users[i].drawEfficiency = 1 + Mathf.Clamp ( storedEnergy / mxStoredEnergyPlus, 0, 1 ) + overuse / -users[i].draw;
        currentDraw = 0;

        storedEnergy += overuse * Time.deltaTime;
        if ( storedEnergy > mxStoredEnergyPlus ) {
          storedEnergy = mxStoredEnergyPlus;
        }
      }
    }
  }
}
