namespace MediaDownload
{
    public interface IMediaDownloader<T>
    {
        T DownloadMedia();
    }
}