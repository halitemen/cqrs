using MediatR;
namespace CQRSExample.Queries
{
    public class GetCommentsCountQuery : IRequest<int>
    {
        public GetCommentsCountQuery()
        {

        }
    }
}
