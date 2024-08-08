using UnityEngine;

public class PIDCore : MonoBehaviour {
  public float proportional;
  public float integral;
  public float derivative;

  private float integralSum = 0;
  private float lastError = 0;
  
  public void ResetError( float target, float current ) {
    lastError = target - current;
  }

  public float WorkFunction ( float target, float current, float deltaTime ) {
    float error = target - current;
    float pTerm = proportional * error;
    integralSum += error * deltaTime;
    float iTerm = integral * integralSum;
    float dTerm = derivative * (error - lastError) / deltaTime;

    lastError = error;
    float output = pTerm + iTerm + dTerm;

    return output;
  }
}