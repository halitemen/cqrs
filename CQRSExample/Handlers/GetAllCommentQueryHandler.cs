using CQRSExample.Context;
using CQRSExample.DAL.UnitOfWork;
using CQRSExample.Dto;
using CQRSExample.Queries;
using CQRSExample.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSExample.Handlers
{
    public class GetAllCommentQueryHandler : IRequestHandler<GetCommentsQuery, List<CommentDto>>
    {
        public async Task<List<CommentDto>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            using (var uow = new UnitOfWork<MasterContext>())
            {
                return uow.GetQueryRepository<CommentDto>().GetAll().ToList();
            }
        }
    }
}
