﻿
using MediatR;

namespace Task_Novin_Teck.ReqClasses
{
    // for command
    public class CreateUserCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }

}
