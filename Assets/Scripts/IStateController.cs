namespace Game
{
    public interface IStateController
    {
        void ChangeState(IState nextState);
    }
}