namespace BarsGroupProjectN1.Core.Contracts;

public record OrderTaskExecution<T>
{
    public T? Executer { get; set; }
    public TimeSpan Time { get; set; }
}