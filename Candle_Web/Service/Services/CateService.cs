using AutoMapper;
using Model.Models;
using Repo.Repository;
using Repo.Repository.Interface;
using Service.Modals;
using Service.Modals.Request;
using Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CateService : ICateService
    {

        private readonly ICateRepo cate;
        private readonly IMapper _mapper;

        public CateService(ICateRepo cate, IMapper mapper)
        {
            this.cate = cate;
            _mapper = mapper;
        }

        public async Task<CateRequest> Create(CateRequest order)
        {
            try
            {
                var map = _mapper.Map<Category>(order);
                var createCandle = await cate.Create(map);
                var resutl = _mapper.Map<CateRequest>(createCandle);
                return resutl;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(int order)
        {
            try
            {
                var candle = await cate.GetCateById(order);
                if (candle == null)
                {
                    throw new Exception($"Category {order} does not exist");
                }

                await cate.Delete(candle);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Category>> GetALL()
        {
            var data = await cate.GetALL();
            return data;    
        }

        public async Task<List<Candle>> GetCandleByCategoryId(int id)
        {
            var data = await cate.GetCandleByCategoryId(id);
            return data;
        }
    }
}
