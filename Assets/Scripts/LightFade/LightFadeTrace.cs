using UnityEngine;

public class LightFadeTrace : MonoBehaviour {
  public float fadeTime;
  private float time;

  public bool lit { get { return time > 0; } }

  private void OnTriggerEnter ( Collider other ) {
    time = fadeTime;
  }

  private void Update () {
    
  }
}
 