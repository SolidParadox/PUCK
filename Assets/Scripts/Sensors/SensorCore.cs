using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class SensorCore : MonoBehaviour {
  public List<GameObject> contacts = new List<GameObject>();
  
  public string targetTag;
  public enum TagMode { Free, Exclude, Include };
  public TagMode tagMode;

  public bool Breached { get; protected set; }

  protected virtual void Start () {
    contacts = new List<GameObject>();
  }

  protected virtual void LateUpdate () {
    CheckDeadContacts ();
  }

  private void CheckDeadContacts () {
    for ( int i = 0; i < contacts.Count; i++ ) {
      if ( contacts[i] == null ) {
        contacts.RemoveAt ( i );
        Breached = contacts.Count != 0;
        i--;
      }
    }
  }

  public bool CheckTag ( string alpha ) {
    if ( tagMode == TagMode.Free ) { return true; }
    if ( alpha == targetTag ) {
      return tagMode == TagMode.Include;
    }
    return tagMode == TagMode.Exclude;
  }

  public void AddContact ( GameObject alpha ) {
    if ( CheckTag ( alpha.tag ) && !contacts.Contains ( alpha ) ) {
      Breached = true;
      contacts.Add ( alpha );
      SensorBeaconWeight sbw;
      if ( alpha.TryGetComponent( out sbw ) ) {
        contacts.OrderBy ( c => Comparer<GameObject>.Create( ( x, y ) => { 
          SensorBeaconWeight bwX = x.GetComponent<SensorBeaconWeight>();  
          SensorBeaconWeight bwY = y.GetComponent<SensorBeaconWeight>();
          int wx = bwX == null ? 0 : bwX.weight;
          int wy = bwY == null ? 0 : bwY.weight;
          return wx > wy ? 1 : wx < wy ? -1 : 0;
        }  ) );
        // users.OrderBy ( u => Comparer<EnergyUser>.Create ( ( x, y ) => x.priority > y.priority ? 1 : x.priority < y.priority ? -1 : 0 ) );
      }
    }
  }

  public void RemoveContact ( GameObject alpha ) {
    if ( contacts.Contains ( alpha ) ) {
      contacts.Remove ( alpha );
      Breached = contacts.Count != 0;
    }
  }
}
