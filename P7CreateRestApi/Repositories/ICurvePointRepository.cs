using P7CreateRestApi.Domain;
 using System.Collections.Generic;
using System.Threading.Tasks;

namespace P7CreateRestApi.Repositories
    {
        public interface ICurvePointRepository
        {
            Task<IEnumerable<CurvePoint>> GetAllAsync();
            Task<CurvePoint> GetByIdAsync(int id);
            Task AddAsync(CurvePoint curvePoint);
            Task UpdateAsync(CurvePoint curvePoint);
            Task DeleteAsync(int id);
        }
    }



