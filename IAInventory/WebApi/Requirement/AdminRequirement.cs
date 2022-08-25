//using Microsoft.AspNetCore.Authorization;
//using System.Security.Claims;

//namespace WebApi.Requirement
//{
//    public class AdminRequirement : AuthorizationHandler<AdminRequirement>, IAuthorizationRequirement
//    {
       
//        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
//        {
//            if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
//            {
//                context.Fail();
//                return Task.CompletedTask;
//            }

//            var dobVal = context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth).Value;
//            var dateOfBirth = Convert.ToDateTime(dobVal);
//            int age = DateTime.Today.Year - dateOfBirth.Year;
//            if (dateOfBirth > DateTime.Today.AddYears(-age))
//            {
//                age--;
//            }

//            if (age >= 18)
//            {
//               return Task.FromResult(context.Succeed(requirement));
//            }
//            else
//            {
//              return  context.Fail();
//            }
//        }
//    }
//}
