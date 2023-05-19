namespace Activity.DAL
{
    public class DalResult
    {
        public bool IsSuccess { get; set; }
        public int EffectRow { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public long NewRowId { get; set; }
        public object Data { get; set; }
    }
}