using HistoricalApp.Models;
using HistoricalApp.Services;

namespace HistoricalApp.Tests;

public class UserAndDailyMissionTests
{
    [Fact]
    public void NewUser_HighestScore_DefaultsToZero()
    {
        var user = new User();

        Assert.Equal(0, user.HighestScore);
    }

    [Fact]
    public void CheckMissionProgress_UpdatesHighestScoreToday_WhenScoreIsHigher()
    {
        var user = new User
        {
            DailyMission1Id = "complete_1_quiz",
            HighestScoreToday = 120
        };

        var quiz = CreateQuiz(points: 50, category: "Events");

        DailyMissionService.CheckMissionProgress(user, quiz, score: 180, totalQuestions: 5);

        Assert.Equal(180, user.HighestScoreToday);
    }

    [Fact]
    public void CheckMissionProgress_DoesNotLowerHighestScoreToday_WhenScoreIsLower()
    {
        var user = new User
        {
            DailyMission1Id = "complete_1_quiz",
            HighestScoreToday = 220
        };

        var quiz = CreateQuiz(points: 50, category: "Events");

        DailyMissionService.CheckMissionProgress(user, quiz, score: 140, totalQuestions: 5);

        Assert.Equal(220, user.HighestScoreToday);
    }

    [Fact]
    public void GetDailyMissions_ResetsHighestScoreToday_OnNewDay()
    {
        var user = new User
        {
            HighestScoreToday = 300,
            LastDailyReset = DateTime.UtcNow.AddDays(-1)
        };

        var missions = DailyMissionService.GetDailyMissions(user);

        Assert.Equal(0, user.HighestScoreToday);
        Assert.Single(missions);
    }

    [Fact]
    public void GetDailyMissions_ResetsDailyTrackingFields_OnNewDay()
    {
        var user = new User
        {
            QuizzesCompletedToday = 9,
            CompletedTimedQuizToday = true,
            LastPlayedCategory = "Battles",
            LastDailyReset = DateTime.UtcNow.AddDays(-1)
        };

        DailyMissionService.GetDailyMissions(user);

        Assert.Equal(0, user.QuizzesCompletedToday);
        Assert.False(user.CompletedTimedQuizToday);
        Assert.Equal(string.Empty, user.LastPlayedCategory);
    }

    [Fact]
    public void CheckMissionProgress_CompleteOneQuizMission_AwardsCoinsAndMarksDone()
    {
        var user = new User { DailyMission1Id = "complete_1_quiz", Currency = 10 };
        var quiz = CreateQuiz(points: 20, category: "Events");

        var coins = DailyMissionService.CheckMissionProgress(user, quiz, score: 20, totalQuestions: 1);

        Assert.Equal(25, coins);
        Assert.True(user.DailyMission1Done);
        Assert.Equal(35, user.Currency);
    }

    [Fact]
    public void CheckMissionProgress_CompleteThreeQuizzesMission_CompletesOnThirdAttempt()
    {
        var user = new User { DailyMission1Id = "complete_3_quizzes", QuizzesCompletedToday = 2, Currency = 0 };
        var quiz = CreateQuiz(points: 30, category: "Events");

        var coins = DailyMissionService.CheckMissionProgress(user, quiz, score: 10, totalQuestions: 1);

        Assert.Equal(75, coins);
        Assert.True(user.DailyMission1Done);
        Assert.Equal(75, user.Currency);
    }

    [Fact]
    public void CheckMissionProgress_CompleteThreeQuizzesMission_DoesNotCompleteBeforeThirdQuiz()
    {
        var user = new User { DailyMission1Id = "complete_3_quizzes", QuizzesCompletedToday = 1, Currency = 0 };
        var quiz = CreateQuiz(points: 30, category: "Events");

        var coins = DailyMissionService.CheckMissionProgress(user, quiz, score: 10, totalQuestions: 1);

        Assert.Equal(0, coins);
        Assert.False(user.DailyMission1Done);
        Assert.Equal(0, user.Currency);
    }

    [Fact]
    public void CheckMissionProgress_PerfectScoreMission_CompletesWhenScoreMeetsMaximum()
    {
        var user = new User { DailyMission1Id = "perfect_score", Currency = 5 };
        var quiz = CreateQuiz(points: 10, category: "People");

        var coins = DailyMissionService.CheckMissionProgress(user, quiz, score: 50, totalQuestions: 5);

        Assert.Equal(100, coins);
        Assert.True(user.DailyMission1Done);
        Assert.Equal(105, user.Currency);
    }

    [Fact]
    public void CheckMissionProgress_PerfectScoreMission_DoesNotCompleteWhenNotPerfect()
    {
        var user = new User { DailyMission1Id = "perfect_score", Currency = 5 };
        var quiz = CreateQuiz(points: 10, category: "People");

        var coins = DailyMissionService.CheckMissionProgress(user, quiz, score: 49, totalQuestions: 5);

        Assert.Equal(0, coins);
        Assert.False(user.DailyMission1Done);
        Assert.Equal(5, user.Currency);
    }

    [Fact]
    public void CheckMissionProgress_CompleteTimedMission_CompletesForTimedQuiz()
    {
        var user = new User { DailyMission1Id = "complete_timed", Currency = 7 };
        var quiz = CreateQuiz(points: 20, category: "Events", isTimeBased: true);

        var coins = DailyMissionService.CheckMissionProgress(user, quiz, score: 20, totalQuestions: 1);

        Assert.True(user.CompletedTimedQuizToday);
        Assert.True(user.DailyMission1Done);
        Assert.Equal(50, coins);
        Assert.Equal(57, user.Currency);
    }

    [Fact]
    public void CheckMissionProgress_CompleteTimedMission_DoesNotCompleteForUntimedQuiz()
    {
        var user = new User { DailyMission1Id = "complete_timed", Currency = 7 };
        var quiz = CreateQuiz(points: 20, category: "Events", isTimeBased: false);

        var coins = DailyMissionService.CheckMissionProgress(user, quiz, score: 20, totalQuestions: 1);

        Assert.False(user.CompletedTimedQuizToday);
        Assert.False(user.DailyMission1Done);
        Assert.Equal(0, coins);
        Assert.Equal(7, user.Currency);
    }

    [Fact]
    public void CheckMissionProgress_PlayBattlesMission_CompletesCaseInsensitiveMatch()
    {
        var user = new User { DailyMission1Id = "play_battles", Currency = 3 };
        var quiz = CreateQuiz(points: 20, category: "bAtTlEs");

        var coins = DailyMissionService.CheckMissionProgress(user, quiz, score: 20, totalQuestions: 1);

        Assert.True(user.DailyMission1Done);
        Assert.Equal(30, coins);
        Assert.Equal(33, user.Currency);
    }

    [Fact]
    public void CheckMissionProgress_PlayBattlesMission_DoesNotCompleteForOtherCategory()
    {
        var user = new User { DailyMission1Id = "play_battles", Currency = 3 };
        var quiz = CreateQuiz(points: 20, category: "Explorer");

        var coins = DailyMissionService.CheckMissionProgress(user, quiz, score: 20, totalQuestions: 1);

        Assert.False(user.DailyMission1Done);
        Assert.Equal(0, coins);
        Assert.Equal(3, user.Currency);
    }

    [Fact]
    public void CheckMissionProgress_DoesNotRewardTwice_WhenMissionAlreadyDone()
    {
        var user = new User
        {
            DailyMission1Id = "complete_1_quiz",
            DailyMission1Done = true,
            Currency = 40
        };
        var quiz = CreateQuiz(points: 20, category: "Events");

        var coins = DailyMissionService.CheckMissionProgress(user, quiz, score: 20, totalQuestions: 1);

        Assert.Equal(0, coins);
        Assert.Equal(40, user.Currency);
    }

    [Fact]
    public void CheckMissionProgress_TracksLastPlayedCategoryAndQuizCount()
    {
        var user = new User { DailyMission1Id = "complete_3_quizzes", QuizzesCompletedToday = 0 };
        var quiz = CreateQuiz(points: 10, category: "People");

        DailyMissionService.CheckMissionProgress(user, quiz, score: 5, totalQuestions: 1);

        Assert.Equal(1, user.QuizzesCompletedToday);
        Assert.Equal("People", user.LastPlayedCategory);
    }

    private static Quiz CreateQuiz(int points, string category, bool isTimeBased = false)
    {
        return new Quiz
        {
            Title = "Test Quiz",
            Description = "Test Description",
            Difficulty = "Easy",
            Category = category,
            Points = points,
            IsTimeBased = isTimeBased,
            Questions = new List<Question>()
        };
    }
}

public class RankCalculatorTests
{
    [Theory]
    [InlineData(0, "Bronze")]
    [InlineData(99, "Bronze")]
    [InlineData(100, "Silver")]
    [InlineData(249, "Silver")]
    [InlineData(250, "Gold")]
    [InlineData(499, "Gold")]
    [InlineData(500, "Diamond")]
    [InlineData(999, "Diamond")]
    [InlineData(1000, "Historian")]
    public void GetRankFromPoints_ReturnsExpectedRankAtThresholds(int points, string expectedRank)
    {
        var rank = RankCalculator.GetRankFromPoints(points);
        Assert.Equal(expectedRank, rank);
    }
}

public class UserTests
{
    [Fact]
    public void RecalculateRank_UsesTotalPointsToUpdateRank()
    {
        var user = new User
        {
            TotalPoints = 530
        };

        user.RecalculateRank();

        Assert.Equal("Diamond", user.Rank);
    }
}
