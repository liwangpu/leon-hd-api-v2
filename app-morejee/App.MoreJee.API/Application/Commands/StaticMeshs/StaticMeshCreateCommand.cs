using MediatR;

namespace App.MoreJee.API.Application.Commands.StaticMeshs
{
    public class StaticMeshCreateCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        /// <summary>
        /// 创建默认模型,默认是不会创建的
        /// </summary>
        public bool CreateProduct { get; set; }
    }
}
