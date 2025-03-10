﻿using AutoMapper;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface.Features;
using Pacagroup.Ecommerce.Application.Interface.Infraestructure;
using Pacagroup.Ecommerce.Application.Interface.Persistence;
using Pacagroup.Ecommerce.Domain.Entities;
using Pacagroup.Ecommerce.Domain.Events;
using Pacagroup.Ecommerce.Transversal.Common;

namespace Pacagroup.Ecommerce.Application.Features.Discounts
{
    public class DiscountsApplication : IDiscountsApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBust;        

        public DiscountsApplication(IUnitOfWork unitOfWork, IMapper mapper, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;            
            _eventBust = eventBus;
        }

        public async Task<Response<bool>> Create(DiscountDto discountDto, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();
            try
            {            
                var discount = _mapper.Map<Discount>(discountDto);
                await _unitOfWork.Discounts.InsertAsync(discount);

                response.Data = await _unitOfWork.Save(cancellationToken)>0;
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Registro Exitoso!!!";

                    var discountCreatedEvent = _mapper.Map<DiscountCreatedEvent>(discount);
                    _eventBust.Publish(discountCreatedEvent);
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<Response<bool>> Delete(int id, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();
            try
            {
                await _unitOfWork.Discounts.DeleteAsync(id.ToString());
                response.Data = await _unitOfWork.Save(cancellationToken) > 0;
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Eliminación Exitosa!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;                
            }
            return response;
        }

        public async Task<Response<DiscountDto>> Get(int id, CancellationToken cancellationToken = default)
        {
            var response = new Response<DiscountDto>();
            try
            {
                var discount = await _unitOfWork.Discounts.GetAsync(id, cancellationToken);
                if (discount is null)
                {
                    response.IsSuccess = true;
                    response.Message = "Descuento no existe...";
                    return response;
                }

                response.Data = _mapper.Map<DiscountDto>(discount);
                response.IsSuccess = true;
                response.Message = "Consulta Exitosa!!!";
            }
            catch (Exception e)
            {
                response.Message = e.Message;                
            }
            return response;
        }

        public async Task<Response<List<DiscountDto>>> GetAll(CancellationToken cancellationToken = default)
        {
            var response = new Response<List<DiscountDto>>();
            try
            {
                var discounts = await _unitOfWork.Discounts.GetAllAsync(cancellationToken);
                response.Data = _mapper.Map<List<DiscountDto>>(discounts);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<Response<bool>> Update(DiscountDto discountDto, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();
            try
            {             
                var discount = _mapper.Map<Discount>(discountDto);
                await _unitOfWork.Discounts.UpdateAsync(discount);

                response.Data = await _unitOfWork.Save(cancellationToken) > 0;
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Actualización Exitosa!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ResponsePagination<IEnumerable<DiscountDto>>> GetAllWithPagination(int pageNumber, int pageSize)
        {
            var response = new ResponsePagination<IEnumerable<DiscountDto>>();
            try
            {
                var count = await _unitOfWork.Discounts.CountAsync();

                var customers = await _unitOfWork.Discounts.GetAllWithPaginationAsync(pageNumber, pageSize);
                response.Data = _mapper.Map<IEnumerable<DiscountDto>>(customers);

                if (response.Data != null)
                {
                    response.PageNumber = pageNumber;
                    response.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                    response.TotalCount = count;
                    response.IsSuccess = true;
                    response.Message = "Consulta Paginada Exitosa!!!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
