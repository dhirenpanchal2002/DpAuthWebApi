using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace DpAuthWebApi.Controllers
{
    public class BaseApiController : ControllerBase
    {
        public string CurrentUserId
        {
            get
            {
                return GetClaimValue(ClaimTypes.NameIdentifier.ToString());
            }
        }

        public string CurrentUserEmail
        {
            get
            {
                return GetClaimValue(ClaimTypes.Email.ToString());
            }
        }

        public string CurrentUserName
        {
            get
            {
                return GetClaimValue(ClaimTypes.Name.ToString());
            }
        }

        private string GetClaimValue(string claimType)
        {
            return User.Claims.Where(x => string.Equals(x.Type, claimType, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault().Value;
        }
    }
}
