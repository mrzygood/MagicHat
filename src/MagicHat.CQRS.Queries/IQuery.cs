namespace ClassLibrary1MagicHat.CQRS.Queries;

public interface IQuery
{
}

public interface IQuery<TResult> : IQuery
{
}
