namespace P7CreateRestApi.Repositories
{
    using global::P7CreateRestApi.Data;
    using global::P7CreateRestApi.Domain;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    namespace P7CreateRestApi.Repositories
    {
        public class CurvePointRepository : ICurvePointRepository
        {
            private readonly LocalDbContext _context;

            public CurvePointRepository(LocalDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<CurvePoint>> GetAllAsync()
            {
                return await _context.CurvePoints.ToListAsync();
            }

            public async Task<CurvePoint> GetByIdAsync(int id)
            {
                return await _context.CurvePoints.FindAsync(id);
            }

            public async Task AddAsync(CurvePoint curvePoint)
            {
                _context.CurvePoints.Add(curvePoint);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(CurvePoint curvePoint)
            {
                _context.CurvePoints.Update(curvePoint);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(int id)
            {
                var curvePoint = await _context.CurvePoints.FindAsync(id);
                if (curvePoint != null)
                {
                    _context.CurvePoints.Remove(curvePoint);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}


