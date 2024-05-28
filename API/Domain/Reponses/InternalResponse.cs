namespace Business.Reponses;

public class InternalResponse<T>
{
    public T? Data{ get; set; }
    public bool Success { get; set; }
    public bool Error { get; set; }
    public string? Message{ get; set; }

    public InternalResponse()
    {
        
    }

    public InternalResponse(Exception ex)
    {
        Data = default;
        Success = false;
        Error = true;
        Message = ex.Message;
    }
}
