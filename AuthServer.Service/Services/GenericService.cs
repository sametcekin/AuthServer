using AuthServer.Core.Repositories;
using AuthServer.Core.Services;
using AuthServer.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AuthServer.Service.Services
{
    public class GenericService<TEntity, TDto> : IGenericService<TEntity, TDto> where TEntity : class
        where TDto : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TEntity> _genericRepository;
        public GenericService(IUnitOfWork unitOfWork, IGenericRepository<TEntity> genericRepository)
        {
            _unitOfWork = unitOfWork;
            _genericRepository = genericRepository;
        }

        public async Task<Response<TDto>> AddAsync(TDto dto)
        {
            var newEntity = ObjectMapper.Mapper.Map<TEntity>(dto);
            await _genericRepository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();

            var newDto = ObjectMapper.Mapper.Map<TDto>(newEntity);
            return Response<TDto>.Success(newDto, 200);
        }

        public async Task<Response<IEnumerable<TDto>>> GetAllAsync()
        {
            var dtos = ObjectMapper.Mapper.Map<List<TDto>>(await _genericRepository.GetAllAsync());
            return Response<IEnumerable<TDto>>.Success(dtos, 200);
        }

        public async Task<Response<TDto>> GetByIdAsync(int id)
        {
            var dto = await _genericRepository.GetByIdAsync(id);
            if (dto is null)
                return Response<TDto>.Fail("Not found", 404, true);

            return Response<TDto>.Success(ObjectMapper.Mapper.Map<TDto>(dto), 200);
        }

        public async Task<Response<NoDataDto>> Remove(int id)
        {
            var existEntity = await _genericRepository.GetByIdAsync(id);

            if (existEntity is null)
                return Response<NoDataDto>.Fail("Not found", 404, true);

            _genericRepository.Remove(existEntity);
            await _unitOfWork.CommitAsync();
            return Response<NoDataDto>.Success(200);
        }

        public async Task<Response<NoDataDto>> Update(TDto dto, int id)
        {
            // This is Detached entity. So we create update entity.
            var existEntity = await _genericRepository.GetByIdAsync(id);

            if (existEntity is null)
                return Response<NoDataDto>.Fail("Not found", 404, true);

            var updateEntity = ObjectMapper.Mapper.Map<TEntity>(dto);

            _genericRepository.Update(updateEntity);
            await _unitOfWork.CommitAsync();
            return Response<NoDataDto>.Success(204);
        }

        public async Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var list = _genericRepository.Where(predicate);

            return Response<IEnumerable<TDto>>.Success(ObjectMapper.Mapper.Map<IEnumerable<TDto>>(await list.ToListAsync()), 200);
        }
    }
}
