namespace Common.Basic.DDD
{
    public interface IEntity
    {
        string ID { get; }
        uint Version { get; set; }
    }
}
