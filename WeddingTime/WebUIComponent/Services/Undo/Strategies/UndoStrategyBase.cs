namespace AIT.WebUIComponent.Services.Undo.Strategies
{
    public abstract class UndoStrategyBase<TInput>
    {
        public object Process(object input)
        {
            return Process((TInput)input);
        }

        protected abstract object Process(TInput input);
    }
}