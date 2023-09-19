public abstract class BaseState
{
    public Enemy enemy; //instance of enemy class

    public StateMachine stateMachine; //instance of statemachine class

    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();
}
