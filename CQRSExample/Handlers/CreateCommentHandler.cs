using CQRSExample.Commands;
using CQRSExample.Context;
using CQRSExample.DAL.UnitOfWork;
using CQRSExample.Dto;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSExample.Handlers
{
    public class CreateCommentHandler : IRequestHandler<CreateCommentCommand, CommentDto>
    {
        public async Task<CommentDto> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var model = new CommentDto
            {
                Body = request.Body,
                Email = request.Email,
                Name = request.Name,
                PostId = request.PostId
            };
            using (var uow = new UnitOfWork<MasterContext>())
            {
                uow.GetCommandRepository<CommentDto>().Add(model);
                uow.SaveChanges();
            }
            return model;
        }
    }
}
