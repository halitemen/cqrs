using CQRSExample.Dto;
using MediatR;

namespace CQRSExample.Commands
{
    public class CreateCommentCommand : IRequest<CommentDto>
    {
        public long PostId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
    }
}
