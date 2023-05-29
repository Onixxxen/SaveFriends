using UnityEngine;

public class Improvements
{
    private MenuPlayer _menuPlayer;

    public Improvements(MenuPlayer menuPlayer)
    {
        _menuPlayer = menuPlayer;
    }

    public void Improve(ImprovementsView improvementsView, int cost, int buyed, int improveHeart, int improveMoney, int improveSocore)
    {
        _menuPlayer.ChangeValues(improvementsView, cost, buyed, improveHeart, improveMoney, improveSocore);
    }
}
