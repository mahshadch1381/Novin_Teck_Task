using MediatR;
using Task_Novin_Teck.Model;

namespace Task_Novin_Teck.ReqClasses
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public int UserId { get; set; }
    }
}
