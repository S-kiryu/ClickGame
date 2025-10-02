using UnityEngine;
using UnityEngine.UI;

public class ConsecutiveHits : MonoBehaviour
{
    // Constants
    private const int MIN_RANDOM = 1;
    private const int MAX_RANDOM = 101;

    // Level System
    [Header("Level System")]
    [SerializeField] private int _level = 1;
    [SerializeField] private float _levelUpExp = 10f;
    [SerializeField] private float _expIncreaseRate = 1.5f;
    [SerializeField] private float _baseExpGain = 1f;
    private float _currentExp = 0;

    // Combat Stats
    [Header("Combat Rates (%)")]
    [Tooltip("クリティカル率")]
    [SerializeField] private int _criticalRate = 1;
    [Tooltip("渾身率")]
    [SerializeField] private int _powerfulRate = 1;

    [Header("Damage Multipliers")]
    [Tooltip("クリティカルダメージ")]
    [SerializeField] private float _criticalDamage = 2.0f;
    [Tooltip("渾身ダメージ")]
    [SerializeField] private float _powerfulDamage = 2.0f;

    // UI
    [Header("UI")]
    [SerializeField] private Text _statusText;

    private void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        HandleInput();
    }

    #region Input Handling
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddExperience();
            CheckLevelUp();
            UpdateUI();
        }
    }
    #endregion

    #region Experience System
    private void AddExperience()
    {
        float multiplier = 1f;
        bool isPowerful = CheckRate(_powerfulRate);
        bool isCritical = CheckRate(_criticalRate);

        if (isPowerful) multiplier *= _powerfulDamage;
        if (isCritical) multiplier *= _criticalDamage;

        LogCombatResult(isPowerful, isCritical);

        _currentExp += _baseExpGain * multiplier;
    }

    private void LogCombatResult(bool isPowerful, bool isCritical)
    {
        if (isPowerful && isCritical)
        {
            Debug.Log("渾身クリティカル発生！");
        }
        else if (isPowerful)
        {
            Debug.Log("渾身発生");
        }
        else if (isCritical)
        {
            Debug.Log("クリティカル発生");
        }
    }
    #endregion

    #region Level System
    private void CheckLevelUp()
    {
        if (_currentExp >= _levelUpExp)
        {
            _level++;
            _currentExp -= _levelUpExp;
            _levelUpExp *= _expIncreaseRate;

            Debug.Log($"レベルアップ！ Lv.{_level}");
        }
    }
    #endregion

    #region UI
    private void UpdateUI()
    {
        if (_statusText != null)
        {
            _statusText.text = $"Lv.{_level}\n{_currentExp:F1}/{_levelUpExp:F1}";
        }
    }
    #endregion

    #region Helper Methods
    /// <summary>
    /// 指定された確率で成功判定を行う
    /// </summary>
    /// <param name="rate">成功率（1-100）</param>
    /// <returns>成功した場合true</returns>
    private bool CheckRate(int rate)
    {
        int value = Random.Range(MIN_RANDOM, MAX_RANDOM);
        return rate >= value;
    }
    #endregion
}