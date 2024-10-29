using UnityEngine;

public class PoolerAutoTimeout : MonoBehaviour {
  public float timeout;
  private float deltaT;

  private void OnEnable () {
    deltaT = timeout;
  }

  void Update () {
    deltaT -= Time.deltaTime;
    if (deltaT < 0) { 
      gameObject.SetActive(false);
    }
  }
}
