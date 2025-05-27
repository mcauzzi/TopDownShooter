using System.Globalization;
using System.Linq;
using SharedScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InGameUI : MonoBehaviour
    {
        private Slider _healthSlider;

        private TextMeshProUGUI _scoreText;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            var player = Resources.FindObjectsOfTypeAll<HealthManager>().First(x=>x.CompareTag("Player"));
            player.onHealthChanged+= UpdateHealthUI;
            _healthSlider = GetComponentInChildren<Slider>();
            _healthSlider.maxValue= player.Health;
            _healthSlider.value = player.Health;
            _scoreText= GetComponentInChildren<TextMeshProUGUI>();
            var scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
            if (scoreKeeper)
            {
                scoreKeeper.OnScoreChanged += UpdateScoreUI;
                UpdateScoreUI(scoreKeeper.Score);
            }
            else
            {
                Debug.LogWarning("No ScoreKeeper found in the scene.");
            }
        }

        private void UpdateScoreUI(double newScore)
        {
            if (_scoreText)
            {
                _scoreText.text = newScore.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                Debug.LogWarning("Score TextMeshProUGUI component not found in the InGameUI.");
            }
        }

        private void UpdateHealthUI(int newHealth)
        {
            _healthSlider.value = newHealth;
        }


        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
