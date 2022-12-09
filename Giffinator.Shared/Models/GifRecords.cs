namespace Giffinator.Shared.Models
{
    public class GifRecords
    {
        public GifRecords(string fullPath, long fileSize, string thumbNailPath)
        {
            FullPath = fullPath;
            FileSize = fileSize;
            ThumbNailPath = thumbNailPath;
        }

        public string FullPath { get; init; }
        public long FileSize { get; init; }
        public string ThumbNailPath { get; init; }
        
        public string FileName { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public int Id { get; set; }
        

        public void Deconstruct(out string fullPath, out long fileSize, out string thumbNailPath)
        {
            fullPath = this.FullPath;
            fileSize = this.FileSize;
            thumbNailPath = this.ThumbNailPath;
        }
    }
}