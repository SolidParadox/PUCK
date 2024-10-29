using UnityEngine;

public class SensorZone : SensorCore {
  public bool includeTriggers = false;

  private void OnTriggerEnter ( Collider other ) {
    if ( !includeTriggers && other.isTrigger ) { return; }
    AddContact ( other.gameObject );
  }

  private void OnTriggerExit ( Collider other ) {
    if ( !includeTriggers && other.isTrigger ) { return; }
    RemoveContact ( other.gameObject );
  }
}
