using App.Basic.Domain.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Basic.API.Infrastructure.Services
{
    public interface IAccessPointTranslatorService
    {
        string[] Translate(string pointKey);
    }

    public class AccessPointTranslatorService : IAccessPointTranslatorService
    {

        public AccessPointTranslatorService()
        {

        }

        public string[] Translate(string pointKey)
        {
            var texts = new string[2];
            //var t = typeof(AccessPointInnerPointKeyConst);


            //throw new NotImplementedException();
            return texts;
        }
    }
}
