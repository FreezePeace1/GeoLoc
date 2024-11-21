namespace GeoLoc.Domain.Enums;

public enum ErrorCode
{
    //Error codes for services, for every service setting range of error codes
    //FileService - 0-8
    UploadingFileIsFailed,
    FilePickedWrongly,
    DirectoryDoesNotExist,
    ReceivingIsFailed,
    FileIsNotFound,
    FilenameIsNotProvided,
    DownloadingFileIsFailed,
    ReadingFileIsFailed,
    
    //ChartService - 9
    ChartIsFailedToCreate,
    
    //MapService - 10
    FailedToGetDataForMap,
}