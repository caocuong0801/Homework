public interface IStoreObjectModel : IBaseModel
{
    bool IsAvaiable { get; set; }
    int Cost { get; set; }
    int Countdown { get; set; }
    float Progress { get; set; }
}
