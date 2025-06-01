using System;
using Shared.Scripts;
using TMPro;
using UnityEngine;

namespace UI.GameOverUI
{
    public class GameOverScore : MonoBehaviour
    {
        private TextMeshProUGUI _textComponent;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            ScoreKeeper.Instance.OnScoreChanged+= UpdateScore;
            _textComponent= GetComponent<TextMeshProUGUI>();
            _textComponent.text="Score: " + ScoreKeeper.Instance.Score.ToString("F2");
        }

        private void OnDestroy()
        {
            if (ScoreKeeper.Instance)
            {
                ScoreKeeper.Instance.OnScoreChanged -= UpdateScore;
            }
        }

        private void UpdateScore(double newScore)
        {
            _textComponent.text="Score: " + newScore;
        }
    }
}
