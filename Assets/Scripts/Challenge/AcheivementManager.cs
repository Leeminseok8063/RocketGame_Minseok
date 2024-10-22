using System.Linq;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;
    public GameObject achievementsCanvs;
    private int currentThresholdIndex;

    [SerializeField] private AchievementSO[] achievements;
    [SerializeField] private AchievementView achievementView;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        achievementView.CreateAchievementSlots(achievements);  // UI 생성
        RocketMovementC.OnHighScoreChanged += CheckAchievement;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            achievementsCanvs.SetActive(!achievementsCanvs.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Application.Quit();
        }
    }

    // 최고 높이를 달성했을 때 업적 달성 판단, 이벤트 기반으로 설계할 것
    private void CheckAchievement(float height)
    {
        if (height > 100)
        {
            achievementView.UnlockAchievement(1);
        }
        if (height > 200)
        {
            achievementView.UnlockAchievement(2);
        }
        if (height > 1000)
        {
            achievementView.UnlockAchievement(3);
        }
    }
}