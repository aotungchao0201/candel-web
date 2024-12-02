using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Repository.Interface
{
    public interface ICateRepo
    {
        public Task<List<Category>> GetALL();
        public Task<Category> Create(Category order);
        public Task<bool> Delete(Category order);

        public Task<List<Candle>> GetCandleByCategoryId(int id);

        public Task<Category> GetCateById(int id);

    }
}
