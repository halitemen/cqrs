using CQRSExample.Context;
using CQRSExample.DAL.UnitOfWork;
using CQRSExample.Dto;
using CQRSExample.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSExample.Handlers
{
    public class GetCountCommentQueryHandler : IRequestHandler<GetCommentsCountQuery, int>
    {
        public async Task<int> Handle(GetCommentsCountQuery request, CancellationToken cancellationToken)
        {
            using (var uow = new UnitOfWork<MasterContext>())
            {
                var count = uow.GetQueryRepository<CommentDto>().Count();
                return count;
            }
        }
    }
}
