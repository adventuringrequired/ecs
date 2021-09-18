using UnityEngine;

namespace Simulations.Garden
{
    [CreateAssetMenu(menuName = "Simulations/Garden/Plant", fileName = "NewPlant")]
    public class PlantObject : ScriptableObject
    {
        [SerializeField]
        private string plantName;

        [SerializeField]
        [Tooltip("In seconds")]
        private float totalTimeToGrow = 10f;

        [SerializeField]
        private float finalSize = 1f;

        [SerializeField]
        private Color startColor = Color.black;

        [SerializeField]
        private Color finalColor = Color.green;

        public string PlantName { get => plantName; set => plantName = value; }
        public float TotalTimeToGrow { get => totalTimeToGrow; set => totalTimeToGrow = value; }
        public float FinalSize { get => finalSize; set => finalSize = value; }
        public Color StartColor { get => startColor; set => startColor = value; }
        public Color FinalColor { get => finalColor; set => finalColor = value; }
    }
}
