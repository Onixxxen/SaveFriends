using UnityEngine;

public class GoalPresenter 
{
    private GoalView _goalView;
    private Goal _goal;

    public void Init(GoalView goalView, Goal goal)
    {
        _goalView = goalView;
        _goal = goal;
    }

    public void Enable()
    {
        _goal.OnGiveGoal += TryGiveGoal;
        _goal.OnUpdateGoalBar += TryUpdateGoalBar;
        _goal.OnCompleteGoal += TryCompleteGoal;

        _goalView.OnGetGoal += TryGetGoal;
    }

    public void Disable()
    {
        _goal.OnGiveGoal -= TryGiveGoal;
        _goal.OnUpdateGoalBar -= TryUpdateGoalBar;
        _goal.OnCompleteGoal -= TryCompleteGoal;

        _goalView.OnGetGoal -= TryGetGoal;
    }

    private void TryGetGoal()
    {
        _goal.GiveGoal();
    }

    public void TryGiveGoal(int goal)
    {
        _goalView.UpdateGoal(goal);
    }

    private void TryUpdateGoalBar(int score)
    {
        _goalView.UpdateGoalBar(score);
    }

    private void TryCompleteGoal()
    {
        _goalView.CompleteGoal();
    }
}
