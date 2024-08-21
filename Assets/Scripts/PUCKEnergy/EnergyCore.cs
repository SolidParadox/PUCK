using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnergyCore : MonoBehaviour {
  public float maxEnergyDraw;
  
  public float maxOverdrawTime;
  public float overdrawMultiplier;

  public float overdrawStun;

  public float currentDraw;

  public List<EnergyUser> users;

  public float drawPercentage { get { return currentDraw / maxEnergyDraw; } }

  private void Awake () {
    users = new List<EnergyUser> ();
  }

  public void RegisterUser ( EnergyUser user ) {
    users.Add ( user );
    users.OrderBy ( u => Comparer<EnergyUser>.Create ( ( x, y ) => x.priority > y.priority ? 1 : x.priority < y.priority ? -1 : 0 ) );
  }

  public void LateUpdate () {
    for ( int i = 0; i < users.Count; i++ ) {
      currentDraw = users[i].draw;
    }
  }
}
