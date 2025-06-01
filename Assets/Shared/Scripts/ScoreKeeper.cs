using UnityEngine;

namespace Shared.Scripts
{
    public class ScoreKeeper : MonoBehaviour
    {
        private double _score = 0;

        public delegate void ScoreChangedHandler(double newScore);

        public event ScoreChangedHandler OnScoreChanged;
        public static ScoreKeeper Instance { get; private set; }
        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }

        public double Score
        {
            get => _score;
            set
            {
                _score = value;
                OnScoreChanged?.Invoke(_score);
            }
        }

        public void ResetScore()
        {
            Score = 0;
        }
    }
}