using CQRSExample.Dto;
using MediatR;

namespace CQRSExample.Queries
{
    public class GetCommentQuery :IRequest<CommentDto>
    {
        public int Id { get; }
        public GetCommentQuery(int id)
        {
            Id = id;
        }
    }
}
