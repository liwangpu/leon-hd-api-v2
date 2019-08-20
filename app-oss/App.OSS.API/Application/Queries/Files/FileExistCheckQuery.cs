using MediatR;

namespace App.OSS.API.Application.Queries.Files
{
    public class FileExistCheckQuery : IRequest<string>
    {
        public string Md5 { get; protected set; }

        #region ctor
        public FileExistCheckQuery(string md5)
        {
            Md5 = md5;
        }
        #endregion
    }

}
