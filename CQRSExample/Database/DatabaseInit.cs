using CQRSExample.Context;
using CQRSExample.DAL.UnitOfWork;
using CQRSExample.Dto;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace CQRSExample.Database
{
    public class DatabaseInit : IDisposable
    {
        public void InitializeDb()
        {
            using (var uow = new UnitOfWork<MasterContext>())
            {
                var count = uow.GetQueryRepository<CommentDto>().Count();
                if (count > 0)
                {
                    return;
                }
                var list = GetData();
                foreach (var item in list)
                {
                    uow.GetCommandRepository<CommentDto>().Add(item);
                }
                uow.SaveChanges();
            }
        }

        private List<CommentDto> GetData()
        {
            RestClient restClient = new RestClient("https://jsonplaceholder.typicode.com/comments");
            RestRequest restRequest = new RestRequest(Method.GET);
            var response = restClient.Execute(restRequest);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var commentDtoList = JsonConvert.DeserializeObject<List<CommentDto>>(response.Content);
                return commentDtoList;
            }
            return null;
        }

        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
        }
    }
}
