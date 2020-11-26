using CQRSExample.Context;
using CQRSExample.DAL.UnitOfWork;
using CQRSExample.Dto;
using CQRSExample.Queries;
using CQRSExample.Repository;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSExample.Handlers
{
    public class GetCommentQueryHandler : IRequestHandler<GetCommentQuery, CommentDto>
    {
        public async Task<CommentDto> Handle(GetCommentQuery request, CancellationToken cancellationToken)
        {
            using (var uow = new UnitOfWork<MasterContext>())
            {
                var result = uow.GetQueryRepository<CommentDto>().Get(x => x.Id == request.Id);
                return result;
            }
        }
    }
}
