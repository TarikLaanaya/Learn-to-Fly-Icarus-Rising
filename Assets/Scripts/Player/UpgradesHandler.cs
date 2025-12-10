using UnityEngine;

public class UpgradesHandler : MonoBehaviour
{
    // Create a struct to handle each iteration of wings or boost (very modular)
    [System.Serializable]
    public struct WingUpgrade 
    {
        public float gravity;
        public float maxSpeed;
        public float speedFactor;
        public float deceleration;
    }

    public WingUpgrade[] wingUpgrades;

    [System.Serializable]
    public struct BoostUpgrade 
    {
        public float boostStrength;
    }

    public BoostUpgrade[] boostUpgrades;
}