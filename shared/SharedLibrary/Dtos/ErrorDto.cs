namespace SharedLibrary.Models
{
    public class ErrorDto
    {
        public List<string> Errors { get; private set; }
        public string Path { get; set; }
        public bool IsShow { get; private set; }

        public ErrorDto()
        {
            Errors = new List<string>();
        }

        public ErrorDto(string error, bool isShow)
        {
            Errors = new List<string>
            {
                error
            };
            IsShow = isShow;
        }

        public ErrorDto(string error, string path, bool isShow)
        {
            Errors = new List<string>
            {
                error
            };
            Path = path;
            IsShow = isShow;
        }

        public ErrorDto(List<string> errors, bool isShow)
        {
            Errors = errors;
            IsShow = isShow;
        }
    }
}
