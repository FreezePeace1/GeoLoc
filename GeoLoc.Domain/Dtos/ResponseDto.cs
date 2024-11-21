namespace GeoLoc.Domain.Dtos;

public class ResponseDto
{
    public string? SuccessMessage { get; set; }
    
    public string? ErrorMessage { get; set; }

    public bool IsSucceed => ErrorMessage == null;
    
    public int? ErrorCode { get; set; }
}

public class ResponseDto<T> : ResponseDto
{
    public ResponseDto()
    {
    }
    
    public ResponseDto(string errorMessage,string successMessage,int errorCode)
    {
        SuccessMessage = successMessage;
        errorCode = errorCode;
        errorMessage = errorMessage;
    }
    
    public T Data { get; set; }
}