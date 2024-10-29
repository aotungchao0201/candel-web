using AutoMapper;
using Model.Models;
using Repo.Repository.Interface;
using Service.Modals;
using Service.Modals.Request;
using Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CandleService : ICandleService
    {

        private readonly ICandleRepo _candleRepo;
        private readonly IMapper _mapper;

        public CandleService(ICandleRepo candleRepo, IMapper mapper)
        {
            _candleRepo = candleRepo;
            _mapper = mapper;
        }

        public async Task<CandleRequest> createCandle(CandleRequest candleDTO)
        {
            try
            {
                var map = _mapper.Map<Candle>(candleDTO);
                var createCandle = await _candleRepo.CreateCandle(map);
                var resutl = _mapper.Map<CandleRequest>(createCandle);
                return resutl;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public async Task<bool> deleteCandle(int id)
        {
            try
            {
                var candle = await _candleRepo.GetCandleById(id);
                if (candle == null)
                {
                    throw new Exception($"Candle {id} does not exist");
                }

                await _candleRepo.DeleteCandle(candle);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CandleDTO>> GetAllcandleAscyn()
        {
            try
            {

                var data = await _candleRepo.GetAllCandle();

                if (!data.Any())
                {
                    return null;
                }

                var map = _mapper.Map<List<CandleDTO>>(data);

                return map;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> updateCandle(int id, CandleRequest dto)
        {
            try
            {
                var candle = await _candleRepo.GetCandleById(id);
                if (candle == null)
                {
                    return false;
                }
                
                _mapper.Map(dto, candle);
                await _candleRepo.UpdateCandle(candle);
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Fail to update candle {ex.Message}");
                return false;
            }
        }
    }
}
