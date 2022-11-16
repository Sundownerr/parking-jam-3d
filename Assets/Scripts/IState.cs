namespace Game
{
    public interface IState
    {
      
    }

    public interface IEnterState : IState
    {
        void Enter();
    }

    public interface IExitState : IState
    {
        void Exit();
    }

    public interface IUpdateState : IState
    {
        void Update();
    }
}