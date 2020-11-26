using CQRSExample.Dto;
using MediatR;
using System.Collections.Generic;

namespace CQRSExample.Queries
{
    public class GetCommentsQuery : IRequest<List<CommentDto>>
    {
        public GetCommentsQuery()
        {

        }
    }
}
